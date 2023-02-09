using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Game/Settings")]
public class GameConfig : ScriptableObject
{
    [field: SerializeField] public int ChoosePoints { get; private set; }
    [field: SerializeField] public int StartFighterHP { get; private set; }
}
