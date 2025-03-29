using Unity.VisualScripting;
using UnityEngine;

public class PlayerX : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    private Rigidbody2D playerRb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.position += Vector3.right * horizontalInput * speed * Time.deltaTime;
        if (horizontalInput != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = horizontalInput > 0 ? 1 : -1;
            transform.localScale = scale;
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            playerRb.AddForce(Vector3.up * jumpForce,ForceMode2D.Impulse);
        }
    }
}
