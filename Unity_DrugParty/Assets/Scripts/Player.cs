using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseUnit{

    [SerializeField]
    private float velocity;

    [SerializeField]
    private float salto;

    private bool isInFloor;

    Rigidbody2D physics;
	Animator animator;
	SpriteRenderer spriteRenderer;
    
    // Pega os componentes necessários quando o Player eh criado
    void Awake(){
        this.physics = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        this.isInFloor = true;
        this.salto = 1;
    }
	
    // =========== Para que essa função serve ?? ===============//
    // ==========================================================
	/*void Lat2eUpdate(){
    	float positionY = transform.position.y;
    	if(positionY < 0) positionY = 0;
    	Camera.main.transform.position =  new Vector3(transform.position.x,positionY,-10f);
    }*/

    // Movimentação Horizontal do Player (porque não está no Update?) (Vamos usar Force invés de mudar a Velocidade)
    void FixedUpdate(){
		float move = Input.GetAxis("Horizontal");

		physics.velocity = new Vector2( move * velocity, physics.velocity.y);
    	if(move > 0 && spriteRenderer.flipX == true || move < 0 && spriteRenderer.flipX == false) Flip();
    }

    // Animação do Player, correr, pular, etc (???)
    void PlayerAnimation(){

    	animator.SetFloat("VelX",Mathf.Abs(physics.velocity.x)); //  <>
    	animator.SetFloat("VelY",Mathf.Abs(physics.velocity.y)); // ^ 
    }

    //Faz a mudança de Sprite quando troca de direção (???)
    void Flip(){
    	spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    void Update(){

        PlayerAnimation();

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            if (this.isInFloor)
            {
                this.physics.AddForce(Vector3.up * velocity * salto, ForceMode2D.Impulse);
                this.isInFloor = false;
                Camera.main.transform.position = this.transform.position;
            }
        }
        
    }

    // Quando no Chão, ele pode pular
    void OnCollisionEnter2D(Collision2D collision)
    {
        this.isInFloor = true;
    }
}
