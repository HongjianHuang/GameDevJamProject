using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject phone;
    public bool isOut = false;

    private void Start() {
        isOut =false;
        phone.transform.LeanMoveLocal(new Vector2(-360, -564), 1).setEaseOutExpo();
        
    }
    public void ChangePhoneState(){
        if(isOut==false){
            phone.transform.LeanMoveLocal(new Vector2(-360, -27), 1).setEaseOutExpo();
            isOut = true;
        }
        else{
            phone.transform.LeanMoveLocal(new Vector2(-360, -564), 1).setEaseOutExpo();
            isOut = false;
        }
    }
    
}
