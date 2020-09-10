using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{

    public LayerMask layer;

    public float velocity;
    public float salto;
    public static bool bebado = false;
    public static bool chapado = false;
    public static bool drogado = false;
    public static bool posFase2 = false;
    //Vida decai com o uso de Drogas
    int vida;
    public bool isPaused = false;

    float movimento;
    public bool isInFloor;
    float previousPositionY;
    public static bool fase2 = false;

    Rigidbody2D physics;
    Animator animator;
    SpriteRenderer spriteRenderer;

    public GameObject object1;
    public GameObject object2;

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
            float positionX = this.transform.position.x;
            float positionY = this.transform.position.y;
    
            if(GameController.level == 3 || Level3.LevelController.PlayerMiny){
                defaultPosition = new Vector3(0f,-0.37f,0f);
                if (positionY < -0.4f) positionY = defaultPosition.y;
            }else{
                defaultPosition = new Vector3(0f,0f,0f);
                if (positionY < 0) positionY = defaultPosition.y;
            }

            if (positionX < 0) positionX = defaultPosition.x;

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
       // isInFloor = PlayerJump.isJump;
        //isInFloor = Physics2D.Linecast(transform.position,groundCheck.position,whatIsGround);
        // Salto
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)){
            if (this.isInFloor){
                if (Player.chapado)
                {
                    this.physics.AddForce(Vector3.up * velocity * salto * 0.9f, ForceMode2D.Impulse);
                }
                else
                {
                    this.physics.AddForce(Vector3.up * velocity * salto, ForceMode2D.Impulse);
                }
                this.isInFloor = false;
            }
        }

        movimento = Input.GetAxis("Horizontal");

        Vector2 nextPosition = new Vector2(transform.position.x,transform.position.y);
        if(movimento > 0){
            nextPosition += new Vector2(0.1f,0f);
        }else if(movimento < 0){
            nextPosition -= new Vector2(0.1f,0f);
        }
        Vector2 currentPosition = new Vector2(transform.position.x,transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(nextPosition,currentPosition,0.01f,layer);
        bool isCollider = false;
        bool isRight = false;
        if(hit.collider != null){
            isCollider = true;
            if(movimento > 0) isRight = true;
            Debug.Log(hit.collider.name+" - "+hit.collider.tag);
        }else{
           Debug.Log("-");
        }
        //if(movimento != 0) object1.transform.localPosition = nextPosition;

        // MOVIMENTO HORIZONTAL
        // Teste de Velocidade com o Metod antigo |
        if (Player.chapado)
        {
            physics.velocity = new Vector2(movimento * velocity/2, physics.velocity.y);
        }
        else
        {
            if(!isCollider || (movimento < 0 && isRight || movimento > 0 && !isRight)){
                physics.velocity = new Vector2( movimento * velocity, physics.velocity.y);
            }
        }

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

