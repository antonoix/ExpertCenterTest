using System;
using UnityEngine;

[RequireComponent(typeof(BodySpotView))]
public class BodySpot : IBodySpot
{
    public event Action<bool> OnDamaged;

    public SpotType GetSpotType => _type;
    public int AttackCount { get; private set; }

    private IPointsCounter _counter;
    private BodySpotView _view;
    private SpotType _type;
    private SpotBehaviour _behaviour;

    private bool _isArmored;

    public BodySpot(IPointsCounter counter, BodySpotView view, SpotBehaviour behavior, SpotType spotType)
    {
        _counter = counter;
        _behaviour = behavior;
        _type = spotType;
        _view = view;
        _view.Set(spotType.ToString());
        _view.OnClicked += HandleSelect;
    }

    private void HandleSelect()
    {
        if (_behaviour == SpotBehaviour.Attack)
        {
            if (_counter.TrySpend())
                _view.HandleAttackSelect(++AttackCount);
        }
        else
        {
            if (_isArmored)
            {
                _counter.ReturnPoint();
                _isArmored = false;
            }
            else if (_counter.TrySpend())
            {
                _isArmored = true;
            }
            else
            {
                return;
            }
            _view.HandleDefenceSelect(_isArmored);
        }
    }

    public void MakeAttack()
    {
        AttackCount--;
    }

    public void HandleAttack()
    {
        if (_behaviour == SpotBehaviour.Attack)
            throw new System.Exception("Can't attack this spot");
        OnDamaged?.Invoke(!_isArmored);
    }

    public void Refresh()
    {
        _view.Refresh();
        _view.OnClicked -= HandleSelect;
    }
}
