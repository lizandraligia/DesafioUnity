using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemy;
    public GameObject boss;
    float nextSpawn = 2f;
    public float spawnRate = 2f;
    public int spawned;
    public static int score;
    public TextMeshProUGUI scoreUI;
    // Start is called before the first frame update
    void Start()
    {
        spawned = 1;
        score = 0;
    }

    void Update()
    {
        scoreUI.text = score.ToString();
        if(!PauseMenu.GameIsPaused){
            if(Time.time >= nextSpawn){
                int randPoint = Random.Range(0, spawnPoints.Length);
                if(spawned % 10 == 0){
                    spawnRate = spawnRate + 0.3f;
                    Instantiate(boss, spawnPoints[randPoint].position, transform.rotation);                
                }else{
                    Instantiate(enemy, spawnPoints[randPoint].position, transform.rotation);
                }
                spawned++;    
                nextSpawn = Time.time + 7f / spawnRate;
            }
        }            
    }
}