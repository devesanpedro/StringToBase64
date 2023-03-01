using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringToBase64.Application.Models
{
    public class ResultModel
    {
        public bool IsSuccessful { get; set; }
        public object Data { get; set; }
        public object Message { get; set; }
        public int Code { get; set; }
    }
}
