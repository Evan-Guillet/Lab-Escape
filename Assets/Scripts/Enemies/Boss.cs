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

    public float maxHitPoints = 100;
    public float currentHitPoints = 100;
    public float lastHitPoints = 100;
    float timer = 0.0f;
    
    void Start(){
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update(){
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
        
        animator.SetBool("IsRunning", Mathf.Abs(agent.velocity.x) != 0 || Mathf.Abs(agent.velocity.y) != 0);

        agent.SetDestination(PerceivedTarget.transform.position);
        if(agent.remainingDistance <= 2){
            Task.current.Fail();
            return;
        }
    }

/*
    [Task]
    void LaserAttack(){
        if(PerceivedTarget == null){
            Task.current.Fail();
            return;
        }

    }
    */

    [Task]
    void FlamethrowerAttack(){
        if(PerceivedTarget == null){
            Task.current.Fail();
            return;
        }
        
        float range = Mathf.Abs(agent.remainingDistance);

        if(agent.velocity.x == 0){
            animator.SetFloat("Idle&Shoot_2", range);

        } else if(agent.velocity.x != 0){
            animator.SetFloat("Run&Shoot_2", range);
        }

        if(agent.remainingDistance >= 1.5){
            //Debug.Log("range > 1.5f: " + range);
            Task.current.Fail();
            return;
        }
    }
/*
    [Task]
    void RocketthrowerAttack(){
        if(PerceivedTarget == null){
            Task.current.Fail();
            return;
        }
        
    }
*/
    [Task]
    void Death(){
        if(currentHitPoints > 0){
            Task.current.Fail();
            return;
        }
        if(timer >= 1.5f){
            Destroy(gameObject);
            return;

        }
        animator.SetBool("Death", currentHitPoints <= 0);

        timer += Time.deltaTime;
    }
}
