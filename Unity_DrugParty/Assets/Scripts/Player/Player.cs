using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{

    public float velocity;
    public static bool bebado = false;
    public bool posFase2 = false;
    //Vida decai com o uso de Drogas
    int vida;
    public bool isPaused = false;

    float movimento;
    bool isInFloor;
    float previousPositionY;
    public static bool fase2 = false;

    [SerializeField]
    float salto;
    Rigidbody2D physics;
    Animator animator;
    SpriteRenderer spriteRenderer;

    // Pega os componentes necessários quando o Player eh criado
    void Awake(){
        this.physics = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        this.velocity = 3;
        this.isInFloor = true;
        this.salto = 1.3f;
     //   this.bebado = false;
    }

    // Movimentação da Camera
    void LateUpdate(){
        if(!isPaused){
            Vector3 defaultPosition;
            if(GameController.level == 3){
                defaultPosition = new Vector3(-1.47f,-0.38f,0f);
            }else{
                defaultPosition = new Vector3(0f,0f,0f);
            }

            float positionY = this.transform.position.y;
            float positionX = this.transform.position.x;
            if (positionY < 0) positionY = defaultPosition.y;
            if (positionX < -1) positionX = defaultPosition.x;

            // CAMERA NORMAL
            Camera.main.transform.position = new Vector3(positionX, positionY, -10f);
        }
    }

    void FixedUpdate(){
        if (!isPaused) Movimentacao();
        PlayerAnimation();
    }

    void Movimentacao()
    {
        if(physics.velocity.y > 0f){
            previousPositionY = transform.position.y;    
        }

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)){
            if (this.isInFloor){
                this.physics.AddForce(Vector3.up * velocity * salto, ForceMode2D.Impulse);
                this.isInFloor = false;
            }
        }

        // MOVIMENTO HORIZONTAL
        movimento = Input.GetAxis("Horizontal");
        // Teste de Velocidade com o Metod antigo |
        physics.velocity = new Vector2( movimento * velocity, physics.velocity.y);

        if (movimento > 0 && spriteRenderer.flipX == true || movimento < 0 && spriteRenderer.flipX == false) Flip();

    }

    // Animações do Player
    void PlayerAnimation(){

        animator.SetFloat("VelX", Mathf.Abs(physics.velocity.x)); //  <>
        animator.SetFloat("VelY", Mathf.Abs(physics.velocity.y)); // ^ 
        animator.SetBool("isJumping", !(physics.velocity.y < 0.1f && transform.position.y < previousPositionY)); 
    }

    //Faz a mudança de Sprite quando troca de direção
    void Flip()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    // Quando no Chão, ele pode pular
    void OnCollisionEnter2D(Collision2D collision){
        switch(collision.gameObject.tag){
            case "Floor":
                isInFloor = true;
            break;
        }
    }
}

