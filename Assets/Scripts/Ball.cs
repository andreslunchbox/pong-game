using UnityEngine;
using Mirror;

public class Ball : NetworkBehaviour
{
    public float speed = 30;
    public Rigidbody2D rb;

    void Start()
    {
        rb.simulated = true;
        rb.velocity = Vector2.left * speed;
    }

    float HitFactor(Vector2 ballPosition, Vector2 racketPosition, float racketHeight)
    {
        return (ballPosition.y - racketPosition.y) / racketHeight;
    }

    [ServerCallback]
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<Player>())
        {
            rb.velocity = Vector2.Reflect(rb.velocity, collision.contacts[0].normal).normalized * speed;

            float y = HitFactor(transform.position, collision.transform.position, collision.collider.bounds.size.y);
            float x = collision.relativeVelocity.x > 0 ? 1 : -1;

            Vector2 dir = new Vector2(x, y).normalized;
            rb.velocity = dir * speed;
        }
    }
}
