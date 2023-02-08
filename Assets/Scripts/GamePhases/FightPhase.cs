using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class FightPhase : MonoBehaviour
{
    [SerializeField] private FightingView _fightingPanel;

    public async Task<(int, int)> StartFighting(Fighter firstFighter, Fighter secondFighter)
    {
        _fightingPanel.gameObject.SetActive(true);
        _fightingPanel.Init(firstFighter, secondFighter);
        await firstFighter.MakeAttack(secondFighter.DefenceSpots);
        await secondFighter.MakeAttack(firstFighter.DefenceSpots);
        _fightingPanel.gameObject.SetActive(false);
        return (firstFighter.Health, secondFighter.Health);
    }
}
