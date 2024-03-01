using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    float _cycleTime = 0.0f;
    [SerializeField] float _fireRate = 1.0f;
    [SerializeField] public GameObject projectil;

    // Update is called once per frame
    void Update()
    {
        LaserAttack();
    }

    void LaserAttack(){
        if(Time.time>_cycleTime){

            _cycleTime = Time.time + _fireRate;
            if (transform.rotation.y > 0){
                Instantiate(projectil, new Vector3(transform.position.x-0.5f,transform.position.y,transform.position.z), transform.rotation);
            }else{
                Instantiate(projectil, new Vector3(transform.position.x+0.5f,transform.position.y,transform.position.z), transform.rotation);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision){

        if(collision.gameObject.tag == "projectile"){   
            Destroy(gameObject);
        }
    }
}
