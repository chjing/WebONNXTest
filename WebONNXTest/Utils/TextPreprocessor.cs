//namespace WebONNXTest.Utils;


//using Microsoft.ML.Tokenizers;
//using Microsoft.ML.OnnxRuntime.Tensors;
//using System.Reflection;


//using System.Text;


///// <summary>
///// 对话文本预处理工具（适配BERT类ONNX模型）
///// 版本适配：Microsoft.ML.Tokenizers 2.0.0
///// </summary>
//public static class TextPreprocessor
//{
//    // 预训练分词器（bert-base-chinese）
//    private static readonly Tokenizer _tokenizer;

//    // 模型输入序列长度（需和训练时一致）
//    private const int MaxSequenceLength = 128;

//    /// <summary>
//    /// 静态构造函数：初始化分词器（2.0.0版本专用）
//    /// </summary>
//    static TextPreprocessor()
//    {
//        try
//        {
//            // 1. 获取vocab.txt路径（必须提前下载放到该目录）
//            var vocabPath = Path.Combine(
//                AppContext.BaseDirectory,  // 替代Assembly方式，更稳定
//                "wwwroot", "tokenizer", "vocab.txt");

//            // 2. 加载词汇表（2.0.0版本必须手动读取文件内容）
//            if (!File.Exists(vocabPath))
//            {
//                throw new FileNotFoundException("请将bert-base-chinese的vocab.txt放入wwwroot/tokenizer/目录", vocabPath);
//            }
//            var vocabContent = File.ReadAllText(vocabPath, Encoding.UTF8);
//            var vocabulary = Vocabulary.FromText(vocabContent);

//            // 3. 配置分词器选项（2.0.0版本支持）
//            var bertOptions = new BertTokenizerOptions
//            {
//                CleanText = true,                // 清理文本
//                Lowercase = true,                // 转小写（中文无影响）
//                TokenizeChineseCharacters = true, // 关键：开启中文字符分词
//                StripAccents = true              // 移除重音
//            };

//            // 4. 初始化分词器（2.0.0版本正确构造方式）
//            _tokenizer = new BertTokenizer(vocabulary, bertOptions);
//        }
//        catch (Exception ex)
//        {
//            // 降级方案：空词汇表（仅保证不报错，生产需替换真实vocab.txt）
//            Console.WriteLine($"分词器初始化警告：{ex.Message}，使用空词汇表");
//            _tokenizer = new BertTokenizer(new BertTokenizerOptions
//            {
//                CleanText = true,
//                Lowercase = true,
//                TokenizeChineseCharacters = true
//            });
//        }
//    }

//    /// <summary>
//    /// 文本转ONNX模型输入张量（input_ids/attention_mask/token_type_ids）
//    /// </summary>
//    /// <param name="text">用户输入文本</param>
//    /// <returns>张量字典（键需和ONNX模型输入节点名一致）</returns>
//    public static Dictionary<string, Tensor<int>> Preprocess(string text)
//    {
//        if (string.IsNullOrWhiteSpace(text))
//            throw new ArgumentNullException(nameof(text), "用户文本不能为空");

//        // 1. 分词（BERT格式：[CLS] + 文本 + [SEP]）
//        var encoding = _tokenizer.Encode(text);

//        // 2. 补齐/截断到固定长度
//        var inputIds = PadOrTruncate(encoding.Ids, MaxSequenceLength);
//        var attentionMask = PadOrTruncate(encoding.AttentionMask, MaxSequenceLength);
//        var tokenTypeIds = PadOrTruncate(encoding.TypeIds, MaxSequenceLength);

//        // 3. 转为ONNX张量（维度：[1, MaxSequenceLength]）
//        return new Dictionary<string, Tensor<int>>
//        {
//            { "input_ids", new DenseTensor<int>(inputIds, new[] { 1, MaxSequenceLength }) },
//            { "attention_mask", new DenseTensor<int>(attentionMask, new[] { 1, MaxSequenceLength }) },
//            { "token_type_ids", new DenseTensor<int>(tokenTypeIds, new[] { 1, MaxSequenceLength }) }
//        };
//    }

//    /// <summary>
//    /// 补齐/截断序列
//    /// </summary>
//    private static int[] PadOrTruncate(List<int> sequence, int maxLen)
//    {
//        var result = new int[maxLen];
//        var actualLen = Math.Min(sequence.Count, maxLen);
//        for (int i = 0; i < actualLen; i++)
//            result[i] = sequence[i];
//        // 剩余补0（BERT的PAD值）
//        for (int i = actualLen; i < maxLen; i++)
//            result[i] = 0;
//        return result;
//    }

//    /// <summary>
//    /// 测试分词器（可选）
//    /// </summary>
//    public static void TestTokenizer()
//    {
//        var testText = "帮我导出昨天的废钢验质报告";
//        var encoding = _tokenizer.Encode(testText);
//        Console.WriteLine($"测试文本：{testText}");
//        Console.WriteLine($"分词结果：{string.Join(", ", encoding.Tokens)}");
//        Console.WriteLine($"InputIds前10个：{string.Join(", ", encoding.Ids.Take(10))}");
//    }
//}