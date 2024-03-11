using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float visionRadius = 5f;
    public float attackRadius = 2f;
    public Animator animator;
    public float velocity = 2f; // Velocidad de movimiento del enemigo
    private int direction = 1; // Dirección inicial del enemigo (1 para derecha, -1 para izquierda)
    private bool isFacingRight = true;
    public int currentHealth;
    public int EnemyMaxHealth = 3;
    


    void Die()
    {
        
        animator.SetTrigger("Death");
        gameObject.SetActive(false);   
    }

    // Método para dañar al enemigo
    public void TakeDamage(float damage)
    {
        // Restar el daño a la salud del enemigo, aquí deberías tener la lógica para calcular la salud del enemigo
        // Si la salud llega a cero o menos, llamamos al método Die()
        if (currentHealth <= 0)
        {
            Die();
        }
        if (currentHealth <= 0) {  currentHealth = 0; }
    }

    void Start()
    {
        animator.SetBool("Walk", true);
    }
    void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        isFacingRight = !isFacingRight;
    }

    bool IsPlayerInVisionRange()
    {
        return Vector2.Distance(transform.position, player.position) <= visionRadius;
    }

    bool IsPlayerInAttackRange()
    {
        return Vector2.Distance(transform.position, player.position) <= attackRadius;
    }

     
    void MoveTowardsPlayer()
            
    {
       Vector2 directionToPlayer = (player.position - transform.position).normalized;
        // Mover al enemigo en la dirección del jugador (puedes ajustar la velocidad según tus necesidades)
         transform.Translate(directionToPlayer * Time.deltaTime * velocity);
                
        Vector3 newPosition = transform.position + new Vector3(directionToPlayer.x, directionToPlayer.y, 0) * Time.deltaTime * velocity;
         transform.position = newPosition;   
    }

            

    void Update()
    {
        
        // Movimiento del enemigo en el eje x
        transform.Translate(Vector2.right * velocity * direction * Time.deltaTime);

        // Raycast para detectar colisión con objetos con el tag "pared"
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * direction, 0.1f);

        // Si el raycast golpea un objeto con el tag "pared"
        if (hit.collider != null && hit.collider.CompareTag("Wall"))
        {
            // Cambia la dirección del enemigo
            Flip();
            direction *= -1;
        }

        // Verificar si el jugador está dentro del rango de visión
        if (IsPlayerInVisionRange())
         {
            MoveTowardsPlayer();
         }
        
           
        


           // Verificar si el jugador está dentro del rango de ataque
        if (IsPlayerInAttackRange())
         {
           // Ejecutar la animación Attack
            animator.SetBool("Walk", false);
            animator.SetTrigger("Attack");
         }   
    }
}
