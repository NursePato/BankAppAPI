namespace BankApp.Domain.Models
{
    public class Disposition
    {
        public int DispositionID { get; set; }
        public int CustomerID { get; set; }
        public int AccountID { get; set; }
        public string Type { get; set; }
    }

}
