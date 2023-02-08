using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class PreparePhase : MonoBehaviour
{
    public event Action<Fighter, Fighter> OnPhaseFinished;

    [SerializeField] private ChoosePanel _choosePanel;
    [SerializeField] private Sprite _firstPerson;
    [SerializeField] private Sprite _secondPerson;
    [SerializeField] private TMP_Text _timer;

    private bool _hasChosen;
    private Fighter _playerOne;
    private Fighter _playerTwo;

    private const string playerOneName = "Player one";
    private const string playerTwoName = "Player two";

    public void StartPhase(int firstHp, int secondHP)
    {
        Choose(firstHp, secondHP);
    }

    public async Task Choose(int firstHp, int secondHP)
    {
        _choosePanel.OnChoosed += () => _hasChosen = true;

        _choosePanel.AttackChoose.Refresh();
        _choosePanel.DefenceChoose.Refresh();
        _choosePanel.Refresh(playerOneName, _firstPerson);
        await GiveTimeToChoose();
        _playerOne = new Fighter(_choosePanel.AttackChoose.BodySpots, _choosePanel.DefenceChoose.BodySpots, firstHp);

        _choosePanel.AttackChoose.Refresh();
        _choosePanel.DefenceChoose.Refresh();
        _choosePanel.Refresh(playerTwoName, _secondPerson);
        await GiveTimeToChoose();
        _playerTwo = new Fighter(_choosePanel.AttackChoose.BodySpots, _choosePanel.DefenceChoose.BodySpots, secondHP);


        OnPhaseFinished?.Invoke(_playerOne, _playerTwo);
    }

    private async Task GiveTimeToChoose()
    {
        var startTime = Time.time;
        while (Time.time < startTime + 15 && !_hasChosen)
        {
            var leftTimeSec = 15 - (Time.time - startTime);
            _timer.text = String.Format("Time: {0:0.0}", leftTimeSec);
            await Task.Yield();
        }
        _hasChosen = false;
    }
}
