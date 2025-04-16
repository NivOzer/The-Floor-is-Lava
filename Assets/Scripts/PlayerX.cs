using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerX : MonoBehaviour
{
    public CharacterController2D controller;
    [SerializeField] float runSpeed = 40f;
    [SerializeField] Rigidbody2D playerRb;
    [SerializeField] Animator animator;
    [SerializeField] GameManager gameManager;
    float horizontalInput = 0f;
    bool jump = false;
    bool crouch = false;
    bool isAlive = true;
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
        PlayerMovement();
    }
    void PlayerMovement(){
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

        if(!isAlive){
            OnDeath();
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

    #region Triggers and Colliders
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Enemy")){
            takeDamage(dmgFromBulletToPlayer);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Lava")){
            Debug.Log("Gaining Lava");
            weapon.gainLava();
            playerRb.mass = 1.2f;
        }
    }

    void OTriggerExit2D(Collider2D collision)
    {
        playerRb.mass = 1f;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")){
            takeDamage(dmgFromBulletToPlayer);
        }
    } 
    #endregion

    void takeDamage(int dmg){
        if (isAlive){
            animator.SetTrigger("BeenHit");
            health -= dmg;
            healthBar.setHealth(health);
            if (health <=0) isAlive = false;
        }
    }

    void OnDeath()
    {
        animator.SetBool("isAlive", isAlive);
        isAlive = true;
        StartCoroutine(gameManager.EndGame());
    }
}
