using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    public Power power;
    public Slider battery;

    // Update is called once per frame
    void Update()
    {
        battery.value = power.PowerPercentage;
    }
}
