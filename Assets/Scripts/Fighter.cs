using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Fighter
{
    public event Action<Fighter, bool> OnDamaged;

    public List<IBodySpot> AttackSpots { get; private set; }
    public List<IBodySpot> DefenceSpots { get; private set; }

    public int Health { get; private set; }

    private CancellationTokenSource _cancellation;

    public Fighter(List<IBodySpot> attackSpots, List<IBodySpot> defenceSpots, int health)
    {
        Health = health;
        AttackSpots = attackSpots;
        DefenceSpots = defenceSpots;

        foreach (var defence in DefenceSpots)
        {
            defence.OnDamaged += (bool damaged) =>
            {
                if (damaged)
                    Health--;
                OnDamaged?.Invoke(this, damaged);
            };
        }
    }

    public async Task MakeAttack(List<IBodySpot> defenceSpots)
    {
        _cancellation = new CancellationTokenSource();
        foreach (var attackSpot in AttackSpots) 
        { 
            foreach (var defenceOtherFighter in defenceSpots)
            {
                if (attackSpot.GetSpotType == defenceOtherFighter.GetSpotType)
                    while (attackSpot.AttackCount > 0)
                    {
                        attackSpot.MakeAttack();
                        defenceOtherFighter.HandleAttack();
                        if (_cancellation.IsCancellationRequested)
                            break;
                        await Task.Delay(2500);
                    }
            }
        }
    }

    private void OnDestroy()
    {
        _cancellation?.Cancel();
    }

}
