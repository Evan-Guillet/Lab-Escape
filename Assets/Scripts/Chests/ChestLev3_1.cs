using UnityEngine;

public class ChestLev3_1 : MonoBehaviour
{
    public Animator chestAnimator;
    public string openTrigger = "Player";
    public Player player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
       {
            if (player != null)
            {
                player.doorLev3_1 = true;
                chestAnimator.SetTrigger(openTrigger);
            }
            else
            {
                Debug.LogError("Player GameObject non assigné à ChestLev1_1");
            }
        }
    }
}
