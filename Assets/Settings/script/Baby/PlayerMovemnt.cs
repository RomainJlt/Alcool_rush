using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 4f;

    public Rigidbody2D rigidbody;

    public Animator animator; // Référence à l'Animator

    public SpriteRenderer spriteRenderer; // Référence au SpriteRenderer
    
    private Vector2 movement;

    public PlayerHealth playerHealth; // Référence à la classe PlayerHealth

    void Update()
    {
        if (playerHealth.isAlive) // Vérifie si le joueur est vivant
        {
            // Entrées du joueur
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            movement = movement.normalized; // Normaliser le vecteur de mouvement pour éviter une vitesse diagonale plus rapide

            animator.SetFloat("Speed", movement.magnitude); // Mettre à jour le paramètre "Speed" de l'Animator


            if (movement.x > 0)
            {
                spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, -90);
            }
            else if (movement.x < 0)
            {
                spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else if (movement.y > 0)
            {
                spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (movement.y < 0)
            {
                spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, 180);
            }
        }



    }

    void FixedUpdate()
    {
        // Déplacement
        rigidbody.linearVelocity = movement * moveSpeed;
    }
}
