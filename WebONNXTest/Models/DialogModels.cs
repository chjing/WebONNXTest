namespace WebONNXTest.Models;

/// <summary>
/// 用户对话请求
/// </summary>
public class DialogRequest
{
    /// <summary>
    /// 用户输入的自然语言文本
    /// </summary>
    public string UserText { get; set; } = string.Empty;
}

/// <summary>
/// 模型推理后的意图结果
/// </summary>
public class IntentResult
{
    /// <summary>
    /// 识别出的用户意图（如ExportReport、QueryRecord、ModifyParam）
    /// </summary>
    public string Intent { get; set; } = string.Empty;

    /// <summary>
    /// 意图置信度（0-1）
    /// </summary>
    public float Confidence { get; set; }

    /// <summary>
    /// 提取的命令参数（如时间、类型、ID等）
    /// </summary>
    public Dictionary<string, string> Params { get; set; } = new();
}

/// <summary>
/// 对话接口返回结果
/// </summary>
public class DialogResponse
{
    public int Code { get; set; } = 200;
    public string Message { get; set; } = "操作成功";
    public object? Data { get; set; }
}