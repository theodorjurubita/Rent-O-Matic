namespace Rent_O_Matic.GraphicModels
{
    public class CarModelStatistics
    {
        public string CarModel { get; set; }
        public double Percentage { get; set; }

        public CarModelStatistics(string carModel, double percentage)
        {
            CarModel = carModel;
            Percentage = percentage;
        }

        public override string ToString()
        {
            var stringFormated = $"['{CarModel}',{Percentage}],";
            return stringFormated;
        }
    }
}