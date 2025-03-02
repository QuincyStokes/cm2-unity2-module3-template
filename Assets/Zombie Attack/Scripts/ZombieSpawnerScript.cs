using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZombieSpawnerScript : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Transform target;

    public float spawnRange = 40;

    public UnityEvent ZombieDied;

    private bool isCoroutineRunning = false;
    
    private void Update()
    {
        if(!isCoroutineRunning)
        {
            StartCoroutine(ZombieSpawnRepeater());
        }
    }

    public Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(-spawnRange,spawnRange), 1, Random.Range(-spawnRange,spawnRange));
    }

    public void SpawnZombie()
    {
        GameObject zombie = Instantiate(zombiePrefab, RandomPosition(), Quaternion.identity);
        zombie.GetComponent<ZombieScript>().Init(target, this);
    }

    public void ZombieHasDied()
    {
        ZombieDied?.Invoke();
    }

    // LESSON 3-4: Add coroutine below.
    private IEnumerator ZombieSpawnRepeater()
    {
        isCoroutineRunning = true;
        yield return new WaitForSeconds(5f);
        SpawnZombie();
        isCoroutineRunning = false;
    }
}
