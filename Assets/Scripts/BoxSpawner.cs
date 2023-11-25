using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField] private Box _boxPrefab;

    [Header("Spawn Zone Settings")] 
    [SerializeField] private float _spawnTime = 3.5f;
    [SerializeField] private float _spawnHeight = 0f;
    [SerializeField] private float _spawnRangeX = 135f;
    [SerializeField] private float _spawnRangeZ = 100f;

    private void Start()
    {
        StartCoroutine(DelaySpawn());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 startPosition = transform.position;

        Gizmos.DrawLine(startPosition, new Vector3(startPosition.x, startPosition.y, _spawnRangeZ));
        Gizmos.DrawLine(startPosition, new Vector3(_spawnRangeX, startPosition.y, startPosition.z));
        Gizmos.DrawLine(new Vector3(startPosition.x, startPosition.y, _spawnRangeZ),
            new Vector3(_spawnRangeX, startPosition.y, _spawnRangeZ));
        Gizmos.DrawLine(new Vector3(_spawnRangeX, startPosition.y, startPosition.z),
            new Vector3(_spawnRangeX, startPosition.y, _spawnRangeZ));
    }

    private IEnumerator DelaySpawn()
    {
        WaitForSeconds spawnDelay = new WaitForSeconds(_spawnTime);

        while (true)
        {
            yield return spawnDelay;
            Spawn();
        }
    }

    private void Spawn()
    {
        Vector3 position = transform.position;
        Vector3 spawnPosition = new Vector3(Random.Range(position.x, _spawnRangeX), _spawnHeight,
            Random.Range(position.z, _spawnRangeZ));

        Instantiate(_boxPrefab, spawnPosition, Quaternion.identity);
    }
}