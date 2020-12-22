using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EnemySpawnStruct {

    float timeStamp;
    int unitIndex;
    int unitCount;
    int spawner;

    public EnemySpawnStruct(float timeStamp, int unitIndex, int unitCount, int spawner) {
        this.timeStamp = timeStamp;
        this.unitIndex = unitIndex;
        this.unitCount = unitCount;
        this.spawner = spawner;
    }

    public float GetTimeStamp() {
        return timeStamp;
    }
    public int GetUnitCount() {
        return unitCount;
    }
    public int GetUnitIndex() {
        return unitIndex;
    }
    public int GetSpawner() {
        return spawner;
    }
}
