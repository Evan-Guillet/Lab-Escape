using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Player : MonoBehaviour {

    public float walkSpeed = 20; // unité unity par second
    public float jumpForce = 50;
    float moveDirectionX = 0;
    float moveDirectionY = 0;
    float isShooting = 0;
    float isDead = 0;
    public float maxHitPoints = 100;
    public float currentHitPoints = 100;
    public float lastHitPoints = 100;
     public float lastHitPointds = 100;
    public event Action OnHit;
    float time = 0.0f;
    Rigidbody2D rigidBody;
    SpriteRenderer renderer;
    Animator animator;

    [SerializeField] float _fireRate = 1.0f;
    float _cycleTime = 0.0f;

    [SerializeField] public GameObject projectil;

    //pause variables
    float inputPause = 0f;
    public GameObject canvas;

    void Start(){
        rigidBody = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update(){
        transform.position += new Vector3(walkSpeed*moveDirectionX*Time.deltaTime,0,0);
        transform.position += new Vector3(0,walkSpeed*moveDirectionY*Time.deltaTime,0);
        PlayerShoot();
        IsHit();
        Death();
    }

    void OnMoveX(InputValue value){
        moveDirectionX = value.Get<float>();
        animator.SetBool("IsWalking", moveDirectionX != 0 || moveDirectionY != 0);
        if(moveDirectionX > 0){
           transform.rotation = new Quaternion(0,0,0,0);
        } else if (moveDirectionX < 0){
           transform.rotation = new Quaternion(0,180,0,0);
        }
    }

    void OnMoveY(InputValue value){
        moveDirectionY = value.Get<float>();
        animator.SetBool("IsWalking", moveDirectionY != 0 || moveDirectionX != 0);
    }

    /*
    void OnInteract(InputValue value){
        isDead = value.Get<float>();
        animator.SetBool("IsDead", currentHitPoints <= 0);
        Destroy(gameObject, 1.3f);
    }
    */

    void PlayerShoot(){
        animator.SetBool("IsShooting",false);
        if(Input.GetKeyDown(KeyCode.Semicolon)){
            if(Time.time>_cycleTime){
                animator.SetBool("IsShooting", true);
                _cycleTime = Time.time + _fireRate;
                print(projectil);
                if (projectil != null){
                    Instantiate(projectil,new Vector3(transform.position.x,transform.position.y-0.5f,transform.position.z), transform.rotation);

                } else {
                    Debug.LogError("the bullet is NULL");
                }
            }
        }
    }

    void IsHit(){
        if(currentHitPoints < lastHitPoints){
            OnHit?.Invoke();
        }
        lastHitPoints = currentHitPoints;
    }


    void Death(){
        if(currentHitPoints <= 0){
            if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
                animator.SetTrigger("Death");

            if(time >= 1.3f)
                Destroy(gameObject);

            time += Time.deltaTime;
        }
    }

    /*
    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "projectileEnnemie"){
            currentHitPoints -= 1;
        }
    }*/

    void OnPause(InputValue value){
        inputPause = value.Get<float>();
        if (inputPause != 0f){
            Time.timeScale = 0f;
            canvas.SetActive(true);
        }
    }
}
