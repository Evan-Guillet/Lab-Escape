using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Panda;

public class Enemy : MonoBehaviour {

    NavMeshAgent agent;

    public GameObject[] waypoints;

    int currentWaypoint = -1;

    public GameObject PerceivedTarget = null;
    SpriteRenderer renderer;
    float lastPosX = 0;
    float currentPosX = 0;

    void Start(){
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        renderer = GetComponent<SpriteRenderer>();
        lastPosX = transform.position.x;
    }

    void Update(){
        FlipSpriteToX();
    }
    
    void FlipSpriteToX(){
        currentPosX = transform.position.x;
        
        if(currentPosX < lastPosX){
            renderer.flipX = true;

        } else if(currentPosX > lastPosX){
            renderer.flipX = false;
        }
        lastPosX = transform.position.x;
    }

    [Task]
    bool NextDestination(){
        currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        agent.SetDestination(waypoints[currentWaypoint].transform.position);
        return true;
    }

    [Task]
    void GoToDestination(){
        if(PerceivedTarget != null){
            Task.current.Fail();
            return;
        }
        if(agent.remainingDistance <= agent.stoppingDistance){
            Task.current.Succeed();
            return;
        }
    }

    [Task]
    void PursueTarget(){
        if(PerceivedTarget == null){
            Task.current.Fail();
            return;
        }
        agent.SetDestination(PerceivedTarget.transform.position);
    }
}
