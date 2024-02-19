using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

    SpriteRenderer spriteRenderer;
    Animator animator;
    public GameObject PerceivedTarget = null;
    
    void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update(){
        if(Input.GetButtonDown("F1")){
            animator.SetTrigger("Idle");

        } else if(Input.GetButtonDown("F2")){
            animator.SetTrigger("Run");

        } else if(Input.GetButtonDown("F3")){
            animator.SetTrigger("Idle_2");

        } else if(Input.GetButtonDown("F4")){
            animator.SetTrigger("Run_2");

        }
        
    }
}
