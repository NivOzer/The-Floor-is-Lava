using UnityEngine;

public class SkeletonX : Enemy
{    
    float jumpTimer;
    
    // Update is called once per frame
    protected override void Update()
    {
        jumpTimer += Time.deltaTime;
        if(jumpTimer >=1f){
            Jump();
            jumpTimer = 0;
        }
    }

    void Jump(){
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(Vector2.up * 300f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            TakeDamage(20);
            Debug.Log("Took 20 Damage");
        }
    }
}
