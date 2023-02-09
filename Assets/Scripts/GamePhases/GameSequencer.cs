using System.Threading.Tasks;
using UnityEngine;

public class GameSequencer : MonoBehaviour
{
    [SerializeField] private PreparePhase _prepare;
    [SerializeField] private FightPhase _fight;
    [SerializeField] private FinishPhase _finish;

    private int? _firstFighterHP;
    private int? _secondFighterHP;

    async void Start()
    {
        await Task.Delay(1000);
        _prepare.OnPhaseFinished += GoToFight;
        var firstHP = _firstFighterHP == null ? GameSettings.Core.StartFighterHP : _firstFighterHP;
        var secondHP = _secondFighterHP == null ? GameSettings.Core.StartFighterHP : _secondFighterHP;
        _prepare.StartPhase((int)firstHP, (int)secondHP);
    }

    private async void GoToFight(Fighter first, Fighter second)
    {
        (int firstHP, int secondHP) result = await _fight.StartFighting(first, second);
        _firstFighterHP = result.firstHP;
        _secondFighterHP = result.secondHP;
        if (_firstFighterHP > 0 && _secondFighterHP > 0)
        {
            _prepare.StartPhase((int)_firstFighterHP, (int)_secondFighterHP);
        }
        else
        {
            _finish.EndGame((int)_firstFighterHP, (int)_secondFighterHP);
        }
    }
}
