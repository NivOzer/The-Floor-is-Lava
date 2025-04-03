using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] int bulletDamage = 20;
    [SerializeField] float speed = 20f;
    [SerializeField] Rigidbody2D rb;

    void Start()
    {
        rb.linearVelocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        rb.linearVelocity = transform.right * 0;
        animator.SetTrigger("Destroy");
        Enemy enemy = collision.GetComponent<Enemy>();
        if(enemy != null){
            enemy.TakeDamage(bulletDamage);
        }
        Destroy(gameObject,0.333f);
    }
}
