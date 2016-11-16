using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Model.DataFromView;

namespace Service.SerializeServices
{
    public class SerializeService : ISerializeService
    {
        private readonly string fileName = "SerializedData.bin";
        private readonly string remoteFile = "SerializedDataRemote.bin";
        private readonly string companiesBlacklistFile = "SerializedCompaniesBlacklist.bin";

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

        public void SerializeRemoteConnection(RemoteConnection remoteConnection)
        {
            string path = "./" + remoteFile;
            FileInfo myfileinf = new FileInfo(path);
            myfileinf.Delete();
            if (remoteConnection == null)
                return;
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(remoteFile,
                         FileMode.Create,
                         FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, remoteConnection);
            stream.Close();
        }

        public RemoteConnection DeserializeRemote()
        {
            if (File.Exists(fileName))
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(remoteFile,
                                          FileMode.Open,
                                          FileAccess.Read,
                                          FileShare.Read);
                RemoteConnection obj = (RemoteConnection)formatter.Deserialize(stream);
                stream.Close();

                return obj;
            }
            else
                return new RemoteConnection();
        }

        public List<string> DeserializeCompanyName()
        {
            if (File.Exists(companiesBlacklistFile))
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(companiesBlacklistFile,
                                          FileMode.Open,
                                          FileAccess.Read,
                                          FileShare.Read);
                List<string> obj = (List<string>)formatter.Deserialize(stream);
                stream.Close();

                return obj;
            }
            else
                return new List<string>();
        }

        public void SerializeCompanyName(string companyName)
        {
            var oldCompanies = DeserializeCompanyName();
            if (checkIfUniqueCompany(companyName, oldCompanies)) {
                oldCompanies.Add(companyName);
                string path = "./" + companiesBlacklistFile;
                FileInfo myfileinf = new FileInfo(path);
                myfileinf.Delete();
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(companiesBlacklistFile,
                             FileMode.Create,
                             FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, oldCompanies);
                stream.Close();
            }
        }

        private bool checkIfUniqueCompany(string companyName, List<string> oldCompanies)
        {
            if (oldCompanies.Contains(companyName))
                return false;
            return true;
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

