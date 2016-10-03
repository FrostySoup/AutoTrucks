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
    public class SerializeServiceSingleton
    {
        private readonly string fileName = "SerializedData.bin";


        private static SerializeServiceSingleton instance;

        private SerializeServiceSingleton()
        {}

        public static SerializeServiceSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SerializeServiceSingleton();
                }
                return instance;
            }
        }

        public bool SerializeDataSource(DataSource dataSource)
        {

            DataSourceList dataSourceList = DeserializeDataSource();
            if (dataSource == null)
                return false;
            dataSourceList.DataSourceLis.Add(dataSource);

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(fileName,
                         FileMode.Create,
                         FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, dataSourceList);
            stream.Close();
            return true;
        }

        public bool SerializeDataSourceList(ObservableCollection<DataSource> dataSourceListReceived)
        {
            DataSourceList dataSourceList = new DataSourceList();
            dataSourceList.DataSourceLis = new List<DataSource>(dataSourceListReceived);
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(fileName,
                         FileMode.Create,
                         FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, dataSourceList);
            stream.Close();
            return true;
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
