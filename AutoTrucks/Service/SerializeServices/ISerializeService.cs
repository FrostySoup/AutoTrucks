﻿using Model;
using System.Collections.ObjectModel;
using Model.DataFromView;

namespace Service.SerializeServices
{
    public interface ISerializeService
    {
        ObservableCollection<DataSource> SerializeDataSource(DataSource dataSource);

        ObservableCollection<DataSource> SerializeDataSourceList(ObservableCollection<DataSource> dataSourceListReceived);

        ObservableCollection<DataSource> ReturnDataSource();
        void SerializeRemoteConnection(RemoteConnection remoteConnection);
    }
}