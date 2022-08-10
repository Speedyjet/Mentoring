using Mentoring.BL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        // GET: api/<CategoriesApiController>
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
    }
}
