using Model.Enums;
using System;
using System.Windows.Media;

namespace Model.DataFromView
{
    public class PostDataFromView
    {
        public string ID { get; set; }
        public bool Marked { get; set; }
        public int TripMinValue { get; set; }

        public int TripMaxValue { get; set; }

        public DateTime availFrom { get; set; }

        public DateTime availTo { get; set; }

        public StateProvince originState { get; set; }

        public StateProvince destinationState { get; set; }

        public int DHO { get; set; }

        public int DHD { get; set; }

        public string cityDestination { get; set; }
    
        public string cityOrigin { get; set; }

        public EquipmentType equipmentType { get; set; }

        public FullOrPartial fullOrPartial { get; set; }

        public int length { get; set; }

        public int weight { get; set; }

        public string commentOne { get; set; }

        public string commentTwo { get; set; }

        public Color backgroundColor { get; set; }

        public Color foregroundColor { get; set; }

        public bool includeFulls { get; set; }

        public bool includeLtls { get; set; }

        public Alarm alarm { get; set; }

        public PostDataFromView()
        {
            availFrom = DateTime.Now;
            availTo = DateTime.Now;
            includeFulls = true;
            includeLtls = false;
            backgroundColor = Color.FromRgb(255, 255, 255);
            foregroundColor = Color.FromRgb(0, 0, 0);
        }

        public string Trip
        {
            get
            {
                if (TripMinValue < 0 || TripMaxValue < 1)
                    return "-";
                return string.Format("Trip: {0}-{1}", TripMinValue, TripMaxValue);
            }
        }

        public string Origin
        {
            get
            {
                return originState.ToString();
            }
        }

        public string Destination
        {
            get
            {
                return destinationState.ToString();
            }
        }

        public string Pickup
        {
            get
            {
                return String.Format("{0:MM/dd}/{1:MM/dd}", availFrom, availTo);
            }
        }

        public string DHOToString
        {
            get
            {
                if (DHO < 0)
                    return "-";
                return DHO.ToString();
            }
        }

        public string DHDToString
        {
            get
            {
                if (DHD < 0)
                    return "-";
                return DHD.ToString();
            }
        }

        public string EquipmentType
        {
            get
            {
                return equipmentType.ToString();
            }
        }

        public string Length
        {
            get
            {
                return length.ToString();
            }
        }

        public string Weight
        {
            get
            {
                return weight.ToString();
            }
        }

        public string AlarmState
        {
            get
            {
                if (alarm != null)
                    return "Active";
                else
                    return "Inactive";
            }
        }

        public string FullOrPartial
        {
            get
            {
                return fullOrPartial.ToString();
            }
        }
        public Brush BackgroundColor
        {
            get
            {
                return new SolidColorBrush(backgroundColor);
            }
        }

        public Brush ForegroundColor
        {
            get
            {
                return new SolidColorBrush(foregroundColor);
            }
        }

    }
}
