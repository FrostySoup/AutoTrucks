using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Service.ConnexionService.AlarmService
{
    public class AlarmService : IAlarmService
    {
        private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(500);

        protected bool RequireDistinctUserAccounts
        {
            get { return true; }
        }

        public int Execute(ISessionFacade session, Uri alarmUrl)
        {
            PostAssetRequest load = new PostAssetRequest();
            PostAssetRequest truckWithAlarm = new PostAssetRequest();
            DateTime deadline = DateTime.Now + Timeout;

            // listener implements IDisposable to allow cleanup of HTTP ports
            using (var listener = new Listener(new Uri("http://192.168.10.118:1010/AlarmMatch"), deadline))
            {
                var cancellableWaitForAlarm = new CancellationTokenSource();

                // task for getting the listener started
                Task startListener = Task.Factory.StartNew(() => listener.Start());

                // task for returning an HTTP request, if any
                Task<HttpListenerRequest> receiveAlarm = startListener.ContinueWith(p => listener.GetRequest(),
                                                                                    cancellableWaitForAlarm.Token);

                // if receive an alarm before the timeout, print it to console
                if (receiveAlarm.Wait(Timeout))
                {
                    if (receiveAlarm.IsCanceled)
                    {
                        Console.WriteLine("Background task canceled before any alarm was received.");
                        return -1;
                    }
                    if (receiveAlarm.IsFaulted)
                    {
                        Console.WriteLine("Background task faulted before any alarm was received.");
                        return -1;
                    }
                    if (!receiveAlarm.IsCompleted)
                    {
                        Console.WriteLine("Unspecified error with listener awaiting alarm in background task.");
                        return -1;
                    }
                    Console.WriteLine("Background task completed.");
                    HttpListenerRequest request = receiveAlarm.Result;
                    Console.WriteLine("HTTP requested host name and port: " + request.UserHostName);
                    Console.WriteLine("HTTP requested IP address and port: " + request.UserHostAddress);
                    Console.WriteLine("HTTP raw url: " + request.RawUrl);
                    Console.WriteLine("HTTP method: " + request.HttpMethod);
                    Console.WriteLine("HTTP content type: " + request.ContentType);
                    if (request.HasEntityBody)
                    {
                        Console.WriteLine("HTTP body: ");
                    }
                    Console.WriteLine("IP address and port of requester: " + request.RemoteEndPoint);
                }
                else
                {
                    Console.WriteLine("Background task timed out after {0}ms.", Timeout.TotalMilliseconds);
                    return 0;
                }
            }
            return 1;
        }

        protected int Validate()
        {
            if (HttpListener.IsSupported == false)
            {
                Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                return -1;
            }
            return 1;
        }

        public class Listener : IDisposable
        {
            private HttpListener _httpListener;
            private bool _disposed;
            private readonly DateTime _deadline;

            public Listener(Uri alarmUrl, DateTime deadline)
            {
                _deadline = deadline;
                _httpListener = new HttpListener();
                string uriPrefix = alarmUrl.AbsoluteUri;
                if (uriPrefix.Last() != '/')
                {
                    uriPrefix += "/";
                }
                _httpListener.Prefixes.Add(uriPrefix);
            }

            ~Listener()
            {
                Dispose(false);
            }

            public void Start()
            {
                EnsureNotDisposed();

                _httpListener.Start();

                while (_httpListener.IsListening == false)
                {
                    if (DeadlineExpired())
                    {
                        return;
                    }
                }
            }

            private bool DeadlineExpired()
            {
                return DateTime.Now >= _deadline;
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            private void Dispose(bool disposing)
            {
                if (_disposed)
                {
                    return;
                }
                if (_httpListener != null)
                {
                    if (_httpListener.IsListening)
                    {
                        _httpListener.Abort();
                        _httpListener = null;
                    }
                }
                _disposed = true;
            }


            public HttpListenerRequest GetRequest()
            {
                EnsureNotDisposed();
                // block until request arrives
                HttpListenerContext httpListenerContext = _httpListener.GetContext();
                return httpListenerContext.Request;
            }


            private void EnsureNotDisposed()
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(GetType().Name);
                }
            }
        }
    }
}