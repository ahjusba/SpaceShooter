using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    Rigidbody2D rb;

    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float moveSmoothment = 2f;
    [SerializeField] float warpCooldown = 1f;

    Vector2 velocity = Vector2.zero;

    bool canWarp = true;
    [SerializeField] float warpDistance = 1f;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction, bool warpActive) {
        if (warpActive && canWarp) {
            StartCoroutine(WarpCooldown(warpCooldown));
            //warp 1.5 units towards direction
            rb.MovePosition(new Vector2(transform.position.x + direction.x * warpDistance, transform.position.y + direction.y * warpDistance));
            Vector2 targetVelocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);
            rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref velocity, moveSmoothment);

        } else {
            Vector2 targetVelocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);
            rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref velocity, moveSmoothment);
        }
    }

    IEnumerator WarpCooldown(float cooldownTime) {
        canWarp = false;
        yield return new WaitForSeconds(cooldownTime);
        canWarp = true;
    }
}
