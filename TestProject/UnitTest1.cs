using Mentoring.BL;
using Mentoring.Controllers;
using Mentoring.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestProject
{
    public class UnitTest1
    {
        [Fact]
        public async Task Index_ReturnsAView()
        {
            var mockRepo = new Mock<IBusinessLogic>();
            
            var testCategory = new Category()
            {
                CategoryId = 1,
                CategoryName = "Test",
                Description = "Test",
                Picture = null,
                Products = new List<Product>(),
            };
            mockRepo.Setup(x => x.GetCategories()).ReturnsAsync(() => {
                return new List<Category>() { testCategory };
            });
            var controller = new CategoriesController(mockRepo.Object);

            //Act
            var result = await controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }
    }
}