namespace BankApp.Domain.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int AccountID { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Operation { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
    }

}
