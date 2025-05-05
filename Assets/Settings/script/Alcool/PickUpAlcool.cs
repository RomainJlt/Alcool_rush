using UnityEngine;

public class PickUpAlcool : MonoBehaviour
{
    public AlcoolManager am;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Alcool"))
        {
            am.alcoolcount++;

            // Détruire l'objet alcool
            Destroy(other.gameObject);
        }
    }
}
