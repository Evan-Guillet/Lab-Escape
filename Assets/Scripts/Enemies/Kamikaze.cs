using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Panda;

public class Kamikaze : MonoBehaviour {

    NavMeshAgent agent;

    public GameObject[] waypoints;

    int currentWaypoint = -1;

    public GameObject PerceivedTarget = null;
    SpriteRenderer renderer;
    Animator animator;
    float time = 0.0f;
    bool notYetExploded = true;
    float damage = 35;

    void Start(){
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update(){
        FlipSpriteToX();
        Animations();
    }

    void FlipSpriteToX(){
        if(agent.velocity.x < 0){
            renderer.flipX = true;

        } else if(agent.velocity.x > 0){
            renderer.flipX = false;
        }
    }

    void Death(){
        Destroy(gameObject);
    }

    void Animations(){
        animator.SetBool("IsRunning", agent.velocity.x != 0);

        if(Input.GetKeyDown(KeyCode.F3)){
            animator.SetTrigger("Death");
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
        if(agent.remainingDistance <= 1){
            Task.current.Fail();
            return;
        }
    }

    [Task]
    void SelfDestruction(){
        if(time >= 2.29f){
            Destroy(gameObject);
            return;

        } else if(time >= 2.0f && notYetExploded){
            TargetTest player = PerceivedTarget.GetComponent<TargetTest>();
            player.currentHitPoints -= damage;
            notYetExploded = false;
            return;
        }

        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            animator.SetTrigger("Death");

        time += Time.deltaTime;
    }
}
