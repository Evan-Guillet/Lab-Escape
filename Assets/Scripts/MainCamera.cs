using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainCamera : MonoBehaviour {
    public float amplitude = 0.2f;
    public float speed = 2.0f;
    float initialZPosition;
    public Transform playerPosition;
    
    void Start(){
        initialZPosition = transform.position.z;
        FindObjectOfType<Player>().OnHit += Hit;    // Lancer le tremblement quand le personnage est touché
    }

    void Update(){
        if (playerPosition != null){
            transform.position = new Vector3(playerPosition.position.x, playerPosition.position.y, transform.position.z);
        }
    }

    void Hit(){
        StartCoroutine(ShakeCamera());
    }

    IEnumerator ShakeCamera(){
        float elapsedTime = 0f;
        
        while(elapsedTime < 0.15f){ // Durée du tremblement
            // Calcul de tremblement en X et en Y
            float shakeX = Mathf.Cos(Time.time * speed) * amplitude;
            float shakeY = Mathf.Sin(Time.time * speed) * amplitude;
            
            // Appliquer le tremblement
            transform.localPosition = new Vector3(shakeX, shakeY, initialZPosition);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        // Repositionner la camera à sa position initial
        transform.localPosition = new Vector3(0f, 0f, initialZPosition);
    }
}
