using System.ComponentModel.DataAnnotations;

namespace TCMA.BLL.Models
{
    public class ComponentSaveModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string UniqueNumber { get; set; }

        public bool CanAssignQuantity { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
