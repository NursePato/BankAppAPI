namespace BankApp.Domain.Models
{
    public class Account
    {
        public int AccountID { get; set; }
        public int UserId { get; set; } //volatile?
        public string Frequency { get; set; }
        public DateTime Created { get; set; }
        public decimal Balance { get; set; }
        public int? AccountTypesID { get; set; }
    }

}
