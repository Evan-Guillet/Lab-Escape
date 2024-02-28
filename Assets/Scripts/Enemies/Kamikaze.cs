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
    SpriteRenderer spriteRenderer;
    Animator animator;
    float time = 0.0f;
    bool notYetExploded = true;
    public float damage = 105;
    public float hp = 2;

    public AudioSource audioSource;
    public AudioClip explode;
    SpriteRenderer alerteSpriteRenderer = null;

    void Start(){
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        Transform alerteChild = transform.Find("Alerte");
        alerteSpriteRenderer = alerteChild.GetComponent<SpriteRenderer>();
        alerteSpriteRenderer.enabled = false;
    }

    void Update(){
        FlipSpriteToX();
        Animations();
    }

    void FlipSpriteToX(){
        if(agent.velocity.x < 0){
            spriteRenderer.flipX = true;

        } else if(agent.velocity.x > 0){
            spriteRenderer.flipX = false;
        }
    }

    void Animations(){
        animator.SetBool("IsRunning", agent.velocity.x != 0);
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
            alerteSpriteRenderer.enabled = false;
            Task.current.Fail();
            return;
        }
        alerteSpriteRenderer.enabled = true;
        agent.SetDestination(PerceivedTarget.transform.position);
        if(agent.remainingDistance <= 1){
            alerteSpriteRenderer.enabled = false;
            Task.current.Fail();
            return;
        }
    }

    [Task]
    void SelfDestruction(){
        if(time >= 3f){
            Destroy(gameObject);
            return;

        } else if(time >= 2.0f){
            if(notYetExploded){
                Player player = PerceivedTarget.GetComponent<Player>(); // Bug is here
                player.currentHitPoints -= damage;
                Debug.Log(player.currentHitPoints);
                audioSource.PlayOneShot(explode);
                notYetExploded = false;
                return;
            }
        }

        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            animator.SetTrigger("Death");

        time += Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision){

        if(collision.gameObject.tag == "projectile")
        {
            hp-=1;
            if (hp <= 0){
                Destroy(gameObject);
            }
        }
    }
}
