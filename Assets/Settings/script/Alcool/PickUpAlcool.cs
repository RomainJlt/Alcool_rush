using UnityEngine;

public class PickUpAlcool : MonoBehaviour
{
    public AlcoolManager am;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){

            // Détruire l'objet alcool
            Destroy(gameObject);
        }
    }
}
