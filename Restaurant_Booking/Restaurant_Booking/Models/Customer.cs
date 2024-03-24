using System.ComponentModel.DataAnnotations;

namespace Restaurant_Booking.Models
{
    public class Customer
    {
        [Key]
        public int Customer_Id { get; set; }
        public string? Customer_Name { get; set;}
        public string? Password { get; set; }

        public int Confirm_Password { get; set; }
        public string? Customer_Email { get; set;}
        public string? Customer_PhoneNo { get; set;}

      }
}
