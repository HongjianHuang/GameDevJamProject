using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour
{
    [Range(0f, 100f)]
    public float powerPercentage;
    public float PowerPercentage {
        get {
            return powerPercentage;
        }
        set {
            powerPercentage = Mathf.Clamp(value, 0f, 100f);
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
    public bool isOn;
    public float chargeSpeed;
    private bool isCharging;

    public void PowerOff() {
        PowerPercentage = 0f;
        isOn = false;
    }
    
    public IEnumerator Charge() {
        if (isCharging) {
            yield return null;
        } else {
            isCharging = true;
            while (PowerPercentage < 100f) {
                PowerPercentage += chargeSpeed * Time.deltaTime;
                yield return null;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn) {
            int beforePowerPercentageFloor = Mathf.FloorToInt(PowerPercentage);
            PowerPercentage -= Time.deltaTime * DrainSpeed;
            int afterPowerPercentageFloor = Mathf.FloorToInt(PowerPercentage);
            if (afterPowerPercentageFloor < beforePowerPercentageFloor) {
                float powerOffProbability = 1f - 0.1f * afterPowerPercentageFloor;
                float rand = Random.Range(0f, 1f);
                if (rand <= powerOffProbability) {
                    PowerOff();
                }
            }
        }
    }
}
