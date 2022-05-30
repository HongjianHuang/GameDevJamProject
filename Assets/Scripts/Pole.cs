using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pole : MonoBehaviour
{   
    public Collider2D rangeCollider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            other.GetComponentInChildren<Power>().Charge();
            other.GetComponentInChildren<Power>().IsOn = true;
            //Debug.Log(other.GetComponentInChildren<Power>().IsOn);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            other.GetComponentInChildren<Power>().isCharging = false;
        }
    }
}
