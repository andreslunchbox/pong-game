using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    public float speed = 10f;
    public Rigidbody2D rb;

    void FixedUpdate()
    {
        if(!isLocalPlayer)
        {
            return;
        }
        rb.velocity = speed * Time.fixedDeltaTime * new Vector2(0, Input.GetAxisRaw("Vertical"));
    }
}
