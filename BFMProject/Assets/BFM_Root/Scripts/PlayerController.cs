using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //V Rferencia
    private Rigidbody2D playerRb;
    private Animator anim;
    private float horizontalInput;

    //V EStadisticas
    public float speed;
    public float jumpforce;
    public Transform RespawnPoint;
    public Transform ShootPoint;
    private bool isFacingRight = true;
    [SerializeField] bool isGrounded;
    [SerializeField] GameObject groundCheck;
    [SerializeField] LayerMask groundLayer;

    [Header("Player Stats")]
    public int playerHealth;
    public int maxHealth = 3;



    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerHealth = maxHealth;
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position,0.1f,groundLayer);
        Movement();
        Jump();
        Attack();
        if (playerHealth > maxHealth) { playerHealth = maxHealth; }
        if (playerHealth < 0) {  playerHealth = 0; }
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        playerRb.velocity = new Vector2(horizontalInput * speed,playerRb.velocity.y);

        //Flip
        if (horizontalInput >0)
        {
            anim.SetBool("walk",true);
            if(!isFacingRight)
            {
                Flip();
            }
        }
        if (horizontalInput < 0)
        {
            anim.SetBool("walk",true);
            if (isFacingRight)
            {
                Flip();
            }
        }
        if ( horizontalInput == 0)
        {
            anim.SetBool("walk",false);
        }
    }

    void Jump()
    {
        anim.SetBool("Jump",!isGrounded);
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerRb.AddForce(Vector3.up * jumpforce,ForceMode2D.Impulse);
        }
        
    }

    void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        isFacingRight = !isFacingRight;
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.F))
        {
            anim.SetTrigger("Attack");
        }
    }

    void Respawn()
    {
        transform.position = RespawnPoint.position;
    }

    void OnCollisionEnter(Collision other)
    {
     // Comprobar si la colisión es con un obstáculo (puedes ajustar esto según las etiquetas o capas de tus objetos)
     if (playerHealth == 0)
     {
        // Respawnea al jugador al punto de inicio
        Respawn();
     }
    }  
}
