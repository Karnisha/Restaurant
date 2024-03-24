using System.ComponentModel.DataAnnotations;

namespace Restaurant_Booking.Models
{
    public class Reservation
    {
        [Key]
        public int ReqId { get; set; }
       
        public string? Date { get; set; }
        
        public string? Time { get; set; }


        public int NoOfTables {  get; set; }
        
      

    }
}
