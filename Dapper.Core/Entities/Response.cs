using System.Collections.Generic;

namespace Dapper.Core.Entities
{
    public class Response
    {
        public bool IsSuccess { get; set; } = true;
        public object Result { get; set; }
        public string Message { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}
