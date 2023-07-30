using System.ComponentModel.DataAnnotations;

namespace PredmetProjekat.Models.Models
{
    public  class Receipt
    {
        [Key]
        public Guid ReceiptId { get; set; }
        public DateTime Date { get; set; }
        public Register register { get; set; }
        public Account SoldBy { get; set; }
    }
}
