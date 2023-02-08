using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSequencer : MonoBehaviour
{
    [SerializeField] private PreparePhase _prepare;
    [SerializeField] private FightPhase _fight;

    private int? _firstFighterHP;
    private int? _secondFighterHP;

    void Start()
    {
        _prepare.OnPhaseFinished += GoToFight;
        var firstHP = _firstFighterHP == null ? 8 : _firstFighterHP;
        var secondHP = _secondFighterHP == null ? 8 : _secondFighterHP;
        _prepare.StartPhase((int)firstHP, (int)secondHP);
    }

    private async void GoToFight(Fighter first, Fighter second)
    {
        print("GoToFight");
        (int firstHP, int secondHP) result = await _fight.StartFighting(first, second);
        _firstFighterHP = result.firstHP;
        _secondFighterHP = result.secondHP;
        if (_firstFighterHP > 0 && _secondFighterHP > 0)
            _prepare.StartPhase((int)_firstFighterHP, (int)_secondFighterHP);
    }
}
