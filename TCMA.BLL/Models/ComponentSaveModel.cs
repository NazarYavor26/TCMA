namespace TCMA.BLL.Models
{
    public class ComponentSaveModel
    {
        public string Name { get; set; }
        public string UniqueNumber { get; set; }
        public bool CanAssignQuantity { get; set; }
        public int Quantity { get; set; }
    }
}
