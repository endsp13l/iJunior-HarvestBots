using System;
using UnityEngine;

public class HarvestUnit : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private Transform _target;

    private bool _isBusy = false;
    public bool IsBusy => _isBusy;

    private void Update()
    {
        Move();
    }

    public void SetTarget(Transform target)
    {
        _target = target;
        // if (target)
        // {
        //     Vector3 direction = target.position - transform.position;
        //     _isBusy = true;
        //
        //     while (transform.position.x < target.position.x)
        //     {
        //         //transform.Rotate(direction * (_speed * Time.deltaTime));
        //
        //         transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime);
        //         //transform.Translate(direction.normalized * (_speed * Time.deltaTime));
        //     }
        // }
    }

    private void Move()
    {
        if (_target)
        {
            transform.position = Vector3.Lerp(transform.position, _target.position, Time.deltaTime);

            Debug.DrawLine(transform.position, _target.position, Color.magenta, 100f);
        }
    }
}