using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Panda;

public class Boss : MonoBehaviour {

    NavMeshAgent agent;

    SpriteRenderer spriteRenderer;
    Animator animator;
    public GameObject PerceivedTarget = null;
    
    void Start(){
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update(){
        /*
        if(Input.GetKeyDown(KeyCode.F1)){
            animator.SetTrigger("Idle");

        } else if(Input.GetKeyDown(KeyCode.F2)){
            animator.SetBool("Run", true);

        } else if(Input.GetKeyDown(KeyCode.F3)){
            animator.SetTrigger("Death");

        }
        */
        FlipSpriteToX();
    }

    void FlipSpriteToX(){
        if(agent.velocity.x < 0){
            spriteRenderer.flipX = true;

        } else if(agent.velocity.x > 0){
            spriteRenderer.flipX = false;
        }
    }

    [Task]
    void PursueTarget(){
        if(PerceivedTarget == null){
            Task.current.Fail();
            return;
        }
        animator.SetBool("IsRunning", agent.velocity.x != 0 || agent.velocity.y != 0);
        agent.SetDestination(PerceivedTarget.transform.position);
        if(agent.remainingDistance <= 4){
            Task.current.Fail();
            return;
        }
    }
}
