using UnityEngine;
[System.Serializable]
public class BuildingLevel
{
    public int level;
    public int numberOfBuilding;                // max 39
    public int minHeight;                       // legalább 2
    public int maxHeight;                       // max 12
    public bool isRebuilding;                   // újra építhetőség miatt
    public float reTimer;                       // újra inditási idő
    public int reConstructionDistance;          // újra építési távolság a repülötől ha ezen távon belül van a repcsi nem építek
    public Vector3 startPlane;                  // -22 17 0
    public Vector3 stopPlane;                   // 22 17 0
    public Vector3 finishPlane;                 // (numberOfBuilding / 2) + 1, 0 , 0

    public BuildingLevel(int level, int numberOfBuilding, int minHeight, int maxHeight, bool isRebuilding, float reTimer, int reConstructionDistance, Vector3 startPlane, Vector3 stopPlane, Vector3 finishPlane)
    {
        this.level = level;
        this.numberOfBuilding = numberOfBuilding;
        this.minHeight = minHeight;
        this.maxHeight = maxHeight;
        this.isRebuilding = isRebuilding;
        this.reTimer = reTimer;
        this.reConstructionDistance = reConstructionDistance;
        this.startPlane = startPlane;
        this.stopPlane = stopPlane;
        this.finishPlane = finishPlane;
    }
}
