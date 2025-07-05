using Microsoft.EntityFrameworkCore;
using TCMA.BLL.Enums;
using TCMA.BLL.Models;
using TCMA.BLL.Utilities;
using TCMA.DAL.Entities;
using TCMA.DAL.Repositories;

namespace TCMA.BLL.Services
{
    public class ComponentService : IComponentService
    {
        private readonly IComponentRepository _componentRepository;

        public ComponentService(IComponentRepository componentRepository)
        {
            _componentRepository = componentRepository;
        }

        public async Task<PagedResult<ComponentGetModel>> GetAllAsync(ComponentFilterModel filter)
        {
            var query = _componentRepository.GetQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                query = query.Where(c =>
                c.Name.Contains(filter.Search) ||
                c.UniqueNumber.Contains(filter.Search));
            }

            query = SortComponentQuery(query, filter);
            int componentsCount = await query.CountAsync();

            var components = await query
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(c => c.ToModel())
                .ToListAsync();

            return new PagedResult<ComponentGetModel>
            {
                Items = components,
                Count = componentsCount,
                Page = filter.Page,
                PageSize = filter.PageSize
            };
        }

        public async Task<ComponentGetModel> GetByIdAsync(int id)
        {
            var component = await _componentRepository.GetByIdAsync(id);

            if (component == null)
            {
                throw new KeyNotFoundException($"Component with id {id} not found.");
            }

            return component.ToModel();
        }

        public async Task<ComponentGetModel> CreateAsync(ComponentCreateModel componentCreate)
        {
            var existingComponent = await _componentRepository.GetByUniqueNumberAsync(componentCreate.UniqueNumber);

            if (existingComponent != null)
            {
                throw new InvalidOperationException($"Component with UniqueNumber {componentCreate.UniqueNumber} already exists.");
            }

            ValidateQuantityAssignment(componentCreate.CanAssignQuantity, componentCreate.Quantity);

            var createdComponent = await _componentRepository.CreateAsync(componentCreate.ToEntity());
            return createdComponent.ToModel();
        }

        public async Task<ComponentGetModel> UpdateAsync(int componentId, ComponentUpdateModel componentUpdate)
        {
            var existingСomponent = await _componentRepository.GetByIdAsync(componentId);

            if (existingСomponent == null)
            {
                throw new KeyNotFoundException($"Component with id {componentId} not found.");
            }

            if (existingСomponent.UniqueNumber != componentUpdate.UniqueNumber)
            {
                var duplicate = await _componentRepository.GetByUniqueNumberAsync(componentUpdate.UniqueNumber);

                if (duplicate != null)
                {
                    throw new InvalidOperationException($"Component with UniqueNumber {componentUpdate.UniqueNumber} already exists.");
                }
            }

            var entityComponent = componentUpdate.ToEntity();

            if (!componentUpdate.CanAssignQuantity)
            {
                entityComponent.Quantity = null;
            }

            var updatedComponent = await _componentRepository.UpdateAsync(componentId, entityComponent);
            return updatedComponent.ToModel();
        }

        public async Task<ComponentGetModel> UpdateQuantityAsync(int componentId, QuantityUpdateModel quantityUpdate)
        {
            var existingComponent = await _componentRepository.GetByIdAsync(componentId);

            if (existingComponent == null)
            {
                throw new KeyNotFoundException($"Component with id {componentId} not found.");
            }

            if (!existingComponent.CanAssignQuantity)
            {
                throw new InvalidOperationException($"Cannot assign quantity to this component.");
            }

            ValidateQuantityAssignment(true, quantityUpdate.Quantity);

            existingComponent.Quantity = quantityUpdate.Quantity;
            var updatedComponent = await _componentRepository.UpdateAsync(componentId, existingComponent);

            return updatedComponent.ToModel();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _componentRepository.DeleteAsync(id);
        }

        private IQueryable<Component> SortComponentQuery(IQueryable<Component> query, ComponentFilterModel filter)
        {
            return filter.SortBy switch
            {
                ComponentSortField.Name => filter.IsDescending
                ? query.OrderByDescending(c => c.Name)
                : query.OrderBy(c => c.Name),

                ComponentSortField.UniqueNumber => filter.IsDescending
                ? query.OrderByDescending(c => c.UniqueNumber)
                : query.OrderBy(c => c.UniqueNumber),

                _ => filter.IsDescending
                ? query.OrderByDescending(c => c.Id)
                : query.OrderBy(c => c.Id)
            };
        }

        private void ValidateQuantityAssignment(bool canAssignQuantity, int? quantity)
        {
            if (!canAssignQuantity && quantity != null)
            {
                throw new InvalidOperationException("Quantity must be null when CanAssignQuantity is false.");
            }

            if (canAssignQuantity && quantity != null)
            {
                if (int.IsNegative(quantity.Value))
                {
                    throw new InvalidOperationException("Quantity must be a positive integer.");
                }
            }
        }
    }
}
