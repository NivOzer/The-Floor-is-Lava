using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerX : MonoBehaviour
{
    [Header("Player Controller")]
        public CharacterController2D controller;
        [SerializeField] float runSpeed = 40f;
        [SerializeField] Rigidbody2D playerRb;
        float horizontalInput = 0f;
        bool jump = false;
        bool crouch = false;

    [Header("Audio")]
        [SerializeField] AudioSource  playerAudioSource;
        [SerializeField] AudioClip jumpAudio;
        [SerializeField] AudioClip insideLavaSound;
        [SerializeField] AudioClip deathSound;
        [SerializeField] AudioClip takeDamageSound;

    [Header("Animations")]
        [SerializeField] Animator animator;

    [Header("Gameplay")]
        [SerializeField] GameManager gameManager;
        bool isAlive = true;
        bool isInLava = false;
        public Weapon weapon;

    [Header("Health")]
        public HealthBar healthBar;
        [SerializeField] int health = 100;
        [SerializeField] int dmgFromBulletToPlayer = 10;

    
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
            playerAudioSource.PlayOneShot(jumpAudio,isInLava ? 0.1f : 0.4f);
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
        if(isInLava){
            Debug.Log("Gaining Lava");
            weapon.gainLava();
        }
    }

    #region Triggers and Colliders
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Enemy")){
            takeDamage(dmgFromBulletToPlayer);
        }
        if (collision.gameObject.CompareTag("Lava")){
            EnterLava();
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Lava")){
            if(!isInLava){
                EnterLava();
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Lava")){
            ExitLava();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")){
            takeDamage(dmgFromBulletToPlayer);
        }
    } 
    #endregion

    #region Lava Logic
        void EnterLava(){
            isInLava = true;
            animator.SetBool("inLava", true);
            playerAudioSource.clip = insideLavaSound;
            playerAudioSource.loop = true;
            playerAudioSource.Play();
            playerRb.gravityScale = 0.3f;
            controller.setJumpForce(100f);
        }
        void ExitLava(){
            isInLava = false;
            animator.SetBool("inLava", false);
            playerAudioSource.Stop();
            playerAudioSource.loop = false;
            playerRb.gravityScale = 3f;
            controller.setJumpForce(400f);
        }

    #endregion

    void takeDamage(int dmg){
        if (isAlive){
            playerAudioSource.PlayOneShot(takeDamageSound);
            animator.SetTrigger("BeenHit");
            health -= dmg;
            healthBar.setHealth(health);
            if (health <=0) isAlive = false;
        }
        else OnDeath();
    }

    void OnDeath()
    {
        playerAudioSource.PlayOneShot(deathSound);
        animator.SetBool("isAlive", isAlive);
        isAlive = true;
        StartCoroutine(gameManager.EndGame());
    }
}
