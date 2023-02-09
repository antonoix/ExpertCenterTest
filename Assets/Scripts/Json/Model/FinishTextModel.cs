using Newtonsoft.Json;

public partial class FinishText
{
    [JsonProperty("draw")]
    public string DrawText;

    [JsonProperty("firstPlayerWin")]
    public string FirstPlayerWinText;

    [JsonProperty("secondPlayerWin")]
    public string SecondPlayerWinText;
}
