using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemy;
    public GameObject boss;
    float nextSpawn = 2f;
    public float spawnRate = 2f;
    public int score;
    // Start is called before the first frame update
    void Start()
    {
        score = 1;
    }

    void Update()
    {
        if(Time.time >= nextSpawn){
            int randPoint = Random.Range(0, spawnPoints.Length);
            if(score % 10 == 0){
                Instantiate(boss, spawnPoints[randPoint].position, transform.rotation);                
            }else{
                Instantiate(enemy, spawnPoints[randPoint].position, transform.rotation);
            }
            score++;    
            nextSpawn = Time.time + 7f / spawnRate;
        }
            
    }
}