using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;
    }

    void FixedUpdate()
    {
         Vector2 newPosition =
             rb.position + movement * moveSpeed * Time.fixedDeltaTime;

         newPosition.x = Mathf.Clamp(newPosition.x, -5.3f, 5.3f);
         newPosition.y = Mathf.Clamp(newPosition.y, -2.8f, 2.8f);

         rb.MovePosition(newPosition);
    }
}