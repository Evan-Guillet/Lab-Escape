using UnityEngine;

public class ChestTrigger : MonoBehaviour
{
    public Animator chestAnimator;
    public string openTrigger = "Player";
    public GameObject player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //key = true;
            chestAnimator.SetTrigger(openTrigger);

        }
    }
}
