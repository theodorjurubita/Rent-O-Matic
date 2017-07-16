using System;
using System.Collections.Generic;

namespace Rent_O_Matic.GraphicModels
{
    public class CarBrandStatistics : IComparable<CarBrandStatistics>
    {
        public string Name { get; set; }
        public double Percentace { get; set; }
        public List<CarModelStatistics> CarModelStatistics { get; set; }


        public CarBrandStatistics(string name, double percentage)
        {
            Name = name;
            Percentace = percentage;
        }

        public CarBrandStatistics()
        {

        }

        public int CompareTo(CarBrandStatistics other)
        {
            if (this.Percentace > other.Percentace)
            {
                return -1;
            }
            return this.Percentace == other.Percentace ? 0 : 1;
        }
    }
}