using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float gravityScale = 1f;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        
        if(move > 0)
        {
            transform.Translate(new Vector2(move * moveSpeed * Time.deltaTime, 0));
            animator.SetBool("IsRunning", true);
        }
        else if (move < 0)
        {
            transform.Translate(new Vector2(move * moveSpeed * Time.deltaTime, 0));
            transform.Rotate(new Vector2(180f, 0));
            animator.SetBool("IsRunning", true);
        }

        else
        {
            animator.SetBool("IsRunning", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.gravityScale *= -1;
            Vector3 scale = transform.localScale;
            scale.y *= -1;
            transform.localScale = scale;
            animator.SetTrigger("SpacePressed");
        }
    }

}
