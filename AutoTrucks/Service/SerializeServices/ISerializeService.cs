using Model;
using System.Collections.ObjectModel;

namespace Service.SerializeServices
{
    public interface ISerializeService
    {
        bool SerializeDataSource(DataSource dataSource);

        bool SerializeDataSourceList(ObservableCollection<DataSource> dataSourceListReceived);

        ObservableCollection<DataSource> ReturnDataSource();
    }
}