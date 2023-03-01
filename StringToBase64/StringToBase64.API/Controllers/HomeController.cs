using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using StringToBase64.Application.Models;
using StringToBase64.Application.Services.Interface;
using StringToBase64.Common;

namespace StringToBase64.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : BaseController<HomeController>
    {
        private readonly ICommonService _commonService;

        //change port number based on server port
        private readonly string signalRServer = "https://localhost:7252/mainHub";

        public HomeController(ICommonService commonService)
        {
            _commonService = commonService;
        }

        [HttpPost]
        [Route("StringToBase64")]
        public async Task<IActionResult> StringToBase64([FromBody] InputModel model)
        {
            var results = _commonService.StringToBase64(model.text);

            char[] splitBase64String = results.Data.ToString().ToCharArray();

            await using var connection = new HubConnectionBuilder().WithUrl(signalRServer).Build();

            await connection.StartAsync();

            for (int i = 0; i < splitBase64String.Length; i++)
            {
                await connection.InvokeAsync("SendMessage", splitBase64String[i].ToString());
            }

            await connection.StopAsync();

            return ResultHelper.CheckResult(results);
        }

        //static void BusinessComponentDeliverResponse(object sender, DeliverEventArgs e)
        //{
        //    if (e.IsValid)
        //        _result = Ok("received");
        //    else
        //        _result = BadRequest(e.ErrorMessage);
        //}

        [HttpPost]
        [Route("Base64ToString")]
        public async Task<IActionResult> Base64ToString([FromBody] InputModel model)
        {
            var results = _commonService.StringToBase64(model.text);

            return ResultHelper.CheckResult(results);
        }
    }
}
