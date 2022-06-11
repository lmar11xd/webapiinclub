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
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        protected Response _response;

        public UserController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            _response = new Response();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await unitOfWork.Users.GetAllAsync();

                _response.Result = data;
                _response.Message = "Lista de Usuarios";

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
                var data = await unitOfWork.Users.GetByIdAsync(id);
                if (data == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Usuario no encontrado";
                    return NotFound(_response);
                }
                    
                _response.Result = data;
                _response.Message = "Información del Usuario";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                return BadRequest(Utility.GetExceptionResponse(ex));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserDto userDto)
        {
            try
            {
                var data = await unitOfWork.Users.AddAsync(new User { Username = userDto.Username}, userDto.Password);

                if (data == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Usuario no agregado";
                    return BadRequest(_response);
                }

                _response.Result = data;
                _response.Message = "Usuario agregado";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                return BadRequest(Utility.GetExceptionResponse(ex));
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserDto userDto)
        {
            try
            {
                //Este método solo actualiza el campo Username
                var data = await unitOfWork.Users.UpdateAsync(new User { Id = userDto.Id, Username = userDto.Username});

                if (data == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Usuario no agregado";
                    return BadRequest(_response);
                }

                _response.Result = data;
                _response.Message = "Usuario Actualizado";
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
                var data = await unitOfWork.Users.DeleteAsync(id);
                _response.Result = data;
                _response.Message = "Usuario Eliminado";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                return BadRequest(Utility.GetExceptionResponse(ex));
            }
        }
    }
}
