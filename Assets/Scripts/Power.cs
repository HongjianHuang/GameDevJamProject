using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Power : MonoBehaviour
{
    [Range(0f, 100f)]
    public float powerPercentage;
    public float PowerPercentage {
        get {
            return powerPercentage;
        }
        set {
            powerPercentage = value;
        }
    }
    public float drainSpeedWhenUsing;
    public float drainSpeedWhenNotUsing;
    public float DrainSpeed {
        get {
            if (isUsing) {
                return drainSpeedWhenUsing;
            }
            return drainSpeedWhenNotUsing;
        }
    }
    public bool isUsing;
    public bool IsOn {
        get {
            return lightSource.enabled;
        }
        set {
            lightSource.enabled = value;
        }
    }
    public float chargeSpeed;
    public bool isCharging;
    public Light2D lightSource;
    public void PowerOff() {
        PowerPercentage = 0f;
        IsOn = false;
    }
    public void Charge() {
        isCharging = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isCharging) {
            if (PowerPercentage < 100f) PowerPercentage += chargeSpeed * Time.deltaTime;
        } else if (IsOn) {
            int beforePowerPercentageFloor = Mathf.FloorToInt(PowerPercentage);
            PowerPercentage -= Time.deltaTime * DrainSpeed;
            int afterPowerPercentageFloor = Mathf.FloorToInt(PowerPercentage);
            if (afterPowerPercentageFloor < beforePowerPercentageFloor) {
                float powerOffProbability = 1f - 0.1f * afterPowerPercentageFloor;
                float rand = Random.Range(0f, 1f);
                if (rand <= powerOffProbability) {
                    if(!isCharging){
                        PowerOff();
                    }
                    
                }
            }
        }
        
    }
}
