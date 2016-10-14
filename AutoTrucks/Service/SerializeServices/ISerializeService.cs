using Model;
using System.Collections.ObjectModel;

namespace Service.SerializeServices
{
    public interface ISerializeService
    {
        ObservableCollection<DataSource> SerializeDataSource(DataSource dataSource);

        ObservableCollection<DataSource> SerializeDataSourceList(ObservableCollection<DataSource> dataSourceListReceived);

        ObservableCollection<DataSource> ReturnDataSource();
    }
}