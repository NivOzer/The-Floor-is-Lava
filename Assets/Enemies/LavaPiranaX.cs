using UnityEngine;

public class LavaPiranaX : Enemy
{

    [SerializeField] Animator animator;
    int moveDirection = -1;
    public override void Move()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime * moveDirection);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            animator.SetTrigger("Bite");
            TakeDamage(20);
        }
        else if (collision.gameObject.CompareTag("Fortress")){
            FlipSide();
        }
    }

    void FlipSide(){
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        moveDirection *= -1;
    }

}
