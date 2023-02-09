using Newtonsoft.Json;
using UnityEngine;

public partial class FinishText
{
    private const string _filePath = "Texts/ResultTexts";

    public static string GetDrawText()
    {
        var json = Resources.Load<TextAsset>(_filePath);
        return JsonConvert.DeserializeObject<FinishText>(json.text).DrawText;
    }
    public static string GetFirstPlayerWinText()
    {
        var json = Resources.Load<TextAsset>(_filePath);
        return JsonConvert.DeserializeObject<FinishText>(json.text).FirstPlayerWinText;
    }
    public static string GetSecondPlayerWinText()
    {
        var json = Resources.Load<TextAsset>(_filePath);
        return JsonConvert.DeserializeObject<FinishText>(json.text).SecondPlayerWinText;
    }
}
