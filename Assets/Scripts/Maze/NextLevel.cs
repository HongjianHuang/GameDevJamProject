using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    // Start is called before the first frame update
    public MazeGenerator mazeGenerator;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            mazeGenerator.StartNext();
        }
    }
}
