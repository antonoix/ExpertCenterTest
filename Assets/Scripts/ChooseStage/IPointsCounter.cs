using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPointsCounter
{
    public bool TrySpend();
    public void ReturnPoint();
}
