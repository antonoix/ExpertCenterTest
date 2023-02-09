using TMPro;
using UnityEngine;

public class FinishPhase : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private TMP_Text _result;

    public void EndGame(int firstFighterHP, int secondFighterHP)
    {
        _panel.SetActive(true);
        if (firstFighterHP <= 0 && secondFighterHP <= 0)
        {
            _result.text = FinishText.GetDrawText();
        }
        else
        {
            if (firstFighterHP > secondFighterHP)
                _result.text = FinishText.GetFirstPlayerWinText();
            else
                _result.text = FinishText.GetSecondPlayerWinText();
        }
    }
}
