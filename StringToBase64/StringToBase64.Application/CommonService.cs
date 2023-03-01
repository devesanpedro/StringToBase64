using StringToBase64.Application.Models;
using StringToBase64.Application.Services.Interface;
using StringToBase64.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringToBase64.Application
{
    public class CommonService : ICommonService
    {
        public CommonService()
        {

        }

        public ResultModel StringToBase64(string text)
        {
            var result = new ResultModel();
            result.IsSuccessful = false;

            try
            {
                var base64text = StringConverter.Base64Encode(text);

                if (base64text != null)
                {
                    result.IsSuccessful = true;
                    result.Data = base64text;
                    result.Message = "String to Base64 converted successfully.";
                }
            }
            catch (Exception ex)
            {
                result.IsSuccessful = false;
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel Base64ToString(string text)
        {
            var result = new ResultModel();
            result.IsSuccessful = false;

            try
            {
                var stringText = StringConverter.Base64Decode(text);

                if (text != null)
                {
                    result.IsSuccessful = true;
                    result.Data = stringText;
                    result.Message = "String to Base64 converted successfully.";
                }
            }
            catch (Exception ex)
            {
                result.IsSuccessful = false;
                result.Message = ex.Message;
            }

            return result;
        }
    }
}
