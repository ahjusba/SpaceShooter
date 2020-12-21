using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EnemySpawnStruct {

    float timeStamp;
    int unitIndex;
    int unitCount;

    public EnemySpawnStruct(float timeStamp, int unitIndex, int unitCount) {
        this.timeStamp = timeStamp;
        this.unitIndex = unitIndex;
        this.unitCount = unitCount;
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
}
