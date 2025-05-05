using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3; // Santé maximale du joueur
    [SerializeField]private int currentHealth; // Santé actuelle du joueur

    void Awake()
    {
        currentHealth = maxHealth; // Initialise la santé actuelle à la santé maximale
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage; // Réduit la santé actuelle du joueur
        // if (currentHealth <= 0)
        // {
        //     Die(); // Appelle la
        // }
    }

    
}