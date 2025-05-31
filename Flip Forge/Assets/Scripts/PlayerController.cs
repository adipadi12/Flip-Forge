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
            rb.gravityScale *= -1;
            Vector3 scale = transform.localScale;
            scale.y *= -1;
            transform.localScale = scale;
            animator.SetTrigger("SpacePressed");
        }
    }

}
