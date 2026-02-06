namespace WebONNXTest.Services;



using WebONNXTest.Models;
using WebONNXTest.Models;


/// <summary>
/// 对话命令执行服务（解耦意图识别与业务逻辑）
/// </summary>
public class CommandExecutorService
{
    /// <summary>
    /// 执行识别后的命令
    /// </summary>
    /// <param name="intentResult">模型识别的意图结果</param>
    /// <returns>命令执行结果</returns>
    public async Task<object> ExecuteCommandAsync(IntentResult intentResult)
    {
        return intentResult.Intent switch
        {
            // 场景1：导出废钢验质报告
            "ExportReport" => await ExportWasteSteelReportAsync(intentResult.Params),
            // 场景2：查询废钢验质记录
            "QueryRecord" => await QueryWasteSteelRecordAsync(intentResult.Params),
            // 场景3：修改系统参数（示例）
            "ModifyParam" => await ModifySystemParamAsync(intentResult.Params),
            // 未知意图
            _ => new { Message = $"暂不支持执行「{intentResult.Intent}」命令", Intent = intentResult.Intent }
        };
    }

    #region 具体业务命令实现（示例）
    /// <summary>
    /// 导出废钢验质报告
    /// </summary>
    private async Task<object> ExportWasteSteelReportAsync(Dictionary<string, string> param)
    {
        // 提取参数（如时间范围）
        var date = param.ContainsKey("Date") ? param["Date"] : "今日";

        // 模拟导出逻辑（实际可替换为文件生成、数据库查询等）
        await Task.Delay(500); // 模拟异步操作
        return new
        {
            Command = "ExportReport",
            Status = "成功",
            Message = $"已导出{date}的废钢验质报告",
            ReportUrl = $"/reports/waste_steel_{DateTime.Now:yyyyMMdd}.xlsx"
        };
    }

    /// <summary>
    /// 查询废钢验质记录
    /// </summary>
    private async Task<object> QueryWasteSteelRecordAsync(Dictionary<string, string> param)
    {
        // 提取参数（如记录ID、时间）
        var recordId = param.ContainsKey("RecordId") ? param["RecordId"] : "all";
        var date = param.ContainsKey("Date") ? param["Date"] : DateTime.Today.ToString("yyyy-MM-dd");

        // 模拟数据库查询
        await Task.Delay(300);
        return new
        {
            Command = "QueryRecord",
            Status = "成功",
            QueryCondition = new { RecordId = recordId, Date = date },
            Data = new List<object>
            {
                new { Id = "1001", Quality = "优质", Score = 95.2, CreateTime = $"{date} 08:30" },
                new { Id = "1002", Quality = "普通", Score = 82.5, CreateTime = $"{date} 09:15" }
            }
        };
    }

    /// <summary>
    /// 修改系统参数
    /// </summary>
    private async Task<object> ModifySystemParamAsync(Dictionary<string, string> param)
    {
        if (!param.ContainsKey("ParamName") || !param.ContainsKey("Value"))
        {
            return new { Command = "ModifyParam", Status = "失败", Message = "缺少参数：ParamName/Value" };
        }

        // 模拟参数修改
        await Task.Delay(200);
        return new
        {
            Command = "ModifyParam",
            Status = "成功",
            ModifiedParam = new { Name = param["ParamName"], NewValue = param["Value"] }
        };
    }
    #endregion
}