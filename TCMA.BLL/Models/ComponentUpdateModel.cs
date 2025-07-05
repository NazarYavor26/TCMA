using System.ComponentModel.DataAnnotations;

namespace TCMA.BLL.Models
{
    public class ComponentUpdateModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string UniqueNumber { get; set; }

        public bool CanAssignQuantity { get; set; }
    }
}
