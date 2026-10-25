using System;
using UnityEngine;

[System.Serializable]
public struct DataToSave
{
    public int sortedBoxCount;
    public float boxSpawnSpeed; 

    private BoxSpawner boxSpawner;
    private ConveyorBelt conveyorBelt;

    
    public void SetSavedData(BoxSpawner boxSpawner, ConveyorBelt conveyorBelt)
    {
        sortedBoxCount = conveyorBelt.destroyedBoxCount;
        boxSpawnSpeed = boxSpawner.boxSpawnSpeed;

    }

    

    public void LoadSavedData(BoxSpawner boxSpawner, ConveyorBelt conveyorBelt)
    {
        conveyorBelt.destroyedBoxCount = sortedBoxCount;
        boxSpawner.boxSpawnSpeed = boxSpawnSpeed;
    }
}
