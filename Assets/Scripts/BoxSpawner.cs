using UnityEngine;
using System.Collections;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField] private Box _boxPrefab;

    [SerializeField] private float _spawnTime = 10f;
    [SerializeField] private float _spawnHeight = 10f;
    [SerializeField] private float _spawnRangeX = 222f;
    [SerializeField] private float _spawnRangeZ = 100f;

    private void Start()
    {
        StartCoroutine(DelaySpawn());
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