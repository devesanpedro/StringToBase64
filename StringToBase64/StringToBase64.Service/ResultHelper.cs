using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringToBase64.Common
{
    public static class ResultHelper
    {
        public static IActionResult CheckResult(object data, bool deleted = false, bool updatefailed = false)
        {
            try
            {
                if (data != null && deleted)
                {
                    return Ok("Record Deleted");
                }

                if (data == null && updatefailed)
                {
                    return Ok("Update Failed");
                }

                if (data != null && deleted == false)
                {
                    return Ok(data);
                }
                else if (data == null)
                {
                    return OkNullResult(data);
                }
                else
                {
                    return (NotOk());
                }
            }
            catch
            {
                return (NotOk());
            }

        }

        private static IActionResult Ok(object data)
        {
            OkObjectResult okResult = new OkObjectResult(data);
            return okResult;
        }

        private static IActionResult OkNullResult(object data)
        {
            OkObjectResult okResult = new OkObjectResult(data);
            okResult.Value = new object();
            return okResult;
        }

        private static IActionResult NotOk()
        {
            BadRequestResult badResult = new BadRequestResult();
            return badResult;
        }
        private static IActionResult NotFound()
        {
            NotFoundResult notFoundResult = new NotFoundResult();
            return notFoundResult;
        }
    }
}
