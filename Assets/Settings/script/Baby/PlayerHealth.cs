using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3; // Santé maximale du joueur
    private int currentHealth; // Santé actuelle du joueur

    public bool isAlive = true; 

    public Transform healthbarUI;
    
    public GameObject hpPrefab; // Préfabriqué de la barre de vie

    public Animator animator; // Référence à l'Animator

    public SpriteRenderer spriteRenderer; // Référence au SpriteRenderer

    void Awake()
    {
        currentHealth = maxHealth; // Initialise la santé actuelle à la santé maximale
        UpdateHealthbarUI();
    }

    public void takeDamage(int damage)
    {
        if(isAlive);
        {
            currentHealth -= damage; // Réduit la santé actuelle du joueur
            UpdateHealthbarUI();

            if (currentHealth <= 0)
            {
                isAlive = false; // Le joueur est mort
                animator.SetTrigger("Die");
                StartCoroutine(ChargerSceneApresDelai(2f)); // Lance la coroutine
                IEnumerator ChargerSceneApresDelai(float delai)
                {
                    yield return new WaitForSeconds(delai);
                    SceneManager.LoadScene("MainMenuScene");
                }
            }
        }


    }

    public void UpdateHealthbarUI()
    {
        foreach (Transform child in healthbarUI)
        {
            Destroy(child.gameObject); // Détruit les anciennes barres de vie
        }

        for (int i = 0; i < currentHealth; i++)
        {
            Instantiate(hpPrefab, healthbarUI); // Crée de nouvelles barres de vie
        }
    }

    public void DisablePlayerVisual()
    {
        spriteRenderer.enabled = false; // Désactive le SpriteRenderer pour masquer le joueur
    }
    
}