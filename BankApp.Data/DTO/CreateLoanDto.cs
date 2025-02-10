namespace BankApp.Data.DTO
{
    public class CreateLoanDto
    {
        public int CustomerID { get; set; } 
        public int AccountID { get; set; } 
        public decimal Amount { get; set; } 
        public int Duration { get; set; } 
        public decimal Payments { get; set; } 
        public string Status { get; set; } 
    }

}
