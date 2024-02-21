using UnityEngine;

public class DoorLev3_3 : MonoBehaviour
{
    public Animator doorAnimator;
    public BoxCollider2D collider;
    public string openTrigger = "Player";
    public Player player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && player.doorLev3_3 == true)
        {
            collider.isTrigger = true;
            doorAnimator.SetTrigger(openTrigger);
        }
    }
}