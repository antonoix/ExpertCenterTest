using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class SpotsChooser : MonoBehaviour, IPointsCounter
{
    [SerializeField] Transform _spotsContent;

    public List<IBodySpot> BodySpots { get; private set; }

    protected abstract SpotBehaviour _behaviour { get; set; }

    private int _points;

    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _points = GameSettings.Core.ChoosePoints;
        BodySpots = new List<IBodySpot>();

        InitSpots();
    }

    private void InitSpots()
    {
        Queue<SpotType> spotTypes = new Queue<SpotType>();
        foreach (SpotType spot in Enum.GetValues(typeof(SpotType)))
            spotTypes.Enqueue(spot);

        Assert.IsTrue(spotTypes.Count == _spotsContent.childCount);

        foreach (Transform spot in _spotsContent)
        {
            if (spot.TryGetComponent<BodySpotView>(out var spotView))
            {
                var newSpot = new BodySpot(this, spotView, _behaviour, spotTypes.Dequeue());
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
