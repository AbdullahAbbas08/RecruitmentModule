using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RM.Shared.ViewModels
{
    public class GenereicResponse<Entity> where Entity : class
    {
        public Entity Data { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
    }
}
