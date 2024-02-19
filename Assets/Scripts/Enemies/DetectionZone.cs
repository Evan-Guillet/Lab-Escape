using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.tag == "Player"){
            transform.parent.GetComponent<Boss>().PerceivedTarget = collider.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D collider){
        if(collider.tag == "Player"){
            transform.parent.GetComponent<Boss>().PerceivedTarget = null;
        }
    }
}
