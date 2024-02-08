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
    Animator animator;

    void Start(){
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update(){
        FlipSpriteToX();
        animator.SetBool("IsRunning", agent.velocity.x != 0);
    }

    void FlipSpriteToX(){
        if(agent.velocity.x < 0){
            renderer.flipX = true;

        } else if(agent.velocity.x > 0){
            renderer.flipX = false;
        }
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
