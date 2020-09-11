using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{

    public LayerMask layer;

    public float velocity;
    public float salto;
    public static bool bebado = false;
    public static bool chapado = false;
    [SerializeField]
    private float forceChapado;
    public static bool drogado = false;
    public Vector3 posicaoInicial;
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
        this.posicaoInicial = transform.position;
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

    void Update(){
        if (!isPaused && !Player.chapado) Movimentacao();
        if (Player.chapado)
        {
            movimentacaoChapado();
        }
        PlayerAnimation();
    }

    public void PlayerDead(){
        
        transform.position = posicaoInicial;
        //Player.drogado = false;

        if (Player.chapado)
        {
            Player.chapado = false;
            PlayerLife.SetHP(100);
            LoadScene.Load("Level2");
        }
        else
        {
            PlayerLife.SetHP(100);
            LoadScene.Load("Level3");
        }
        //transform.localPosition = new Vector3(0f,0.58f,0f);    
    }

    void Movimentacao()
    {
        if(physics.velocity.y > 0f){
            previousPositionY = transform.position.y;    
        }
        //isInFloor = PlayerJump.isJump;
        //isInFloor = Physics2D.Linecast(transform.position,groundCheck.position,whatIsGround);
        // Salto
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)){
            if (this.isInFloor){
                this.physics.AddForce(Vector3.up * velocity * salto, ForceMode2D.Impulse);
                isInFloor = false;
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
        if(hit.collider != null){
            isCollider = true;
        }

        // MOVIMENTO HORIZONTAL || (movimento < 0 && isRight || movimento > 0 && !isRight)
        // Teste de Velocidade com o Metod antigo |
        if (Player.chapado)
        {
            physics.velocity = new Vector2(movimento * velocity/2, physics.velocity.y);
        }
        else
        {
            if(!isCollider){
                physics.velocity = new Vector2( movimento * velocity, physics.velocity.y);
            }
        }

        if (movimento > 0 && spriteRenderer.flipX == true || movimento < 0 && spriteRenderer.flipX == false) Flip();

    }

    private void movimentacaoChapado()
    {
        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
        {
            this.Impulse();
        }
        if (movimento > 0 && spriteRenderer.flipX == true || movimento < 0 && spriteRenderer.flipX == false) Flip();
    }
    public void Impulse()
    {
        this.physics.velocity = Vector2.zero;
        this.physics.AddForce(Vector2.up * this.forceChapado, ForceMode2D.Impulse);
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
                if (Player.chapado)
                {
                    PlayerLife.SetHP(-3);
                }
                break;
            case "Obstaculo":
                if (Player.chapado)
                {
                    PlayerLife.SetHP(-3);
                }
                break;
        }
    }
}

