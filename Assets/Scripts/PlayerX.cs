using Unity.VisualScripting;
using UnityEngine;

public class PlayerX : MonoBehaviour
{
    public CharacterController2D controller;
    [SerializeField] float runSpeed = 40f;
    [SerializeField] Animator animator;
    float horizontalInput = 0f;
    bool jump = false;
    bool crouch = false;
    public Weapon weapon;
    public HealthBar healthBar;
    private int health = 100;
    private int dmgFromBulletToPlayer = 10;

    void Start()
    {
          healthBar.setMaxHealth(health);
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed",Mathf.Abs(horizontalInput));
        if(Input.GetButtonDown("Jump")){
            jump = true;
            animator.SetBool("Jump", true);
        }
        if(Input.GetButtonDown("Crouch")){
            crouch = true;
        }
        // Crouch button released
        else if(Input.GetButtonUp("Crouch")){
            crouch = false;
        }
        if (Input.GetButtonDown("Shoot")){
            animator.SetTrigger("Shot");
        }
    }

    public void OnLanding(){
        Debug.Log("Landed!");
        animator.SetBool("Jump", false);
    }

    public void OnCrouching(bool isCrouching){
        animator.SetBool("Crouch", isCrouching);
    }

    void FixedUpdate(){
        controller.Move(horizontalInput * Time.fixedDeltaTime,crouch,jump);
        // Stop the Jump after Jumping
        jump = false;
        // Crouch only when crouch button is pressed
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Lava")){
            Debug.Log("Gaining Lava");
            weapon.gainLava();
        }
        else if(collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Enemy")){
            takeDamage(dmgFromBulletToPlayer);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")){
            takeDamage(dmgFromBulletToPlayer);
        }
    }

    void takeDamage(int dmg){
        health -= dmg;
        healthBar.setHealth(health);
    }
}
