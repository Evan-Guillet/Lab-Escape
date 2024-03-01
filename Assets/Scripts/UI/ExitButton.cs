using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    public GameObject button;

    void Start(){
         #if UNITY_WEBGL
            button.SetActive(false);
        #endif
    }

   
}
