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

    
    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal") * runSpeed;
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
    }

    public void OnLanding(){
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
}
