using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Spawn : MonoBehaviour
{
    [SerializeField]
    private float timeToSpawn ;
    [SerializeField] private ObjectPool objectPool;
    [SerializeField]
    private GameObject prefab;
    private string tagg;
    public int remainingEnemy;
    public static Spawn instance;
    
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
  
        remainingEnemy = 40;
        StartCoroutine(SpawnEnemies());
        tagg = prefab.tag;
    }
    
    // Update is called once per frame
    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToSpawn);
            prefab = objectPool.spawnFromPool(tagg, new Vector3(Random.Range(-30f, 30f), 0, Random.Range(-30f, 30f)), Quaternion.identity);
            

        }
    }
}