using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseUnit{

    public float velocity;
    
    Rigidbody2D physics;
	Animator animator;
	SpriteRenderer spriteRenderer;
    
    void Awake(){
        this.physics = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
	
	void Lat2eUpdate(){
    	float positionY = transform.position.y;
    	if(positionY < 0) positionY = 0;
    	Camera.main.transform.position =  new Vector3(transform.position.x,positionY,-10f);
    }

    void FixedUpdate(){
		float move = Input.GetAxis("Horizontal");

		physics.velocity = new Vector2( move * velocity, physics.velocity.y);
    	if(move > 0 && spriteRenderer.flipX == true || move < 0 && spriteRenderer.flipX == false) Flip();
    }

    void PlayerAnimation(){

    	animator.SetFloat("VelX",Mathf.Abs(physics.velocity.x)); //  <>
    	animator.SetFloat("VelY",Mathf.Abs(physics.velocity.y)); // ^ 
    }

    void Flip(){
    	spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.W))
        {
            this.physics.AddForce(Vector3.up * velocity, ForceMode2D.Impulse);
        }
        PlayerAnimation();
    }
}
