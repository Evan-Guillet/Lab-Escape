using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float _spd = 15.0f;
    public void Update(){
        ProjectileMovement();
    }
    public void ProjectileMovement() {
        Vector3 projectileVelocity = Vector3.right * _spd;
        transform.Translate(projectileVelocity * Time.deltaTime);
    }
    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "wall"){
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Player"){
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Kamikaze"){
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Boss"){
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "EnemyProjectile"){
            Destroy(gameObject);
        }
    }
}
