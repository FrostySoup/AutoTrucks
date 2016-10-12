using Service.AddNewWindowFactory;
using Service.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModels.PopUpWindowViewModels;
using Service.DataConvertService;
using Service.ConnexionService;
using Model.DataFromView;

namespace ViewModels.MainWindowViewModels
{
    public class SearchLoadsViewModel : SearchViewModelAbstract, IMainWindowDisplayViewModel
    {

        /* private SearchOperationParams SetValuesForSearch()
        {
            var origin = new SearchArea { stateProvinces = new[] { StateProvince.CA } };

            var destination = new SearchArea { stateProvinces = new[] { StateProvince.IL } };

            var searchCriteria = new CreateSearchCriteria
            {
                ageLimitMinutes = 90,
                ageLimitMinutesSpecified = true,
                assetType = AssetType.Shipment,
                destination = new GeoCriteria { Item = destination },
                equipmentClasses = new[] { EquipmentClass.Flatbeds, EquipmentClass.Reefers },
                includeFulls = true,
                includeLtls = true,
                origin = new GeoCriteria { Item = origin }
            };

            return new SearchOperationParams
            {
                criteria = searchCriteria,
                includeSearch = true,
                includeSearchSpecified = true,
                sortOrder = SortOrder.Age,
                sortOrderSpecified = true
            };
        }*/
        public SearchLoadsViewModel(IDataConvertSingleton dataConvertSingleton,ISessionCacheSingleton sessionCacheSingleton,
            ISearchWindowViewModel searchWindowViewModel, IConnectConnexionService connectConnexionService) 
            : base(dataConvertSingleton, sessionCacheSingleton, searchWindowViewModel, connectConnexionService)
        {
        }

        protected override void AddNewSearch(SearchDataFromView searchData)
        {
            //
        }
    }
}
