using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaginationWEBAPI.DAL.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int Amount { get; set; }
        [Required]
        public double PurchasePrice { get; set; }
        [Required]
        public double SalePrice { get; set; }
        [Required]
        [MaxLength(250)]
        public string Description { get; set; } = string.Empty;
    }
}
