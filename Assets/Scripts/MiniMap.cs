using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform level;
    void Start()
    {
         transform.position = level.transform.position + new Vector3(5, 5, -10);
    }

    // Update is called once per frame
}
