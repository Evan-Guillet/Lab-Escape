using UnityEngine;

public class DoorLev3_2: MonoBehaviour
{
    public Animator doorAnimator;
    public BoxCollider2D collider;
    public string openTrigger = "Player";
    public Player player;
    public bool key = false;

    private void Start()
    {
        if (player != null)
        {
            if (player.doorLev3_2)
            {
                key = true;
            }
        }
        else
        {
            Debug.LogError("Player GameObject non assigné à DoorLev3_2.cs");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && key)
        {
            collider.isTrigger = true;
            doorAnimator.SetTrigger(openTrigger);
        }
    }
}