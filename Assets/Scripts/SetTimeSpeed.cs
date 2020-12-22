using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTimeSpeed : MonoBehaviour {

    [Range(0, 50)] public float timeSpeed = 1;

    private void Update() {
        Time.timeScale = timeSpeed;
    }

}
