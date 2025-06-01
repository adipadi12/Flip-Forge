using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float gravityScale = 1f;
    [SerializeField] private float shakeIntensity = 0.1f; // Shake intensity

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal");

        float moveInput = Input.GetAxis("Horizontal") * moveSpeed;

        Vector3 movement = Vector3.right * moveInput * Time.deltaTime;

        if (move > 0)
        {
            transform.localRotation = new Quaternion(0, 0, 0, move);
            transform.Translate(movement);
        }
        else if (move < 0)
        {
            transform.localRotation = new Quaternion(0, 180, 0, move);
            transform.Translate(-movement);
        }
        animator.SetFloat("Speed", Mathf.Abs(move));
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.Instance.PlayGravitySwitchSound(); // Play gravity switch sound
            rb.gravityScale *= -1;
            Vector3 scale = transform.localScale;
            scale.y *= -1;
            transform.localScale = scale;
            animator.SetTrigger("SpacePressed");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            //animator.SetBool("IsGrounded", true);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}
