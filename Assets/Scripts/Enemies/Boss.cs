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

    float timer = 0.0f;
    public float hitPoints = 10;
    Vector3 deathPosition;

    [SerializeField] public GameObject projectil;

    [SerializeField] float _fireRate = 1.0f;
    float _cycleTime = 0.0f;
    
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
            // spriteRenderer.flipX = true;
            transform.rotation = new Quaternion(0,180,0,0);

        } else if(agent.velocity.x > 0){
            // spriteRenderer.flipX = false;
            transform.rotation = new Quaternion(0,0,0,0);
        }
    }

    [Task]
    void PursueTarget(){
        if(PerceivedTarget == null){
            Task.current.Fail();
            return;
        }
        if(hitPoints <= 0){
            deathPosition = transform.position;
            Task.current.Fail();
            return;
        }
        RocketthrowerAttack();
        agent.SetDestination(PerceivedTarget.transform.position);
        animator.SetBool("IsRunning", Mathf.Abs(agent.remainingDistance) > 0.0002f);
        //Debug.Log("Mathf.Abs(agent.remainingDistance): " + Mathf.Abs(agent.remainingDistance));
        
        if(agent.remainingDistance <= 1f){
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
    /*
    [Task]
    void FlamethrowerAttack(){
        if(PerceivedTarget == null){
            Task.current.Fail();
            return;
        }
        
        float range = Mathf.Abs(agent.remainingDistance);

        if(Mathf.Abs(agent.velocity.x) == 0 || Mathf.Abs(agent.velocity.y) == 0){
            animator.SetFloat("Idle&Shoot_2", range);

        } else if(Mathf.Abs(agent.velocity.x) != 0 || Mathf.Abs(agent.velocity.y) != 0){
            animator.SetFloat("Run&Shoot_2", range);
        }


        if(agent.remainingDistance >= 1.5){
            Task.current.Fail();
            return;
        }
        
    }
    */
    


    [Task]
    void RocketthrowerAttack(){
        if(PerceivedTarget == null){
            Task.current.Fail();
            return;
        }
        if(Time.time>_cycleTime){
            _cycleTime = Time.time + _fireRate;
            if(PerceivedTarget.transform.position.y >= transform.position.y && PerceivedTarget.transform.position.y <= transform.position.y + 10f){
                if(agent.velocity.x > 0 && PerceivedTarget.transform.position.x >= transform.position.x){
                    Instantiate(projectil, new Vector3(transform.position.x+2f,transform.position.y+1f,transform.position.z), transform.rotation);
                } 
                if(agent.velocity.x < 0 && PerceivedTarget.transform.position.x <= transform.position.x){
                    Instantiate(projectil, new Vector3(transform.position.x-2f,transform.position.y+1f,transform.position.z), transform.rotation);
                } 
            } else {
                Task.current.Fail();
                return;
            }
        }
    }

    [Task]
    void Death(){
        if(hitPoints > 0){
            Task.current.Fail();
            return;
        }
        if(timer >= 1.5f){
            Destroy(gameObject);
            return;
        }
        transform.position = deathPosition;
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            animator.SetTrigger("Death");
        
        timer += Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision){

        if(collision.gameObject.tag == "projectile"){
            hitPoints -= 10;
            Debug.Log(hitPoints);
        }
    }
}
