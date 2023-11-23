using System;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private Transform _bezierReferencePoint1;
    [SerializeField] private Transform _bezierReferencePoint2;
    [SerializeField] private float _loadTime = 1f;

    [SerializeField] private bool _isMarked;
    [SerializeField] private bool _isOnGround;

    public bool IsMarked => _isMarked;
    public bool IsOnGround => _isOnGround;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Ground>())
            _isOnGround = true;
    }

    public void Mark() => _isMarked = true;

    public void Load(Transform target)
    {
        transform.LookAt(target);

        float loadingTime = 0;
        while (loadingTime < _loadTime)
        {
            transform.position = Bezier.GetPoint(transform.position, _bezierReferencePoint1.position,
                _bezierReferencePoint2.position, target.position, loadingTime / _loadTime);

            loadingTime += Time.deltaTime;
        }
    }
}