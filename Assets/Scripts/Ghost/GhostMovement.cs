using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManager gm;
    private Vector3 startPosition;
    private Vector3 backStartPosition;
    public float desiredDuration = 20f;
    private float elapsedTime;
    private float backElapsedTime;
    public Transform player;
    public Power power;
    public bool isDying = true;
    public bool isChasing = true;
    public float dyingSpeed = 0.5f; 
    void Start()
    {
        startPosition = transform.position;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player").transform;
        power = GameObject.Find("Player").GetComponentInChildren<Power>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            gm.GameLost();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(!power.IsOn){
            if(isChasing){  
                isChasing = false;
                LeanTween.alpha(gameObject, 1f , 18f );
            }
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime/desiredDuration;
            transform.position = Vector3.Lerp(startPosition, player.position, percentageComplete);
        }
        else{
            if(isDying){
                backStartPosition  = transform.position;
                LeanTween.alpha(gameObject, 0f , 2f );
                isDying = false; 
            }
            if(gameObject.GetComponent<SpriteRenderer>().color.a <= 0){
                Destroy(gameObject);
            }
            backElapsedTime += Time.deltaTime;
            float backPercentageComplet = backElapsedTime/desiredDuration;
            transform.position = Vector3.Lerp(backStartPosition, startPosition , backPercentageComplet);

        }
        

    }
}
