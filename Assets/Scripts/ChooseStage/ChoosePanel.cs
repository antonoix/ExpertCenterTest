using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoosePanel : MonoBehaviour
{
    public event Action OnChoosed;

    [field: SerializeField] public AttackChooser AttackChoose { get; private set; }
    [field: SerializeField] public DefenceChooser DefenceChoose { get; private set; }

    [SerializeField] private Button _chooseButton;
    [SerializeField] private TMP_Text _header;
    [SerializeField] private Image[] _personImages;

    private void Awake()
    {
        _chooseButton.onClick.AddListener(() => OnChoosed?.Invoke());
    }

    public void Refresh(string headerTxt, Sprite person)
    {
        _header.text = headerTxt;
        foreach(var personImage in _personImages)
        {
            personImage.sprite = person;
        }
    }
}
