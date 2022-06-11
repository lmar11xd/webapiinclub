using APIInClub.Utilities;
using Dapper.Application.Interfaces;
using Dapper.Core.Entities;
using Dapper.Core.Entities.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace APIInClub.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        protected Response _response;

        public OrderController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            _response = new Response();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await unitOfWork.Orders.GetAllAsync();

                _response.Result = data;
                _response.Message = "Lista de Ordenes de Compra";

                return Ok(_response);
            }
            catch (Exception ex)
            {
                return BadRequest(Utility.GetExceptionResponse(ex));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var data = await unitOfWork.Orders.GetByIdAsync(id);

                if (data == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Orden no encontrada";
                    return NotFound(_response);
                }
                    
                _response.Result = data;
                _response.Message = "Información de la Orden de Compra";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                return BadRequest(Utility.GetExceptionResponse(ex));
            }
        }

        [HttpGet("User/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            try
            {
                var data = await unitOfWork.Orders.GetByUserIdAsync(userId);
                _response.Result = data;
                _response.Message = "Información de la Orden de Compra";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                return BadRequest(Utility.GetExceptionResponse(ex));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(OrderDto orderDto)
        {
            try
            {
                var data = await unitOfWork.Orders.AddUpdateAsync(orderDto);

                if (data == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "La Orden de Compra no fue agregada";
                    return BadRequest(_response);
                }

                _response.Result = data;
                _response.Message = "Orden de Compra agregada";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                return BadRequest(Utility.GetExceptionResponse(ex));
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(OrderDto orderDto)
        {
            try
            {
                var data = await unitOfWork.Orders.AddUpdateAsync(orderDto);

                if (data == null) {
                    _response.IsSuccess = false;
                    _response.Message = "La Orden de Compra no fue actualizada";
                    return BadRequest(_response);
                }

                _response.Result = data;
                _response.Message = "Orden de Compra actualizada";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                return BadRequest(Utility.GetExceptionResponse(ex));
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var data = await unitOfWork.Orders.DeleteAsync(id);
                _response.Result = data;
                _response.Message = "Orden de Compra eliminada";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                return BadRequest(Utility.GetExceptionResponse(ex));
            }
        }
    }
}
