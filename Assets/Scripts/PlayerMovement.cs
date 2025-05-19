using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator animator;
    private bool grounded;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalnput = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(horizontalnput * speed, body.linearVelocity.y);

        if (Input.GetKey(KeyCode.Space) && grounded)
            Jump();

        //flips character
        if (horizontalnput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalnput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        animator.SetBool("run", horizontalnput != 0);
        animator.SetBool("grounded", grounded);


    }

    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, speed);
        animator.SetTrigger("jump");
        grounded = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }

}
