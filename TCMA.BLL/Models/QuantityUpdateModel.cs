using System.ComponentModel.DataAnnotations;

namespace TCMA.BLL.Models
{
    public class QuantityUpdateModel
    {
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a positive integer.")]
        public int Quantity { get; set; }
    }
}
