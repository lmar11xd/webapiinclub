using Dapper.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIInClub.Utilities
{
    public static class Utility
    {
        public static Response GetExceptionResponse(Exception ex) {
            var _response = new Response();
            _response.IsSuccess = false;
            _response.Message = "Error";
            _response.ErrorMessages = new List<string> { ex.ToString() };
            return _response;
        }
    }
}
