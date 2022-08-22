﻿using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour {

    GameObject[] spawnPoint;
    public GameObject zombie;
    public PlayerPrefs z;
    public float minSpawnTime = 0.2f;
    public float maxSpawnTime = 1;
    private float lastSpawnTime = 0;
    private float spawnTime = 0;
	// Use this for initialization
	void Start () {
        spawnPoint = GameObject.FindGameObjectsWithTag("Respawn");
        UpdateSpawnTime();
	}

    void UpdateSpawnTime()
    {
        lastSpawnTime = Time.time;
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

    void Spawn()
    {
        int point = Random.Range(0, spawnPoint.Length);
        Instantiate(zombie, spawnPoint[point].transform.position, Quaternion.identity);        
        UpdateSpawnTime();
    }
	
	// Update is called once per frame
	void Update () {
	    if (Time.time >= lastSpawnTime + spawnTime)
        {
            Spawn();
        }
	}
}
