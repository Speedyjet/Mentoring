﻿using Mentoring.BL;
using Mentoring.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mentoring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsApiController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsApiController> _logger;
        public ProductsApiController(IProductService productService, ILogger<ProductsApiController> logger)
        {
            _productService = productService;
            _logger = logger;
        }
        [HttpGet]
        public string Get()
        {
            return JsonConvert.SerializeObject(_productService.GetProducts().Result);
        }

        [HttpPost]
        public void Create()
        {

        }

        [HttpPut]
        public void Update([FromBody] Product product)
        {

        }

        [HttpDelete]
        public void Delete([FromBody]Product product)
        {

        }
    }
}
