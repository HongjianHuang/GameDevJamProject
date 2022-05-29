using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject phone;
    public Power power;
    public GameObject blackScreen;
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
        phone.transform.LeanMoveLocal(new Vector2(-475, -845), 0f).setEaseOutExpo();
        LeanTween.alpha(blackScreen.GetComponent<RectTransform>(), 1f , 0f );
        
    }
    public void PhoneOut(){
        phone.transform.LeanMoveLocal(new Vector2(-475, -27), 0.7f).setEaseOutExpo();
        if(power.IsOn){
            LeanTween.alpha(blackScreen.GetComponent<RectTransform>(), 0f , 0.3f ).setEaseOutQuad();
        }
        
        IsOut = true;
    }
    public void PhoneIn(){
        phone.transform.LeanMoveLocal(new Vector2(-475, -845), 0.7f).setEaseOutQuad();
        LeanTween.alpha(blackScreen.GetComponent<RectTransform>(), 1f , 0.3f ).setEaseOutQuad();
        IsOut = false;
    }
    public void ChangePhoneState(){
        if(IsOut==false){
            PhoneOut();
        }
        else{
            PhoneIn();
        }
    }
    private void Update() {
        if(!power.IsOn){
            LeanTween.alpha(blackScreen.GetComponent<RectTransform>(), 1f , 0.3f ).setEaseOutQuad();
        }
        if(power.IsOn && IsOut){
            LeanTween.alpha(blackScreen.GetComponent<RectTransform>(), 0f , 0.3f ).setEaseOutQuad();
        }
    }
    
}
