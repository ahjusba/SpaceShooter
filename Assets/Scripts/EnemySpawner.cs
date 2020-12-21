using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour {
    public GameObject rockSmall;            //0
    public GameObject rockMedium;           //1
    public GameObject rockBig;              //2

    public GameObject fighter;              //3
    public GameObject mineLayer;            //4

    List<EnemySpawnStruct> wave1 = new List<EnemySpawnStruct>();

    public List<GameObject> spawners = new List<GameObject>();
    List<GameObject> enemies = new List<GameObject>();

    float gameTimer = 0f;
    EnemySpawnStruct enemy;

    private void Awake() {

        enemies.Add(rockSmall);
        enemies.Add(rockMedium);
        enemies.Add(rockBig);
        enemies.Add(fighter);
        enemies.Add(mineLayer);

        enemy = new EnemySpawnStruct(2, 0, 1);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(3, 0, 1);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(4, 0, 1);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(6, 2, 1);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(6, 0, 3);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(12, 3, 2);
        wave1.Add(enemy);

        enemy = new EnemySpawnStruct(20, 4, 1);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(22, 4, 1);
        wave1.Add(enemy);
        enemy = new EnemySpawnStruct(24, 4, 1);
        wave1.Add(enemy);

    }

    private void Update() {

        gameTimer += Time.deltaTime;


        if (wave1.Count > 0) {
            if (wave1.Count > 0 && wave1[0].GetTimeStamp() < gameTimer) {
                SpawnEnemy(wave1[0].GetUnitIndex(), wave1[0].GetUnitCount());
                wave1.RemoveAt(0);
            }
        }
    }

    public void SpawnEnemy(int unitIndex, int unitCount) {

        if (unitCount <= 3) {
            for (int i = 0; i < unitCount; i++) {
                Instantiate(enemies[unitIndex], spawners[i].transform.position, Quaternion.identity);
            }
        } else {
            print("Tried  to spawn more than 3 units at once");
        }
    }
}
