using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public AudioSource music;
    public float speed = 0.3f;

    private void Awake() {
        music.volume = 0;
        AudioFW.PlayLoop("EWind");
        AudioFW.PlayLoop("ETractorBeam1");
    }

    private void Update() {
        music.volume += Time.deltaTime;
    }


}
