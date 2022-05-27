using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject phone;
    public Power power;
    [SerializeField]
    private bool isOut = false;
    public bool IsOut {
        get {
            return isOut;
        }
        set {
            isOut = value;
            power.isUsing = value;
        }
    }

    private void Start() {
        IsOut =false;
        phone.transform.LeanMoveLocal(new Vector2(-360, -564), 1).setEaseOutExpo();
        
    }
    public void ChangePhoneState(){
        if(IsOut==false){
            phone.transform.LeanMoveLocal(new Vector2(-360, -27), 1).setEaseOutExpo();
            IsOut = true;
        }
        else{
            phone.transform.LeanMoveLocal(new Vector2(-360, -564), 1).setEaseOutExpo();
            IsOut = false;
        }
    }
    
}
