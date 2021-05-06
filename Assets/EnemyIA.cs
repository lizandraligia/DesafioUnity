using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyIA : MonoBehaviour
{
    public Transform enemy;
    Transform target;
    Path path;
    Seeker seeker;
    Rigidbody2D body;

    public float speed = 200f;
    public float nextWayPointDistance = 3f;

    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    public bool boss;

    void Start()
    {
        target = GameObject.Find("Hero").transform;
        seeker = GetComponent<Seeker>();
        body = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath(){
        
        if(seeker.IsDone())
            seeker.StartPath(body.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p){

        if(!p.error){
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        if(path == null){
            return;
        }
        if(currentWaypoint>=path.vectorPath.Count){
            reachedEndOfPath=true;
            return;
        }else{
            reachedEndOfPath=false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - body.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        body.AddForce(force);

        float distance = Vector2.Distance(body.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWayPointDistance){
            currentWaypoint++;
        }
        
        if(boss){
            if(body.velocity.x >= 0.01f){
                enemy.localScale = new Vector3(-1f,1f,1f);
            }else if(body.velocity.x <= 0.01f){
                enemy.localScale = new Vector3(1f,1f,1f);
            }
        }else{
            if(body.velocity.x >= 0.01f){
                enemy.localScale = new Vector3(-0.6f,0.6f,0.6f);
            }else if(body.velocity.x <= 0.01f){
                enemy.localScale = new Vector3(0.6f,0.6f,0.6f);
            }
        }

        
    }
}   