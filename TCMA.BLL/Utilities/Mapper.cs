using TCMA.BLL.Models;
using TCMA.DAL.Entities;

namespace TCMA.BLL.Utilities
{
    public static class Mapper
    {
        public static ComponentModel ToModel(this Component entitiy)
        {
            if (entitiy == null) return null;

            return new ComponentModel
            {
                Id = entitiy.Id,
                Name = entitiy.Name,
                UniqueNumber = entitiy.UniqueNumber,
                CanAssignQuantity = entitiy.CanAssignQuantity,
                Quantity = entitiy.Quantity,
            };
        }

        public static Component ToEntity(this ComponentModel model)
        {
            if (model == null) return null;

            return new Component
            {
                Id = model.Id,
                Name = model.Name,
                UniqueNumber = model.UniqueNumber,
                CanAssignQuantity = model.CanAssignQuantity,
                Quantity = model.Quantity,
            };
        }
    }
}
