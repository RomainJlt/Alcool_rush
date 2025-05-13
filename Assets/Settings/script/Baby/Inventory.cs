using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int alcoolCount; // Compteur d'alcool
    public Text alcoolCountText; // Référence au texte d'affichage de l'alcool

    public static Inventory instance; // Instance unique de l'inventaire

    private void Awake()
    {

        instance = this; // Assigne l'instance si elle n'existe pas déjà
    
    }

    public void AddAlcool(int count)
    {
        alcoolCount += count; // Ajoute la quantité d'alcool à l'inventaire
        alcoolCountText.text = alcoolCount.ToString(); // Met à jour le texte d'affichage
    }

}