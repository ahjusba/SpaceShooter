using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour {
    

    List<EnemySpawnStruct> wave1 = new List<EnemySpawnStruct>();

    public List<GameObject> movingSpawners = new List<GameObject>();
    public List<GameObject> stationarySpawners = new List<GameObject>();

    public GameObject leftSpawn;
    public GameObject rightSpawn;
    List<GameObject> leftSpawner = new List<GameObject>();
    List<GameObject> rightSpawner = new List<GameObject>();

    List<GameObject> enemies = new List<GameObject>();

    float gameTimer = 0f;
    EnemySpawnStruct enemy;
    List<GameObject> spawnerList = new List<GameObject>();

    public GameObject rockSmall;            //0
    public GameObject rockMedium;           //1
    public GameObject rockBig;              //2

    public GameObject fighter;              //3
    public GameObject mineLayer;            //4

    public GameObject laserLeft;            //5
    public GameObject laserRight;           //6
    public GameObject laserDown;            //7
    public GameObject laserTilted;          //8
    private void Awake() {

        leftSpawner.Add(leftSpawn);
        rightSpawner.Add(rightSpawn);

        enemies.Add(rockSmall);
        enemies.Add(rockMedium);
        enemies.Add(rockBig);
        enemies.Add(fighter);
        enemies.Add(mineLayer);
        enemies.Add(laserLeft);
        enemies.Add(laserRight);
        enemies.Add(laserDown);
        enemies.Add(laserTilted);

        //Rockshower Wave1 0 - 45
        enemy = new EnemySpawnStruct(10, 0, 1, 1);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(12, 0, 1, 1);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(13, 0, 1, 1);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(15, 1, 1, 1);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(16, 0, 3, 2);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(17, 0, 3, 1);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(18, 1, 2, 1);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(25, 2, 1, 1);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(28, 0, 3, 1);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(29, 2, 1, 1);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(31, 1, 1, 1);        
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(35, 2, 2, 1);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(36, 0, 3, 2);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(37, 0, 3, 1);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(38, 1, 3, 2);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(38, 1, 3, 1);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(38, 0, 1, 3);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(38, 0, 1, 4);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(39, 0, 3, 1);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(39, 0, 3, 3);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(40, 2, 3, 1);
        wave1.Add(enemy);

        //Fighters Wave1 45 - 75
        enemy = new EnemySpawnStruct(45, 3, 1, 1);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(47, 3, 2, 1);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(49, 3, 3, 1);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(51, 3, 3, 1);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(53, 1, 2, 1);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(54, 0, 1, 3);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(54, 0, 1, 4);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(60, 3, 3, 2);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(61, 3, 1, 3);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(61, 3, 1, 4);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(65, 3, 3, 1);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(68, 0, 3, 1);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(70, 3, 2, 1);
        wave1.Add(enemy);

        //MineLayers 75 - 115
        enemy = new EnemySpawnStruct(75, 4, 1, 1);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(83, 4, 2, 1);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(84, 3, 2, 2);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(85, 0, 3, 1);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(88, 0, 1, 3);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(89, 0, 1, 4);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(90, 0, 1, 3);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(92, 0, 1, 3);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(92, 0, 1, 4);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(97, 3, 3, 1);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(102, 4, 3, 1);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(105, 0, 1, 4);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(110, 4, 3, 1);
        wave1.Add(enemy);

        //Layers 115-...
        enemy = new EnemySpawnStruct(125, 5, 1, 4);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(130, 5, 1, 4);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(132, 6, 1, 3);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(136, 7, 3, 1);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(140, 3, 1, 2);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(145, 8, 1, 3);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(146, 8, 1, 4);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(148, 8, 1, 3);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(150, 8, 1, 3);
        wave1.Add(enemy);

        enemy = new EnemySpawnStruct(157, 8, 1, 3);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(161, 7, 1, 2);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(162, 3, 2, 1);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(165, 8, 1, 4);
        wave1.Add(enemy);

        enemy = new EnemySpawnStruct(173, 5, 1, 4);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(174, 6, 1, 3);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(178, 5, 1, 4);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(179, 6, 1, 3);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(180, 5, 1, 4);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(180, 4, 3, 1);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(182, 6, 1, 3);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(185, 5, 1, 4);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(188, 8, 1, 3);
        wave1.Add(enemy);

        enemy = new EnemySpawnStruct(198, 0, 1, 4);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(200, 0, 1, 4);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(200, 1, 1, 5);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(205, 3, 2, 1);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(205, 7, 2, 1);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(210, 4, 2, 1);
        wave1.Add(enemy);

        enemy = new EnemySpawnStruct(220, 2, 2, 1);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(220, 4, 1, 2);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(220, 3, 1, 1);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(221, 0, 3, 2);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(222, 3, 2, 2);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(222, 4, 2, 1);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(222, 8, 1, 2);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(223, 1, 2, 2);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(224, 0, 1, 4);
        wave1.Add(enemy);

        //Bossfight 240 - ...
        enemy = new EnemySpawnStruct(245, 3, 1, 4);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(250, 3, 1, 5);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(255, 3, 1, 4);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(260, 3, 1, 5);
        wave1.Add(enemy);
    }

    private void Update() {

        gameTimer += Time.deltaTime;


        if (wave1.Count > 0) {
            if (wave1.Count > 0 && wave1[0].GetTimeStamp() < gameTimer) {
                SpawnEnemy(wave1[0].GetUnitIndex(), wave1[0].GetUnitCount(), wave1[0].GetSpawner());
                wave1.RemoveAt(0);
            }
        }
    }

    public void SpawnEnemy(int unitIndex, int unitCount, int spawner) {

        List<int> randomNumbers = new List<int>();
        
        if(spawner == 1) {
            spawnerList = stationarySpawners;
        } else if (spawner == 2) {
            spawnerList = movingSpawners;
        } else if (spawner == 3) {
            spawnerList = leftSpawner;
        } else if (spawner == 4) {
            spawnerList = rightSpawner;
        }

        if (unitCount >= 3) {
            for (int i = 0; i < unitCount; i++) {
                int j;
                if(spawner == 1 || spawner == 2) {
                    j = i;
                } else {
                    j = Random.Range(0, spawnerList.Count);
                }
                Instantiate(enemies[unitIndex], spawnerList[j].transform.position, Quaternion.identity);
            }
        } else if (unitCount >= 2) {
            for (int i = 0; i < unitCount; i++) {
                int j;
                if (spawner == 1 || spawner == 2) {
                    j = i;
                } else {
                    j = Random.Range(0, spawnerList.Count);
                }
                Instantiate(enemies[unitIndex], spawnerList[j].transform.position, Quaternion.identity);
            }
            
        } else {
            for (int i = 0; i < unitCount; i++) {
                int j = Random.Range(0, spawnerList.Count);
                Instantiate(enemies[unitIndex], spawnerList[j].transform.position, Quaternion.identity);
            }
        }
    }
}
