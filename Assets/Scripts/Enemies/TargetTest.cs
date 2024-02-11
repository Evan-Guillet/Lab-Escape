using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TargetTest : MonoBehaviour {
    
    public float maxHitPoints = 100;
    public float currentHitPoints = 100;
    public float lastHitPoints = 100;
    public event Action OnHit;

    void Update(){
        Debug.Log("hitPoints : " + currentHitPoints);
        IsHit();
        Death();
    }

    void IsHit(){
        if(currentHitPoints < lastHitPoints){
            OnHit?.Invoke();
        }
        lastHitPoints = currentHitPoints;
    }

    void Death(){
        if(currentHitPoints <= 0)
            Destroy(gameObject);
    }
}
