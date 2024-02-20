using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Animator doorAnimator;
    public string openTrigger = "Player";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))// and player.key == true)
        {
            doorAnimator.SetTrigger(openTrigger);
        } else if (collision.CompareTag("Enemy"))
        {
            doorAnimator.SetTrigger(openTrigger);
        } else {
            doorAnimator.SetTrigger("Close");
        }
    }
}
