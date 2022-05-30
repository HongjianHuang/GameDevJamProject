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
    private float backElapsedTime = 3.0f;
    public Transform player;
    public Power power;
    public bool isDying = true;
    public bool isChasing = true;
    public AudioSource audioSource;
    public bool isPlaying = true;
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
        if(Time.timeScale == 0 && isPlaying){
            audioSource.Pause();
            isPlaying = false;
        }
        if(Time.timeScale != 0 && !isPlaying){
            audioSource.Play();
            isPlaying = true;
        }
        if(!power.IsOn){
            if(isChasing){  
                isChasing = false;
                LeanTween.alpha(gameObject, 1f , 18.0f );
            }
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime/desiredDuration;
            transform.position = Vector3.Lerp(startPosition, player.position, percentageComplete);
            audioSource.volume = Mathf.Clamp01(1f / (transform.position - player.position).magnitude);
        }
        else{
            if(isDying){
                backStartPosition  = transform.position;
                LeanTween.cancel(gameObject);
                LeanTween.alpha(gameObject, 0f , 2.0f );
                isDying = false; 
            }
            if(gameObject.GetComponent<SpriteRenderer>().color.a <= 0){
                Destroy(gameObject);
            }
            backElapsedTime += Time.deltaTime;
            float backPercentageComplet = backElapsedTime/desiredDuration;
            transform.position = Vector3.Lerp(backStartPosition, startPosition , backPercentageComplet);
            audioSource.volume -= 0.3f * Time.deltaTime;

        }
        

    }
}
