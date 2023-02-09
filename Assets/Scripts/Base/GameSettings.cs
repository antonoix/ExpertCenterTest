using System.IO;
using UnityEngine;


public class GameSettings : MonoBehaviour
{
    public static GameConfig Core { get; private set; }
    public static UIConfig UI { get; private set; }

    private const string _path = "Settings";
    private const string _gameSettingsFile = "GameSettings";
    private const string _UISettingsFile = "UISettings";

    private void Awake()
    {
        Core = Resources.Load<GameConfig>(Path.Combine(_path, _gameSettingsFile));
        UI = Resources.Load<UIConfig>(Path.Combine(_path, _UISettingsFile));
    }
}
