using Model.DataFromView;
using Model.ReceiveData.AlarmMatch;
using Service.DataExtractService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Input;

namespace Service.ConnexionService.AlarmService
{
    public class HttpService : IHttpService
    {
        private readonly HttpListener _listener;
        private readonly Thread _listenerThread;
        private readonly Thread[] _workers;
        private readonly ManualResetEvent _stop, _ready;
        private Queue<HttpListenerContext> _queue;
        private readonly IDataExtractService _dataExtractService;
        private List<DisplayFoundAsset> _foundAssets;
        public ICommand AssetUpdatedCommand { get; set; }

        public HttpService(IDataExtractService dataExtractService)
        {
            int maxThreads = 5;
            _foundAssets = new List<DisplayFoundAsset>();
            _dataExtractService = dataExtractService;
            _workers = new Thread[maxThreads];
            _queue = new Queue<HttpListenerContext>();
            _stop = new ManualResetEvent(false);
            _ready = new ManualResetEvent(false);
            _listener = new HttpListener();
            _listenerThread = new Thread(HandleRequests);
        }

        private IPAddress GetPrivateIP()
        {
            string hostName = Dns.GetHostName();
            IPAddress[] addresses = Dns.GetHostAddresses(hostName);
            return addresses.First(x => x.AddressFamily == AddressFamily.InterNetwork);
        }

        public void Start(RemoteConnection remoteConnection)
        {
            if (remoteConnection != null && remoteConnection.Port != null)
            {
                if (_listener.Prefixes.Count > 0)
                    _listener.Prefixes.Clear();
                _listener.Prefixes.Add(string.Format("http://{0}:{1}/AlarmMatch/", GetPrivateIP().ToString(), remoteConnection.Port));
                int a = 5;
                try
                {
                    _listener.Start();
                }
                catch (HttpListenerException)
                {
                    return;
                }
                _listenerThread.Start();

                for (int i = 0; i < _workers.Length; i++)
                {
                    _workers[i] = new Thread(Worker);
                    _workers[i].Start();
                }
            }
        }

        public void Dispose()
        { Stop(); }

        public void Stop()
        {
            _stop.Set();
            _listenerThread.Join();
            foreach (Thread worker in _workers)
                worker.Join();
            _listener.Stop();
        }

        private void HandleRequests()
        {
            while (_listener.IsListening)
            {
                var context = _listener.BeginGetContext(ContextReady, null);

                if (0 == WaitHandle.WaitAny(new[] { _stop, context.AsyncWaitHandle }))
                {
                    return;
                }
            }
        }

        private void ContextReady(IAsyncResult ar)
        {
            try
            {
                lock (_queue)
                {
                    _queue.Enqueue(_listener.EndGetContext(ar));
                    _ready.Set();
                }
            }
            catch { return; }
        }

        private void Worker()
        {
            WaitHandle[] wait = new[] { _ready, _stop };
            while (0 == WaitHandle.WaitAny(wait))
            {
                HttpListenerRequest context;
                lock (_queue)
                {
                    if (_queue.Count > 0)
                    {
                        context = _queue.Dequeue().Request;                        
                    }
                    else
                    {
                        _ready.Reset();
                        continue;
                    }
                }
                if (context != null) {
                    DisplayFoundAsset displayFoundAsset = _dataExtractService.ConvertContextToDisplayFoundAsset(context.InputStream);
                    if (displayFoundAsset != null)
                    {
                        lock (_foundAssets)
                        {
                            _foundAssets.Add(displayFoundAsset);
                            AssetUpdatedCommand.Execute(null);
                        }
                    }
                }

            }
        }

        public ObservableCollection<DisplayFoundAsset> GetAssets()
        {
            return new ObservableCollection<DisplayFoundAsset>(_foundAssets);
        }

        public void BindCommand(ICommand assetUpdatedCommand)
        {
            this.AssetUpdatedCommand = assetUpdatedCommand;
        }

        public void ClearFoundAssets()
        {
            lock (_foundAssets)
            {
                _foundAssets = new List<DisplayFoundAsset>();
            }
        }

        public event Action<HttpListenerContext> ProcessRequest;
    }
}