using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class DefenceChooser : MonoBehaviour, IPointsCounter
{
    [SerializeField] Transform _spotsContent;

    public List<IBodySpot> BodySpots { get; private set; }

    private int _points;

    void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _points = 4;
        BodySpots = new List<IBodySpot>();
        Queue<SpotType> spotTypes = new Queue<SpotType>();
        foreach (SpotType spot in Enum.GetValues(typeof(SpotType)))
        {
            spotTypes.Enqueue(spot);
        }

        Assert.IsTrue(spotTypes.Count == _spotsContent.childCount);
        InitSpots(spotTypes);
    }

    private void InitSpots(Queue<SpotType> spotTypes)
    {
        foreach (Transform spot in _spotsContent)
        {
            if (spot.TryGetComponent<BodySpotView>(out var spotView))
            {
                var newSpot = new BodySpot(this, spotView, SpotBehaviour.Defend, spotTypes.Dequeue());
                BodySpots.Add(newSpot);
            }
        }
    }

    public void Refresh()
    {
        foreach (var spot in BodySpots)
            spot.Refresh();
        Initialize();
    }

    public bool TrySpend()
    {
        if (_points > 0)
        {
            _points--;
            return true;
        }
        return false;
    }

    public void ReturnPoint()
    {
        _points++;
    }
}
