using UnityEngine;

[CreateAssetMenu(fileName = "UISettings", menuName = "UI/Settings")]
public class UIConfig : ScriptableObject
{
    [field: SerializeField] public int ButtonsTransitionDurationMs { get; private set; }
}
