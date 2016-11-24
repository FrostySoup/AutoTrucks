using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DataFromView;
using Model.ReceiveData.AlarmMatch;
using Service.ConnexionService;
using Service.ConnexionService.AlarmService;
using Service.SerializeServices;

namespace Service.ViewModelsHelpers
{
    public class AssetsViewModelHelper : IAssetsViewModelHelper
    {            
        private List<string> deserializedCompaniesList { get; set; }

        private readonly ISerializeService serializeService;

        public AssetsViewModelHelper(ISerializeService serializeService)
        {
            this.serializeService = serializeService;
            deserializedCompaniesList = serializeService.DeserializeCompanyName();
        }

        public Alarm AddAlarmForAsset(ISessionFacade sessionFacade, PostDataFromView asset, IConnectConnexionService connectConnexionService)
        {
            var alarmSearchCriteria = new AlarmSearchCriteria();

            if (asset.DHD >= 0)
                alarmSearchCriteria.destinationRadius = new Mileage()
                {
                    miles = asset.DHD,
                    method = MileageType.Road
                };

            if (asset.DHO >= 0)
                alarmSearchCriteria.originRadius = new Mileage()
                {
                    miles = asset.DHO,
                    method = MileageType.Road
                };      
            else
                alarmSearchCriteria.originRadius = new Mileage()
                {
                    miles = 50,
                    method = MileageType.Road
                };

            alarmSearchCriteria.maxMatches = 30;
            alarmSearchCriteria.maxMatchesSpecified = true;

            return connectConnexionService.CreateAlarm(sessionFacade, asset.ID, alarmSearchCriteria);
        }

        public ObservableCollection<DisplayFoundAsset> GetAssetsFromHttpService(IHttpService httpService, ObservableCollection<PostDataFromView> postAssets)
        {
            var assets = httpService.GetAssets();

            assets = removeBlacklistedCompaniesAssets(assets, httpService);

            var lastAddedAsset = assets.LastOrDefault();

            if (lastAddedAsset != null)
            {
                string assetId = lastAddedAsset.AssetId;
                bool matchAsset = false;
                foreach (var post in postAssets)
                {
                    if (assetId.Equals(post.ID))
                    {
                        lastAddedAsset.BackgroundColor = post.BackgroundColor;
                        matchAsset = true;
                        break;
                    }
                }
                if (!matchAsset)
                {
                    httpService.RemoveAsset(lastAddedAsset);
                    assets.Remove(lastAddedAsset);
                }
            }

            return assets;
        }

        private ObservableCollection<DisplayFoundAsset> removeBlacklistedCompaniesAssets(ObservableCollection<DisplayFoundAsset> assets, IHttpService httpService)
        {
            for(int i = assets.Count - 1; i >= 0; i--)
            {
                foreach (var blacklistedCompany in deserializedCompaniesList)
                {
                    if (assets[i].CompanyName != null && assets[i].CompanyName.Equals(blacklistedCompany))
                    {
                        httpService.RemoveAsset(assets[i]);
                        assets.RemoveAt(i);
                    }
                }
            }
            return assets;
        }

        public ObservableCollection<PostDataFromView> RemoveSelectedAssets(ISessionFacade sessionFacade, ObservableCollection<PostDataFromView> postAssets, IConnectConnexionService connectConnexionService)
        {
            List<string> Ids = new List<string>();

            foreach (var item in postAssets)
            {
                if (item != null && item.Marked && item.ID != null)
                {
                    Ids.Add(item.ID);
                }
            }

            if (Ids.Count > 0)
            {
                connectConnexionService.DeleteAssetsById(sessionFacade, Ids.ToArray());
                postAssets = new ObservableCollection<PostDataFromView>(postAssets
                    .Where(x => !Ids.Any(y => y.Equals(x.ID))));
            }

            return postAssets;
        }

        public void SerializeCompanyName(string companyName)
        {
            deserializedCompaniesList = serializeService.SerializeCompanyName(companyName);
        }
    }
}
