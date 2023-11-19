using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PredmetProjekat.Models.Models
{
    public class Receipt
    {
        [Key]
        public Guid ReceiptId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public Register Register { get; set; }
        [Required]
        public Account SoldBy { get; set; }
        [Required]
        public IEnumerable<SoldProduct> SoldProducts { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal TotalPrice { get; set; }
    }
}
