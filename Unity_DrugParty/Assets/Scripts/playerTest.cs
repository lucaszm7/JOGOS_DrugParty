using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTest : MonoBehaviour
{

    [SerializeField]
    private float velocity;

    [SerializeField]
    private float salto;

    [SerializeField]
    bool bebado;

    private float movimento;
    private string direcao;
    private bool isInFloor;

    Rigidbody2D physics;
    Animator animator;
    SpriteRenderer spriteRenderer;

    // Pega os componentes necessários quando o Player eh criado
    void Awake()
    {
        this.physics = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        this.isInFloor = true;
        this.salto = 1.3f;
        this.bebado = true;
    }

    // Movimentação da Camera
    void LateUpdate()
    {
        float positionY = transform.position.y;
        if (positionY < 0) positionY = 0;

        // CAMERA BEBADA
        if (this.bebado)
        {
            Camera.main.transform.position = new Vector3(transform.position.x, positionY, -10f);
        }
    }

    void Update()
    {
        PlayerAnimation();
        Movimentacao();
    }

    void Movimentacao()
    {
        // Salto
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            if (this.isInFloor)
            {
                this.physics.AddForce(Vector3.up * velocity * salto, ForceMode2D.Impulse);
                this.isInFloor = false;
            }
        }

        // MOVIMENTO HORIZONTAL
        movimento = Input.GetAxis("Horizontal");
        if ((movimento > 0 || movimento < 0))
        {
            if (movimento > 0)
            {
                if (direcao == "left")
                {
                    this.physics.velocity = Vector2.zero;
                }
                direcao = "right";
                if (this.physics.velocity.x < 10f)
                {
                    this.physics.AddForce(Vector2.right * velocity);
                }
                
            }
            else
            {
                if (direcao == "right")
                {
                    this.physics.velocity = Vector2.zero;
                }
                direcao = "left";
                if (this.physics.velocity.x > -10f)
                {
                    this.physics.AddForce(Vector2.left * velocity);
                }
                
            }
            if (movimento > 0 && spriteRenderer.flipX == true || movimento < 0 && spriteRenderer.flipX == false) Flip();
        }
    }

    // Animação do Player, correr, pular, etc (???)
    void PlayerAnimation()
    {

        animator.SetFloat("VelX", Mathf.Abs(physics.velocity.x)); //  <>
        animator.SetFloat("VelY", Mathf.Abs(physics.velocity.y)); // ^ 
    }

    //Faz a mudança de Sprite quando troca de direção (???)
    void Flip()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    // Quando no Chão, ele pode pular
    void OnCollisionEnter2D(Collision2D collision)
    {
        this.isInFloor = true;
    }
}

