using System;

public interface IBodySpot
{
    public event Action<bool> OnDamaged;

    public SpotType GetSpotType { get; }

    public int AttackCount { get; }

    public void MakeAttack();
    public void HandleAttack();
    public void Refresh();
}

public enum SpotBehaviour
{
    Attack,
    Defend
}

public enum SpotType
{
    LH,
    RH,
    LUB,
    RUB,
    LMB,
    RMB,
    LLB,
    RLB
}
