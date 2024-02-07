using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour {
    
    void Start(){
        
    }

    void Update(){
        
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            transform.parent.GetComponent<Enemy>().pursuedPlayer = collider.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            transform.parent.GetComponent<Enemy>().pursuedPlayer = null;
        }
    }
}
