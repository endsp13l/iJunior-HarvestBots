using UnityEngine;

public class HarvestUnit : MonoBehaviour
{
    [SerializeField] private float _pathTime = 5f;
    [SerializeField] private float _runningTime;
    [SerializeField] private float _loadDistance = 5f;

    [SerializeField] private Transform _base;
    [SerializeField] private Transform _cargoPlatform;
    [SerializeField] private LayerMask _cargoMask;
    [SerializeField] private LayerMask _baseMask;

    private Transform _target;
    private Vector3 _startPosition;
    private Vector3 _direction;
    private Box _box;
    private Ray _collectRay;

    private bool _isBusy = false;
    private bool _hasCargo = false;

    public bool IsBusy => _isBusy;

    private void Update()
    {
        Move();
        TryInteract();
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

        if (_box)
        {
            _box.transform.localPosition = Vector3.zero;
        }
    }

    private void TryInteract()
    {
        RaycastHit hit;
        _collectRay = new Ray(transform.position, _direction);

        if (Physics.Raycast(_collectRay, out hit, _loadDistance, _cargoMask))
        {
            if (hit.transform == _target && _hasCargo == false)
            {
                Collect(hit);
            }
        }

        if (Physics.Raycast(_collectRay, out hit, _loadDistance, _baseMask))
        {
            if (_hasCargo)
            {
                Unload(hit);
            }
        }
    }

    private void Collect(RaycastHit hit)
    {
        _target = null;
        _box = hit.collider.gameObject.GetComponent<Box>().Load(_cargoPlatform);
        _hasCargo = true;

        if (_box)
            Return();
    }

    private void Unload(RaycastHit hit)
    {
        _target = null;

        _cargoPlatform.DetachChildren();
        _box.Unload();
        _box = null;

        hit.collider.gameObject.GetComponent<Base>().ReceiveCargo();

        _hasCargo = false;
        _isBusy = false;
    }

    private void Return()
    {
        SetTarget(_base);
        transform.LookAt(_target);
    }
}