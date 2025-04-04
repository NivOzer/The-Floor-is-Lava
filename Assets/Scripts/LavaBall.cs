using UnityEngine;

public class LavaBall : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] int bulletDamage = 20;
    [SerializeField] float speed = 20f;
    [SerializeField] float frequency = 2f;
    [SerializeField] Rigidbody2D rb;
    float amplitude;
    float yStart;
    void Start()
    {
        amplitude = Random.Range(0.1f,1f);
        yStart = transform.position.y;
        rb.linearVelocity = transform.right * speed;
    }

    void Update(){
        float yOffset = Mathf.Cos(transform.position.x * frequency) * amplitude;
        transform.position = new Vector2(transform.position.x, yStart + yOffset);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy") && !collision.gameObject.CompareTag("Bullet")){
            rb.linearVelocity = Vector2.zero;
            animator.SetTrigger("Destroy");
            Enemy enemy = collision.GetComponent<Enemy>();
            if(enemy != null){
                enemy.TakeDamage(bulletDamage);
            }
            Destroy(gameObject,0.333f);
        }
    }
}
