using UnityEngine;

public class PickUpAlcool : MonoBehaviour
{
    // public AlcoolManager am; // glisse ici ton GameObject avec le script AlcoolManager dans l'inspecteur

    public int alcoolcount = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Inventory.instance.AddAlcool(1); // Ajoute l'alcool à l'inventaire du joueur
            Destroy(gameObject);
        }
    }
}

