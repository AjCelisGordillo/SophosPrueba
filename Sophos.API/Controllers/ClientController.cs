using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sophos.API.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sophos.API.Models.DTOs;
using Sophos.API.Persistance;
using Sophos.API.Models;

namespace Sophos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : Controller
    {
        public readonly IUnitOfWork _iUnitOfWork;
        private readonly IMapper _mapper;

        public ClientController(IMapper mapper)
        {
            _iUnitOfWork = new UnitOfWork();
            _mapper = mapper;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IActionResult GetClients()
        {
            var clients = _iUnitOfWork.Clients.GetClients();

            if (clients == null)
            {
                return NotFound();
            }

            var objDto = new List<ClientDTO>();

            foreach (var obj in clients)
            {
                objDto.Add(_mapper.Map<ClientDTO>(obj));
            }

            return Ok(objDto);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("{clientId:int}", Name = "GetClient")]
        public IActionResult GetClient(int clientId)
        {
            var obj = _iUnitOfWork.Clients.GetClientById(clientId);

            if (obj == null)
            {
                return NotFound();
            }

            var objDto = _mapper.Map<ClientDTO>(obj);
            return Ok(objDto);
        }

        [Microsoft.AspNetCore.Mvc.HttpPatch("{clientId:int}", Name = "UpdateClient")]
        public IActionResult UpdateClient(int clientId, [Microsoft.AspNetCore.Mvc.FromBody] ClientDTO clientDto)
        {
            if (clientDto == null || clientId != clientDto.Id)
            {
                return BadRequest();
            }

            var clientObj = _mapper.Map<Client>(clientDto);
            if (!_iUnitOfWork.Clients.UpdateClient(clientObj))
            {
                ModelState.AddModelError("", $"No se ha podido actualizar el cliente {clientObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult CreateClient([Microsoft.AspNetCore.Mvc.FromBody] ClientDTO clientDto)
        {
            if (clientDto == null)
            {
                return BadRequest();
            }

            var clientObj = _mapper.Map<Client>(clientDto);

            if (!_iUnitOfWork.Clients.CreateClient(clientObj))
            {
                ModelState.AddModelError("", $"No se ha podido guardar el cliente {clientObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [Microsoft.AspNetCore.Mvc.HttpDelete("{clientId:int}", Name = "DeleteClient")]
        public IActionResult DeleteClient(int clientId)
        {
            var clientInDb = _iUnitOfWork.Clients.GetClientById(clientId);

            if (clientInDb == null)
            {
                return NotFound();
            }

            if (!_iUnitOfWork.Clients.DeleteClient(clientId))
            {
                ModelState.AddModelError("", $"No se ha podido eliminar el producto {clientInDb.Name}.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
