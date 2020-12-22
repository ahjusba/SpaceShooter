using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScroller : MonoBehaviour
{
    public Transform backgroundFar;
    public Transform backgroundMid;
    public Transform backgroundClose;

    public float midMultiplier = 2f;
    public float closeMultiplier = 4f;


    public float farSpeed = 5f;
    public float midSpeed;
    public float closeSpeed;

    float midSpeedInit;

    public GameObject boss;
    public Transform bossLocation;
    public Transform bossArrivalLocation;
    float distance;
    float t = 0;

    public GameObject leftWeapon;
    public GameObject centreWeapon;
    public GameObject rightWeapon;

    float gameTimer = 0;

    Vector2 currentPos;

    private void Awake() {
        distance = bossLocation.position.y - bossArrivalLocation.position.y;
        midSpeed = farSpeed * midMultiplier;
        closeSpeed = farSpeed * closeMultiplier;

        midSpeedInit = midSpeed;
        //Time.timeScale = 15f;
    }

    bool stopActions = false;
    bool fightStart = false;

    public void EnableWeapons() {
        leftWeapon.SetActive(true);
        centreWeapon.SetActive(true);
        rightWeapon.SetActive(true);
    }

    private void Update() {

        gameTimer += Time.deltaTime;

        if(gameTimer > 240f) {
            if(stopActions == false) {
                AudioFW.Play("BossSpawn");
                boss.SetActive(true);
                //boss.transform.position = bossLocation.position;
                stopActions = true;
            }
            
            if(midSpeed > 0) {
                midSpeed -= Time.deltaTime * 0.12f;



            } else {
                midSpeed = 0;
                if (!fightStart) {
                    EnableWeapons();
                    fightStart = true;
                }                
            }    
        }

        currentPos = new Vector2(backgroundFar.position.x, backgroundFar.position.y - Time.deltaTime * farSpeed);
        backgroundFar.position = currentPos;

        currentPos = new Vector2(backgroundMid.position.x, backgroundMid.position.y - Time.deltaTime * midSpeed);
        backgroundMid.position = currentPos;

        currentPos = new Vector2(backgroundClose.position.x, backgroundClose.position.y - Time.deltaTime * closeSpeed);
        backgroundClose.position = currentPos;

    }
}
