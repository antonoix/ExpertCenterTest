using System;
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

        await RefreshAndGiveTime(playerOneName, _firstPerson);
        _playerOne = new Fighter(_choosePanel.AttackChoose.BodySpots, _choosePanel.DefenceChoose.BodySpots, firstHp);

        await RefreshAndGiveTime(playerTwoName, _secondPerson);
        _playerTwo = new Fighter(_choosePanel.AttackChoose.BodySpots, _choosePanel.DefenceChoose.BodySpots, secondHP);

        OnPhaseFinished?.Invoke(_playerOne, _playerTwo);
    }

    private async Task RefreshAndGiveTime(string headerText, Sprite person)
    {
        _choosePanel.AttackChoose.Refresh();
        _choosePanel.DefenceChoose.Refresh();
        _choosePanel.Refresh(headerText, person);
        await GiveTimeToChoose();
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
