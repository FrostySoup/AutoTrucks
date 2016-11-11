using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ReceiveData.AlarmMatch
{
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.tcore.com/TfmiFreightMatching.xsd")]
    public class MyAsset
    {
        private string assetIdField;

        private FmeStatus statusField;

        private BaseAsset itemField;

        private string postersReferenceIdField;

        private bool ltlField;

        private int countField;

        private Dimensions dimensionsField;

        private int stopsField;

        private Availability availabilityField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string assetId
        {
            get
            {
                return this.assetIdField;
            }
            set
            {
                this.assetIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public FmeStatus status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("equipment", typeof(Equipment), Order = 2)]
        [System.Xml.Serialization.XmlElementAttribute("shipment", typeof(Shipment), Order = 2)]
        public BaseAsset Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public string postersReferenceId
        {
            get
            {
                return this.postersReferenceIdField;
            }
            set
            {
                this.postersReferenceIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public bool ltl
        {
            get
            {
                return this.ltlField;
            }
            set
            {
                this.ltlField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public int count
        {
            get
            {
                return this.countField;
            }
            set
            {
                this.countField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        public Dimensions dimensions
        {
            get
            {
                return this.dimensionsField;
            }
            set
            {
                this.dimensionsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
        public int stops
        {
            get
            {
                return this.stopsField;
            }
            set
            {
                this.stopsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
        public Availability availability
        {
            get
            {
                return this.availabilityField;
            }
            set
            {
                this.availabilityField = value;
            }
        }
    }
}
