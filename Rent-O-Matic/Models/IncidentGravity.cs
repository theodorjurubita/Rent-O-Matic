namespace Rent_O_Matic.Models
{
    public class IncidentGravity
    {
        public byte Id { get; set; }

        public string Gravity { get; set; }

        public float CoeficientToIncreasePrice { get; set; }
    }
}