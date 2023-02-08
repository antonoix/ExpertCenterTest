using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class FightingView : MonoBehaviour
{
    [SerializeField] private FighterView _firstFighter;
    [SerializeField] private FighterView _secondFighter;

    private Dictionary<Fighter, FighterView> _fighters;
    private bool _isInited;

    public void Init(Fighter first, Fighter second)
    {
        _fighters = new Dictionary<Fighter, FighterView>()
        {
            { first, _firstFighter },
            { second, _secondFighter }
        };

        first.OnDamaged += PlayEffect;
        second.OnDamaged += PlayEffect;
    }

    public async void PlayEffect(Fighter damaged, bool isBlocked)
    {
        print("AttackView");
        var effect = isBlocked ? _fighters[damaged].DamagedEffect : _fighters[damaged].BlockedEffect;
        effect.Play();
        _fighters[damaged].HealthCount.text = damaged.Health.ToString();
        await Task.Delay(1000);
    }

    private void OnDisable()
    {
        foreach (var e in _fighters.Keys)
        {
            e.OnDamaged -= PlayEffect;
        }
    }
}

[System.Serializable]
public class FighterView
{
    public SpotEffects Effects;
    public TMP_Text HealthCount;
    public ParticleSystem BlockedEffect;
    public ParticleSystem DamagedEffect;
}
