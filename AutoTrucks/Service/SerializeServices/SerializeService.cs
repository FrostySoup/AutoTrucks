using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Model.DataFromView;
using Model.DataHelpers;

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

        public List<string> SerializeCompanyName(string companyName)
        {
            var companies = DeserializeCompanyName();

            if (checkIfUniqueCompany(companyName, companies)) {
                companies.Add(companyName);
                SerializeCompanyNamesListToFile(companies);
            }
            return companies;
        }

        private void SerializeCompanyNamesListToFile(List<string> companyNamesList)
        {
            string path = "./" + companiesBlacklistFile;
            FileInfo myfileinf = new FileInfo(path);
            myfileinf.Delete();

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(companiesBlacklistFile,
                         FileMode.Create,
                         FileAccess.Write, FileShare.None);

            formatter.Serialize(stream, companyNamesList);
            stream.Close();
        }

        private bool checkIfUniqueCompany(string companyName, List<string> oldCompanies)
        {
            if (oldCompanies.Contains(companyName))
                return false;
            return true;
        }

        public void SerializeCompanyNamesList(ObservableCollection<StringWrapper> companiesCollection)
        {
            List<string> companiesCollectionList = ToCompaniesCollectionList(companiesCollection);
            SerializeCompanyNamesListToFile(companiesCollectionList);
        }

        private List<string> ToCompaniesCollectionList(ObservableCollection<StringWrapper> companiesCollection)
        {
            List<string> stringListToSerialize = new List<string>();

            foreach (var stringWrapper in companiesCollection)
            {
                stringListToSerialize.Add(stringWrapper.Value);
            }

            return stringListToSerialize;
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

