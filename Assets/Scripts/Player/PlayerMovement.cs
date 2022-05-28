using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 1f;
    public Rigidbody2D rb;
    Vector2 movement;
    public bool facingRight;
    public Animator anim;
    private void Start() {
        anim = gameObject.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        if(movement.x < 0){
            anim.Play("PlayerSideWalk");
            if(!facingRight){
                Flip();
            }
        }
        if(movement.x > 0){
            anim.Play("PlayerSideWalk");
            if(facingRight){
                Flip();
            } 
        }
        if(movement.y > 0 && movement.x == 0){
            anim.Play("PlayerBackWalk");
        }
        if(movement.y < 0 && movement.x == 0){
            anim.Play("PlayerFrontWalk");
        }
    }
    void Flip(){
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingRight = !facingRight;
    }

}
