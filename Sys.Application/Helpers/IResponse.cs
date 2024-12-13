using SysCapteur.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.Helpers
{
    public interface IResponse<T>
    {
        bool Success { get; set; }
        T Data { get; set; }
        CustomException Error { get; set; }
        int StatusCode { get; set; }
    }
}
