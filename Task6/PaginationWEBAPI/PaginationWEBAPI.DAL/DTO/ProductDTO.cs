using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaginationWEBAPI.DAL.DTO
{
    public  class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public double PurchasePrice { get; set; }
        public double SalePrice { get; set; }
        public string Description { get; set; }
    }
}
