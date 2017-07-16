using Rent_O_Matic.GraphicModels;
using System.Collections.Generic;

namespace Rent_O_Matic.ViewModels
{
    public class HomeCarStatisticsViewModel
    {
        public List<RentedCarsViewModel> RentedCarsViewModel { get; set; }
        public List<CarBrandStatistics> CarBrandStatistics { get; set; }
    }
}