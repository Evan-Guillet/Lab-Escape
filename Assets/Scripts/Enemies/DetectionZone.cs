using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.tag == "Player"){
            if(transform.parent.tag == "Boss")
                transform.parent.GetComponent<Boss>().PerceivedTarget = collider.gameObject;

            if(transform.parent.tag == "Kamikaze")
                transform.parent.GetComponent<Kamikaze>().PerceivedTarget = collider.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D collider){
        if(collider.tag == "Player"){
            if(transform.parent.tag == "Boss")
                transform.parent.GetComponent<Boss>().PerceivedTarget = null;

            if(transform.parent.tag == "Kamikaze")
                transform.parent.GetComponent<Kamikaze>().PerceivedTarget = null;
        }
    }
}
