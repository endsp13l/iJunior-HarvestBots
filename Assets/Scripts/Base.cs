using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    private HarvestUnit[] _units;
    private Queue<Box> _resources;

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

    private void TrySendUnit(HarvestUnit unit)
    {
        Box target = FindTarget();

        if (target)
        {
            target.Mark();
            unit.SetTarget(target.transform);
            Debug.Log("unit sent");
            Debug.Log(target.transform.position);
            Debug.Log(unit.name);
        }
    }

    private Box FindTarget()
    {
        Box target = FindObjectOfType<Box>();

        if (target && target.IsMarked == false && target.IsOnGround)
        {
            Debug.Log("Target found");
            return target;
        }

        return null;
    }
}