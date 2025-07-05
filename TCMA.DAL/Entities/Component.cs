using System.ComponentModel.DataAnnotations;

namespace TCMA.DAL.Entities
{
    public class Component
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string UniqueNumber { get; set; }

        public bool CanAssignQuantity { get; set; }

        [Range(0, int.MaxValue)]
        public int? Quantity { get; set; }
    }
}
