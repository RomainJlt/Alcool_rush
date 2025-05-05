using UnityEngine;

public class PickUpAlcool : MonoBehaviour
{
    public AlcoolManager am;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            am.alcoolcount++;

            // Détruire l'objet alcool
            Destroy(gameObject);
        }
    }
}
