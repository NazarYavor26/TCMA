using System.ComponentModel.DataAnnotations;

namespace TCMA.BLL.Models
{
    public class ComponentCreateModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string UniqueNumber { get; set; }

        public bool CanAssignQuantity { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a positive integer.")]
        public int? Quantity { get; set; }
    }
}
