using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float trucx = 2;
    public float trucy = 2;
    public float speed = 9;
    public float jumpForce = 15;

    Rigidbody2D rb;
    Animator animator;

    bool jumpInput;
    int jumpCount = 0;
    int maxJumpCount = 2;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        LoadPosition(); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            jumpInput = true;
        }
    }

    void FixedUpdate()
    {
        float horizontal = 0f;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localScale = new Vector3(-trucx, trucy, 1);
            horizontal = -1f;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.localScale = new Vector3(trucx, trucy, 1);
            horizontal = 1f;
        }

        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);

        if (jumpInput)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;
            jumpInput = false;
        }

        if (Mathf.Abs(rb.linearVelocity.y) < 0.01f && jumpCount > 0)
        {
            jumpCount = 0;
        }

        animator.SetBool("IsRunning", Mathf.Abs(rb.linearVelocity.x) > 0.01f);
    }
    public void SavePosition()
    {
        PlayerPrefs.SetFloat("PlayerX", transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", transform.position.y);
        PlayerPrefs.Save();
    }

    void LoadPosition()
    {
        if (PlayerPrefs.HasKey("PlayerX") && PlayerPrefs.HasKey("PlayerY"))
        {
            float x = PlayerPrefs.GetFloat("PlayerX");
            float y = PlayerPrefs.GetFloat("PlayerY");

            transform.position = new Vector3(x, y, transform.position.z);
        }
    }
}
