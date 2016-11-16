using Model;
using System.Collections.ObjectModel;
using Model.DataFromView;
using System.Collections.Generic;

namespace Service.SerializeServices
{
    public interface ISerializeService
    {
        ObservableCollection<DataSource> SerializeDataSource(DataSource dataSource);

        ObservableCollection<DataSource> SerializeDataSourceList(ObservableCollection<DataSource> dataSourceListReceived);

        ObservableCollection<DataSource> ReturnDataSource();
        void SerializeRemoteConnection(RemoteConnection remoteConnection);
        RemoteConnection DeserializeRemote();
        void SerializeCompanyName(string companyName);
        List<string> DeserializeCompanyName();
    }
}