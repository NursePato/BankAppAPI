using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Data.DTO
{
    public class CreateCustomerDto
    {
        public string Gender { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public DateTime? Birthday { get; set; }
        public string TelephoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public int AccountTypeId { get; set; }
        public decimal InitialDeposit { get; set; }
    }

}
