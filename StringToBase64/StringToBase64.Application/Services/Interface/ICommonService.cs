using StringToBase64.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringToBase64.Application.Services.Interface
{
    public interface ICommonService
    {
        ResultModel StringToBase64(string text);
    }
}
