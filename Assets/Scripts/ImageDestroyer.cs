﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageDestroyer : MonoBehaviour {
    private void Update() {
        if(transform.position.y < -50) {
            Destroy(this.gameObject);
        }
    }
}
