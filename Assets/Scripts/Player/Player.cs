using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float walkSpeed = 4; // unité unity par second
    public float jumpForce = 50;
    float moveDirectionX = 0;
    float moveDirectionY = 0;
    float isShooting = 0;
    float isDead = 0;
    Rigidbody2D rigidBody;
    SpriteRenderer renderer;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
         transform.position += new Vector3(walkSpeed*moveDirectionX*Time.deltaTime,0,0);
         transform.position += new Vector3(0,walkSpeed*moveDirectionY*Time.deltaTime,0);
    }

    void OnMoveX(InputValue value){
        moveDirectionX = value.Get<float>();
         animator.SetBool("IsWalking",moveDirectionX != 0 || moveDirectionY != 0);
        if(moveDirectionX >0){
            renderer.flipX = false;
        }else if (moveDirectionX <0){
            renderer.flipX = true;
        }
    }

    void OnMoveY(InputValue value){
        moveDirectionY = value.Get<float>();
        animator.SetBool("IsWalking",moveDirectionY != 0 || moveDirectionX != 0);
    }

    void OnShoot(InputValue value){
        isShooting = value.Get<float>();
        animator.SetBool("IsShooting",isShooting != 0 );
    }

     void OnInteract(InputValue value){
        isDead = value.Get<float>();
        animator.SetBool("IsDead",isDead != 0 );
        Destroy(gameObject, 1.3f);
    }
}
