using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sophos.API.Core;
using Sophos.API.Models.DTOs;
using Sophos.API.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Sophos.API.Models;
using IMapper = AutoMapper.IMapper;

namespace Sophos.API.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        public readonly IUnitOfWork _iUnitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IMapper mapper)
        {
            _iUnitOfWork = new UnitOfWork();
            _mapper = mapper;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IActionResult GetProducts()
        {
            var Products = _iUnitOfWork.Products.GetProducts();

            if(Products == null)
            {
                return NotFound();
            }

            var objDto = new List<ProductDTO>();

            foreach (var obj in Products)
            {
                objDto.Add(_mapper.Map<ProductDTO>(obj));
            }

            return Ok(objDto);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("{productId:int}", Name= "GetProduct")]
        public IActionResult GetProduct(int productId)
        {
            var obj = _iUnitOfWork.Products.GetProductById(productId);

            if (obj == null)
            {
                return NotFound();
            }

            var objDto = _mapper.Map<ProductDTO>(obj);
            return Ok(objDto);
        }

        [Microsoft.AspNetCore.Mvc.HttpPatch("{productId:int}", Name = "UpdateProduct")]
        public IActionResult UpdateProduct(int productId, [Microsoft.AspNetCore.Mvc.FromBody] ProductDTO productDto)
        {
            if (productDto == null || productId != productDto.ProductId)
            {
                return BadRequest();
            }

            var productObj = _mapper.Map<Product>(productDto);
            if (!_iUnitOfWork.Products.UpdateProduct(productObj))
            {
                ModelState.AddModelError("", $"No se ha podido actualizar el producto {productObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult CreateProduct([Microsoft.AspNetCore.Mvc.FromBody] ProductDTO productDto)
        {
            if (productDto == null)
            {
                return BadRequest();
            }

            var productObj = _mapper.Map<Product>(productDto);

            if (!_iUnitOfWork.Products.CreateProduct(productObj))
            {
                ModelState.AddModelError("", $"No se ha podido guardar el producto {productObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [Microsoft.AspNetCore.Mvc.HttpDelete("{productId:int}", Name = "DeleteProduct")]
        public IActionResult DeleteProduct(int productId)
        {
            var productInDb = _iUnitOfWork.Products.GetProductById(productId);

            if (productInDb == null)
            {
                return NotFound();
            }

            if (!_iUnitOfWork.Products.DeleteProduct(productId))
            {
                ModelState.AddModelError("", $"No se ha podido eliminar el producto {productInDb.Name}.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
