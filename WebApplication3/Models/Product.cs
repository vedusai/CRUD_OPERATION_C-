using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; } = default!;
        public int CategoryId { get; set; }


        [ForeignKey("CategoryId")]
        public Category Category { get; set; } = default!;
    }
}
