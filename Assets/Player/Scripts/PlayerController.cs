using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public float speed;

    private Rigidbody2D rb;
    private PlayerAnimation playerAnimation;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    void Update()
    {
        Vector2 direction;
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); // This is for editor

        Move(direction);
    }

    private void Move(Vector2 dir)
    {
        dir = dir.normalized;
        rb.velocity = dir * speed;
        playerAnimation.Animate(dir);
    }
}
