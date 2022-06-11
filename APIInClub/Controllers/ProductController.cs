using APIInClub.Utilities;
using Dapper.Application.Interfaces;
using Dapper.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIInClub.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        protected Response _response;

        public ProductController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            _response = new Response();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await unitOfWork.Products.GetAllAsync();

                _response.Result = data;
                _response.Message = "Lista de Productos";

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
                var data = await unitOfWork.Products.GetByIdAsync(id);
                if (data == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Producto no encontrado";
                    return NotFound(_response);
                }
                    
                _response.Result = data;
                _response.Message = "Información del Producto";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                return BadRequest(Utility.GetExceptionResponse(ex));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            try
            {
                var data = await unitOfWork.Products.AddAsync(product);
                _response.Result = data;
                _response.Message = "Producto agregado";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                return BadRequest(Utility.GetExceptionResponse(ex));
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(Product product)
        {
            try
            {
                var data = await unitOfWork.Products.UpdateAsync(product);
                _response.Result = data;
                _response.Message = "Producto Actualizado";
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
                var data = await unitOfWork.Products.DeleteAsync(id);
                _response.Result = data;
                _response.Message = "Producto Eliminado";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                return BadRequest(Utility.GetExceptionResponse(ex));
            }
        }
    }
}
