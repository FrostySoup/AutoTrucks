using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.FillDataFactory
{
    public class FillDataFactory
    {
        public ObservableCollection<Truck> GenerateTrucks()
        {
            ObservableCollection<Truck> trucks = new ObservableCollection<Truck>();
            trucks.Add(new Truck
            {
                Name = "Truck1",
                Number = 5
            });
            trucks.Add(new Truck
            {
                Name = "Trucky",
                Number = 25
            });
            trucks.Add(new Truck
            {
                Name = "Trucktour",
                Number = 35445
            });

            return trucks;
        }
    }
}
