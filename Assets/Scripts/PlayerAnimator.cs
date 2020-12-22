using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

    Rigidbody2D rb;
    SpriteRenderer rend;

    public Sprite PlayerTop;
    public Sprite PlayerR15;
    public Sprite PlayerR30;
    public Sprite PlayerR45;
    public Sprite PlayerL15;
    public Sprite PlayerL30;
    public Sprite PlayerL45;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponentInChildren<SpriteRenderer>();
        rend.sprite = PlayerTop;
    }

    private void Update() {

        if (rb.velocity.x < -0.4f) {
            rend.sprite = PlayerL45;
        } else if (rb.velocity.x < -0.3f) {
            rend.sprite = PlayerL30;
        } else if (rb.velocity.x < -0.1f) {
            rend.sprite = PlayerL15;

        } else if (rb.velocity.x > 0.4f) {
            rend.sprite = PlayerR45;
        } else if (rb.velocity.x > 0.3f) {
            rend.sprite = PlayerR30;
        } else if (rb.velocity.x > 0.1f) {
            rend.sprite = PlayerR15;

        } else {
            rend.sprite = PlayerTop;
        }
    }    
}
