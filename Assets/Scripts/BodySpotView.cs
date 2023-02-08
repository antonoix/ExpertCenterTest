using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BodySpotView : MonoBehaviour
{
    public event Action OnClicked;

    [SerializeField] private TMP_Text _name;
    [SerializeField] private GameObject _cross;
    [SerializeField] private TMP_Text _attackCount;

    public void Set(string name)
    {
        Refresh();
        GetComponentInChildren<Button>().onClick.AddListener(() => OnClicked?.Invoke());

        _name.text = name;

    }

    public void HandleDefenceSelect(bool value)
    {
        _cross.SetActive(value);
    }

    public void HandleAttackSelect(int count)
    {
        _attackCount.text = count.ToString();
    }

    public void Refresh()
    {
        GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        if (_cross != null)
            _cross.SetActive(false);
        if (_attackCount != null)
            _attackCount.text = "0";
    }
}
