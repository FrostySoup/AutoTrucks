using Model.SearchCRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.SendData
{
    [Serializable]
    public class SearchOperationParams
    {
        public CreateSearchCriteria criteria { get; set; }

        public bool includeSearch { get; set; }

        public bool includeSearchSpecified { get; set; }

        public SortOrder sortOrder { get; set; }

	    public bool sortOrderSpecified { get; set; }
    }
}
