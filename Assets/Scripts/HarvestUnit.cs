using System;
using UnityEngine;

public class HarvestUnit : MonoBehaviour
{
    [SerializeField] private float _pathTime = 5f;
    [SerializeField] private float _runningTime;

    [SerializeField] private Transform _target;
    [SerializeField] private Transform _base;
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector3 _direction;

    [SerializeField] private float _collectDistance = 10f;
    [SerializeField] private Transform _cargoPlatform;
    [SerializeField] private LayerMask _cargoMask;

    [SerializeField] private bool _isBusy = false;
    [SerializeField] private bool _hasCargo = false;

    private Ray _collectRay;

    public bool IsBusy => _isBusy;

    private void Update()
    {
        Move();
        Collect();
    }

    public void SetTarget(Transform target)
    {
        _runningTime = 0;
        _target = target;
        _startPosition = transform.position;
        _isBusy = true;
    }

    private void Move()
    {
        if (_target)
        {
            transform.LookAt(_target);
            _direction = _target.position - transform.position;

            _runningTime += Time.deltaTime;
            transform.position = Vector3.Lerp(_startPosition, _target.position, _runningTime / _pathTime);
        }
    }

    private void Collect()
    {
        if (_hasCargo)
            return;

        RaycastHit hit;
        _collectRay = new Ray(transform.position, _direction);
        Debug.DrawRay(transform.position, _direction, Color.yellow, _collectDistance);

        if (Physics.Raycast(_collectRay, out hit, _collectDistance, _cargoMask))
        {
            _target = null;
            hit.collider.GetComponent<Box>().Load(_cargoPlatform);
            _hasCargo = true;
        }

        if (_hasCargo)
        {
            Return();
        }
    }

    private void Return()
    {
        SetTarget(_base);
        transform.LookAt(_target);
    }
}