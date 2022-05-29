using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    // Start is called before the first frame update
    public Power power;
    public GameObject player;
    public float distance;
    [SerializeField]
    private GameObject ghostPrefab;
    [SerializeField]
    private Vector2 randomPointOnCircle;
    [SerializeField]
    private bool nullVector;
    private void Start() {
        GetNewRandomPoint();
    }
    // Update is called once per frame
    private void GetNewRandomPoint(){
        randomPointOnCircle = Random.insideUnitCircle.normalized * distance;
        if(randomPointOnCircle == new Vector2(0f, 0f)) 
            nullVector = true;
        while(nullVector)
        {
            randomPointOnCircle = Random.insideUnitCircle.normalized * distance;
            if(randomPointOnCircle != new Vector2(0f, 0f)) 
                nullVector = false;
        }
        
    }
    void Update()
    {
        if(!power.IsOn){
            if (GameObject.Find("Ghost(Clone)") == null)
            {
                GetNewRandomPoint();
                Instantiate(ghostPrefab, new Vector3(player.transform.position.x + randomPointOnCircle.x, 
                player.transform.position.y + randomPointOnCircle.y, 0), Quaternion.identity);
            }
            
        }
        
        
    }
}
