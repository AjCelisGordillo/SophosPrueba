using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sophos.API.Core;
using Sophos.API.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sophos.API.Models.DTOs;
using Sophos.API.Models;

namespace Sophos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : Controller
    {
        public readonly IUnitOfWork _iUnitOfWork;
        private readonly IMapper _mapper;

        public SaleController(IMapper mapper)
        {
            _iUnitOfWork = new UnitOfWork();
            _mapper = mapper;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IActionResult GetSales()
        {
            var sales = _iUnitOfWork.Sales.GetSales();

            if (sales == null)
            {
                return NotFound();
            }

            var objDto = new List<SaleDTO>();

            foreach (var obj in sales)
            {
                objDto.Add(_mapper.Map<SaleDTO>(obj));
            }

            return Ok(objDto);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("{saleId:int}", Name = "GetSale")]
        public IActionResult GetSale(int saleId)
        {
            var obj = _iUnitOfWork.Sales.GetSaleById(saleId);

            if (obj == null)
            {
                return NotFound();
            }

            var objDto = _mapper.Map<SaleDTO>(obj);
            return Ok(objDto);
        }

        [Microsoft.AspNetCore.Mvc.HttpPatch("{saleId:int}", Name = "UpdateSale")]
        public IActionResult UpdateSale(int saleId, [Microsoft.AspNetCore.Mvc.FromBody] SaleDTO saleDto)
        {
            if (saleDto == null || saleId != saleDto.SaleId)
            {
                return BadRequest();
            }

            var saleObj = _mapper.Map<Sale>(saleDto);
            if (!_iUnitOfWork.Sales.UpdateSale(saleObj))
            {
                ModelState.AddModelError("", $"No se ha podido actualizar la venta {saleObj.SaleId}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult CreateSale([Microsoft.AspNetCore.Mvc.FromBody] SaleDTO saleDto)
        {
            if (saleDto == null)
            {
                return BadRequest();
            }

            var saleObj = _mapper.Map<Sale>(saleDto);

            if (!_iUnitOfWork.Sales.CreateSale(saleObj))
            {
                ModelState.AddModelError("", $"No se ha podido guardar la venta {saleObj.SaleId}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [Microsoft.AspNetCore.Mvc.HttpDelete("{saleId:int}", Name = "DeleteSale")]
        public IActionResult DeleteSale(int saleId)
        {
            var saleInDb = _iUnitOfWork.Sales.GetSaleById(saleId);

            if (saleInDb == null)
            {
                return NotFound();
            }

            if (!_iUnitOfWork.Sales.DeleteSale(saleId))
            {
                ModelState.AddModelError("", $"No se ha podido eliminar el producto {saleInDb.SaleId}.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
