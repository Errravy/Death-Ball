using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Vector2 spawnTime;
    [SerializeField] GameObject enemy;
    bool canSpawn = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Spawn();
    }
    void Spawn()
    {
        float spawn = Random.Range(spawnTime.x,spawnTime.y);
        if(canSpawn)
        StartCoroutine(SpawnNow(spawn));
    }
    IEnumerator SpawnNow(float time)
    {
        canSpawn = false;
        yield return new WaitForSeconds(time);
        Instantiate(enemy,transform.position,Quaternion.identity);
        canSpawn = true;
    }
}
