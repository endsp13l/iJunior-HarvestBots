using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    [SerializeField] Text _collectedCargoCount;
    
    private HarvestUnit[] _units;
    private int _cargoCount;

    private void Start()
    {
        _units = GetComponentsInChildren<HarvestUnit>();
    }

    private void Update()
    {
        for (int i = 0; i < _units.Length; i++)
        {
            if (_units[i].IsBusy == false)
                TrySendUnit(_units[i]);
        }
    }

    public void ReceiveCargo()
    {
        _cargoCount++;
        _collectedCargoCount.text = $"Collected: {_cargoCount} boxes";
    }

    private void TrySendUnit(HarvestUnit unit)
    {
        Box target = FindTarget();

        if (target)
        {
            target.Mark();
            unit.SetTarget(target.transform);
        }
    }

    private Box FindTarget()
    {
        Box target = FindObjectOfType<Box>();

        if (target && target.IsMarked == false && target.IsOnGround)
            return target;

        return null;
    }
}