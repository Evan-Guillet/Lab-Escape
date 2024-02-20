using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Animator doorAnimator; // Référence à l'Animator de la porte
    public string openTrigger = "Player"; // Nom du déclencheur d'ouverture dans l'Animator

    // Déclenché lorsque le Collider2D entre en collision avec un autre Collider2D
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Si le joueur entre dans la zone de détection, déclencher l'animation d'ouverture de la porte
            doorAnimator.SetTrigger(openTrigger);
        } else if (collision.CompareTag("Enemy"))
        {
            doorAnimator.SetTrigger(openTrigger);
        } else {
            doorAnimator.SetTrigger("Close");
        }
    }

    // Déclenché lorsque le Collider2D perd la collision avec un autre Collider2D
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Si le joueur sort de la zone de détection, déclencher l'animation de fermeture de la porte
            doorAnimator.SetTrigger(openTrigger);
        }
    }
}
