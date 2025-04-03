using UnityEngine;

public class PurpleBatX : Enemy
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            TakeDamage(20);
            Debug.Log("Took 20 Damage");
        }
    }
}
