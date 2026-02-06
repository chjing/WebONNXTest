using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebONNXTest.Models;
using WebONNXTest.Services;

namespace WebONNXTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly OnnxModelService _intentService;
        private readonly CommandExecutorService _commandExecutor;

        public HomeController(OnnxModelService intentService, CommandExecutorService commandExecutor)
        {
            _intentService = intentService;
            _commandExecutor = commandExecutor;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        ///// <summary>
        ///// 人机对话接口：接收自然语言，识别意图并执行命令
        ///// </summary>
        ///// <param name="request">用户对话请求</param>
        ///// <returns>命令执行结果</returns>
        //[HttpPost("chat")]
        //public async Task<ActionResult<DialogResponse>> Chat([FromBody] DialogRequest request)
        //{
        //    try
        //    {
        //        // 1. 验证输入
        //        if (string.IsNullOrWhiteSpace(request.UserText))
        //        {
        //            return BadRequest(new DialogResponse
        //            {
        //                Code = 400,
        //                Message = "用户输入不能为空",
        //                Data = null
        //            });
        //        }

        //        // 2. 识别用户意图
        //     //   var intentResult = await _intentService.RecognizeIntentAsync(request.UserText);

        //        // 3. 执行对应命令
        //        //var commandResult = await _commandExecutor.ExecuteCommandAsync(intentResult);

        //        //// 4. 返回结果
        //        //return Ok(new DialogResponse
        //        //{
        //        //    Code = 200,
        //        //    Message = "对话处理成功",
        //        //    Data = new
        //        //    {
        //        //        UserInput = request.UserText,
        //        //        //IntentResult = intentResult,
        //        //        CommandResult = commandResult
        //        //    }
        //        //});
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new DialogResponse
        //        {
        //            Code = 500,
        //            Message = $"对话处理失败：{ex.Message}",
        //            Data = null
        //        });
        //    }
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
