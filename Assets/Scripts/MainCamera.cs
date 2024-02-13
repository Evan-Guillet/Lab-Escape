using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {
    public float amplitude = 0.2f;
    public float speed = 2.0f;
    float initialZPosition;

    void Start(){
        initialZPosition = transform.position.z;
        FindObjectOfType<TargetTest>().OnHit += Hit;    // Lancer le tremblement quand le personnage est touché
    }

    void Hit(){
        StartCoroutine(ShakeCamera());
    }

    IEnumerator ShakeCamera(){
        float elapsedTime = 0f;

        while(elapsedTime < 0.15f){ // Durée du tremblement
            // Calcul de tremblement en X et en Y
            float shakeX = Mathf.Cos(Time.time * speed * 1.5f) * amplitude;
            float shakeY = Mathf.Sin(Time.time * speed * 1.5f) * amplitude;

            // Appliquer le tremblement
            transform.localPosition = new Vector3(shakeX, shakeY, initialZPosition);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Repositionner la camera à sa position initial
        transform.localPosition = new Vector3(0f, 0f, initialZPosition);
    }
}
