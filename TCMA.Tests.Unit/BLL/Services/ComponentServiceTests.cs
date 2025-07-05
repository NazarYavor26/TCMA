using Moq;
using TCMA.BLL.Models;
using TCMA.BLL.Services;
using TCMA.DAL.Entities;
using TCMA.DAL.Repositories;

namespace TCMA.Tests.Unit.BLL.Services
{
    [TestFixture]
    public class ComponentServiceTests
    {
        private Mock<IComponentRepository> _repoMock;
        private ComponentService _service;

        [SetUp]
        public void Setup()
        {
            _repoMock = new Mock<IComponentRepository>(MockBehavior.Strict);
            _service = new ComponentService(_repoMock.Object);
        }

        [Test]
        public async Task GetByIdAsync_ExistingId_ReturnsComponent()
        {
            // Arrange
            var component = new Component { Id = 1, Name = "Wheel", UniqueNumber = "U1" };
            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(component);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("Wheel", result.Name);
        }

        [Test]
        public void GetByIdAsync_NotFound_ThrowsException()
        {
            // Arrange
            _repoMock.Setup(r => r.GetByIdAsync(10)).ReturnsAsync((Component)null);

            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetByIdAsync(10));
        }

        [Test]
        public void CreateAsync_WithDuplicateUniqueNumber_Throws()
        {
            // Arrange
            var model = new ComponentCreateModel { UniqueNumber = "exist", CanAssignQuantity = false };
            _repoMock.Setup(r => r.GetByUniqueNumberAsync("exist")).ReturnsAsync(new Component());

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => _service.CreateAsync(model));
        }

        [Test]
        public void CreateAsync_InvalidQuantityAssignment_Throws()
        {
            // Arrange
            var model = new ComponentCreateModel
            {
                Name = "testName",
                UniqueNumber = "testNumber",
                CanAssignQuantity = false,
                Quantity = 5
            };

            _repoMock.Setup(r => r.GetByUniqueNumberAsync(model.UniqueNumber)).ReturnsAsync((Component)null);

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => _service.CreateAsync(model));
        }

        [Test]
        public async Task UpdateAsync_CanAssignQuantityFalse_SetsQuantityToNull()
        {
            // Arrange
            var existing = new Component { Id = 1, UniqueNumber = "testNumber", Quantity = 10 };

            var updateModel = new ComponentUpdateModel
            {
                Name = "updatedName",
                UniqueNumber = "updatedNumber",
                CanAssignQuantity = false
            };

            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existing);
            _repoMock.Setup(r => r.GetByUniqueNumberAsync("updatedNumber")).ReturnsAsync((Component)null);
            _repoMock.Setup(r => r.UpdateAsync(1, It.IsAny<Component>())).ReturnsAsync((int id, Component c) => c);

            // Act
            var result = await _service.UpdateAsync(1, updateModel);

            // Assert
            Assert.IsNull(result.Quantity);
        }

        [Test]
        public async Task UpdateQuantityAsync_Valid_ReturnsUpdated()
        {
            // Arrange
            var component = new Component { Id = 1, CanAssignQuantity = true };
            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(component);

            _repoMock.Setup(r => r.UpdateAsync(1, It.IsAny<Component>()))
                .ReturnsAsync((int id, Component c) => c);

            var updateModel = new QuantityUpdateModel { Quantity = 5 };

            // Act
            var result = await _service.UpdateQuantityAsync(1, updateModel);

            // Assert
            Assert.AreEqual(5, result.Quantity);
        }

        [Test]
        public void UpdateQuantityAsync_WhenCantAssignQuantity_Throws()
        {
            // Arrange
            var component = new Component { Id = 1, CanAssignQuantity = false };
            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(component);
            var updateModel = new QuantityUpdateModel { Quantity = 5 };

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => _service.UpdateQuantityAsync(1, updateModel));
        }

        [Test]
        public async Task DeleteAsync_ReturnsTrue()
        {
            // Arrange
            _repoMock.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _service.DeleteAsync(1);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
