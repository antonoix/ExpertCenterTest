using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotEffects : MonoBehaviour
{
    public SpotType Type { get; private set; }

    public void Init(SpotType type)
    {
        Type = type;
    }

    public void MakeEffect()
    {

    }
}
