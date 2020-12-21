using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpawner : MonoBehaviour
{
    //X clamp: -4.5 ... 4.5
    public float speed = 2f;
    bool movingRight = true;

    private void Update() {

        if(transform.position.x >= 4.5f) {
            movingRight = !movingRight;
        } else if (transform.position.x < -4.5f) {
            movingRight = !movingRight;
        }

        speed = (movingRight ? speed : speed * -1);
        float direction = speed * Time.deltaTime;
        transform.Translate(direction, 0, 0, Space.Self);
    }
}
