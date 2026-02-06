namespace WebONNXTest.Services;

using Microsoft.Extensions.Hosting;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using Microsoft.ML.Tokenizers;
using System.Text;

/// <summary>
/// ONNX模型推理服务（单例）
/// </summary>
public class OnnxModelService : IDisposable
{
    //// ONNX运行时会话（核心对象，单例复用）
    //private readonly InferenceSession _inferenceSession;
    //// 分词器（适配NLP模型，若你的模型是CV/其他类型可删除）
    //private readonly Tokenizer _tokenizer;
    //// 模型文件路径
    //private readonly string _modelPath;

    //// 构造函数：加载模型和分词器
    //public OnnxModelService(IHostEnvironment env)
    //{


    //    // 1. 获取模型文件绝对路径（适配开发/部署环境）
    //    _modelPath = Path.Combine(env.ContentRootPath, "models", "model.onnx");
    //    if (!File.Exists(_modelPath))
    //    {
    //        throw new FileNotFoundException("ONNX模型文件未找到，请检查路径：" + _modelPath);
    //    }

    //    try
    //    {
    //        // 2. 初始化ONNX推理会话（单例，减少资源消耗）
    //        var sessionOptions = new SessionOptions();
    //        // 根据你的硬件选择：CPU（默认）/GPU（需安装OnnxRuntime-GPU包）
    //        // sessionOptions.AppendExecutionProvider_CUDA(0); // 启用GPU（需额外安装对应版本的OnnxRuntime-GPU）
    //        _inferenceSession = new InferenceSession(_modelPath, sessionOptions);

    //        // 3. 初始化分词器（以BERT类模型为例，根据你的模型调整）
    //        // 若你的模型是CV/非NLP类型，删除以下分词器代码
    //       // _tokenizer = new Tokenizer(BertModel.GetPreTrained("bert-base-uncased"));
    //        // 自定义词汇表：若有本地vocab.txt，替换为以下代码
    //        // _tokenizer = new Tokenizer(new Bpe().Load("./wwwroot/models/vocab.txt", "./wwwroot/models/merges.txt"));
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception("模型/分词器初始化失败：" + ex.Message, ex);
    //    }
    //}

    ///// <summary>
    ///// 模型推理核心方法（以NLP文本分类为例，需根据你的模型输入输出调整）
    ///// </summary>
    ///// <param name="inputText">输入文本</param>
    ///// <returns>推理结果</returns>
    //public async Task<string> PredictAsync(string inputText)
    //{
    //    if (string.IsNullOrWhiteSpace(inputText))
    //    {
    //        throw new ArgumentNullException(nameof(inputText), "输入文本不能为空");
    //    }

    //    try
    //    {
    //        // 步骤1：文本分词预处理（NLP模型必备，CV模型替换为图像预处理）
    //        var encoding = _tokenizer.Encode(inputText);
    //        // 构造ONNX模型输入（需与你的模型输入维度/名称完全匹配）
    //        // 示例：BERT模型输入（input_ids, attention_mask, token_type_ids）
    //        var inputIds = new DenseTensor<long>(encoding.Ids.ToArray(), new[] { 1, encoding.Ids.Count });
    //        var attentionMask = new DenseTensor<long>(encoding.AttentionMask.ToArray(), new[] { 1, encoding.AttentionMask.Count });
    //        var tokenTypeIds = new DenseTensor<long>(encoding.TypeIds.ToArray(), new[] { 1, encoding.TypeIds.Count });

    //        // 步骤2：构造ONNX输入参数（key为模型输入名称，需严格匹配）
    //        var inputs = new List<NamedOnnxValue>
    //            {
    //                NamedOnnxValue.CreateFromTensor("input_ids", inputIds),
    //                NamedOnnxValue.CreateFromTensor("attention_mask", attentionMask),
    //                NamedOnnxValue.CreateFromTensor("token_type_ids", tokenTypeIds)
    //            };

    //        // 步骤3：执行模型推理（异步调用，避免阻塞）
    //        using var results = await _inferenceSession.RunAsync(inputs);

    //        // 步骤4：解析推理结果（需与你的模型输出名称/维度匹配）
    //        // 示例：获取分类结果（输出名称为"logits"，需替换为你的模型输出名）
    //        var logits = results.First(r => r.Name == "logits").AsTensor<float>();
    //        // 解析logits为最终结果（示例：取概率最大的类别）
    //        var maxIndex = logits.ArgMax();
    //        var result = $"推理结果：类别{maxIndex}，置信度：{logits[maxIndex]:0.000}";

    //        return result;
    //    }
    //    catch (Exception ex)
    //    {
    //        return $"推理失败：{ex.Message}";
    //    }
    //}

    // 释放资源（单例服务销毁时释放模型会话）
    public void Dispose()
    {
        //_inferenceSession?.Dispose();
        GC.SuppressFinalize(this);
    }
}