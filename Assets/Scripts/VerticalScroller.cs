using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScroller : MonoBehaviour
{
    public Transform background;
    public float scrollSpeed = 5f;
    Vector2 currentPos;


    private void Update() {
        currentPos = new Vector2(background.position.x, background.position.y - Time.deltaTime * scrollSpeed);
        background.position = currentPos;
    }
}
