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
        rb.linearVelocity = transform.right * 0; // Stops on impact
        animator.SetTrigger("Destroy");
        if (collision.TryGetComponent<IDamagable>(out var damagable)){
            damagable.TakeDamage(bulletDamage);
        }
        Destroy(gameObject, 0.333f);
    }

}
