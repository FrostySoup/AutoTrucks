using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Service.SerializeServices
{
    public class SerializeService : ISerializeService
    {
        private readonly string fileName = "SerializedData.bin";

        public ObservableCollection<DataSource> SerializeDataSource(DataSource dataSource)
        {
            DataSourceList dataSourceList = DeserializeDataSource();
            if (dataSource == null)
                return new ObservableCollection<DataSource>(dataSourceList.DataSourceLis);
            dataSourceList.DataSourceLis.Add(dataSource);

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(fileName,
                         FileMode.Create,
                         FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, dataSourceList);
            stream.Close();
            return new ObservableCollection<DataSource>(dataSourceList.DataSourceLis);
        }

        public ObservableCollection<DataSource> SerializeDataSourceList(ObservableCollection<DataSource> dataSourceListReceived)
        {
            string path = "./" + fileName;
            FileInfo myfileinf = new FileInfo(path);
            myfileinf.Delete();
            if (dataSourceListReceived == null || dataSourceListReceived.Count < 1)
                return new ObservableCollection<DataSource>();
            DataSourceList dataSourceList = new DataSourceList();
            dataSourceList.DataSourceLis = new List<DataSource>(dataSourceListReceived);
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(fileName,
                         FileMode.Create,
                         FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, dataSourceList);
            stream.Close();
            return new ObservableCollection<DataSource>(dataSourceList.DataSourceLis);
        }

        private DataSourceList DeserializeDataSource()
        {
            if (File.Exists(fileName))
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(fileName,
                                          FileMode.Open,
                                          FileAccess.Read,
                                          FileShare.Read);
                DataSourceList obj = (DataSourceList)formatter.Deserialize(stream);
                stream.Close();

                return obj;
            }
            else
                return new DataSourceList();
        }

        public ObservableCollection<DataSource> ReturnDataSource()
        {
            DataSourceList dataSource = DeserializeDataSource();
            return new ObservableCollection<DataSource>(dataSource.DataSourceLis);
        }
    }


    [Serializable]
    internal class DataSourceList
    {
        public List<DataSource> DataSourceLis { get; set; }

        public DataSourceList()
        {
            DataSourceLis = new List<DataSource>();
        }
    }
}

