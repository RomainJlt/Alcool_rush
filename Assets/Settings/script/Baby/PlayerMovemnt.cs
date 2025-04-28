using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 4f;

    public Rigidbody2D rigidbody;

    public Animator animator; // Référence à l'Animator

    public SpriteRenderer spriteRenderer; // Référence au SpriteRenderer
    
    private Vector2 movement;

    void Update()
    {
        // Entrées du joueur
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement = movement.normalized; // Normaliser le vecteur de mouvement pour éviter une vitesse diagonale plus rapide

        animator.SetFloat("Speed", movement.magnitude); // Mettre à jour le paramètre "Speed" de l'Animator
    
// Initialisation du regard au lancement du jeu


        if (movement.x > 0)
        {
            spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (movement.x < 0)
        {
            spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, -90); // Rotation vers la gauche (-90 degrés)
        }
        else if (movement.y > 0)
        {
            spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, 180); // Rotation vers le bas (180 degrés)

        }
        else if (movement.y < 0)
        {
            spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, 0); // Rotation vers le haut (pas de rotation)

        }


    }

    void FixedUpdate()
    {
        // Déplacement
        rigidbody.linearVelocity = movement * moveSpeed;
    }
}
