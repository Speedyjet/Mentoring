using Mentoring.BL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mentoring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesApiController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoriesController> _logger;
        public CategoriesApiController(ICategoryService categoryService, ILogger<CategoriesController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }
        /// <summary>
        /// Returns the list of products
        /// </summary>
        /// <returns>the list of products</returns>
        [HttpGet]
        public string Get()
        {
            _logger.LogInformation("Getting the list of products");
            return JsonConvert.SerializeObject(_categoryService.GetCategories().Result);
        }

        // GET api/<CategoriesApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            _logger.LogInformation("Getting product information by id");
            return JsonConvert.SerializeObject(_categoryService.GetCategory(id).Result);
        }

        [HttpGet]
        public string GetImage(int id) => _categoryService.GetImageById(id).Result.ToString() ?? String.Empty;

        [HttpPost]
        public void UpdateImage([FromBody]byte[] image, int id)
        {
            _categoryService.UpdateImage(image, id);
        }
    }
}
