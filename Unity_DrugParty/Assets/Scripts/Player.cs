using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float velocity;
    [SerializeField]
    float salto;
    [SerializeField]
    bool bebado;

    //Vida decai com o uso de Drogas
    int vida;

    float movimento;
    bool isInFloor;
    float previousPositionY;


    Rigidbody2D physics;
    Animator animator;
    SpriteRenderer spriteRenderer;

    // Pega os componentes necessários quando o Player eh criado
    void Awake()
    {
        this.physics = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        this.velocity = 3;
        this.isInFloor = true;
        this.salto = 1.3f;
        this.bebado = false;
    }

    // Movimentação da Camera
    void LateUpdate(){
        float positionY = transform.position.y;
        float positionX = transform.position.x;
        if (positionY < 0) positionY = 0;
        if (positionX < 0) positionX = 0;

        // CAMERA NORMAL
        Camera.main.transform.position = new Vector3(positionX, positionY, -10f);

        // CAMERA BEBADA
        if (this.bebado)
        {
            Camera.main.transform.position = new Vector3(positionX, positionY, -10f);
        }
    }

    void Update(){
        Movimentacao();
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
            case "Finish":
                collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                //GameController.Finish();
            break;
            case "Floor":
                isInFloor = true;
            break;
        }
    }
}

