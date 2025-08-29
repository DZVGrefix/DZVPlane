using UnityEngine;


/*
    Building adatok kezelés
*/
public class BuildingDM : DataManager<BuildingLevel>
{
    #region Singelton
    private static BuildingDM _instance;
    public static BuildingDM Instance()
    {
        return _instance;
    }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    [Header("Building DM settings")]
    [SerializeField] BuildingLevel[] buildingLevels;            // ezek tartalmazzák a pályákat.
    const string LoadBuildingKey = "LoadBuildingKey";

    // kezdő értékek beállítása
    public void Setup()
    {
        if (buildingLevels == null || buildingLevels.Length == 0)
        {
            CreateBuildingLevelArray();
        }
        Load(LoadBuildingKey, buildingLevels);
    }

    // ki kérem a megadott level pályát 
    public BuildingLevel GetBuildingLevelWithIndex(int index)
    {
        index = Mathf.Clamp(index, 0, datas.Count - 1);
        return datas[index];
    }

    // nem mentem el file-ba a pálya adatokat ezért ezt nem használom.
    public override void SetData(BuildingLevel value, int index)
    {
        // itt nem kell modosítani semmit
    }

    void OnValidate()
    {
        CreateBuildingLevelArray();
    }

    // pálya adatok létrehozzás itt könyebb volt mint az editorban :) 
    // isRepairBuilding - még nem használom, ha használni fogom akkor ez fogja figyelni hogy újra építhetek-e lebombázott épület elemeket.
    void CreateBuildingLevelArray()
    {
        buildingLevels = new BuildingLevel[] {
            new(level: 1, numberOfBuilding: 13, minHeight: 3, maxHeight: 6, isRepairBuilding: false, startPlane: new Vector3(-22, 10, 0), stopPlane: new Vector3(22, 10, 0), finishPlane: new Vector3(7, 0, 0)),
            new(level: 2, numberOfBuilding: 13, minHeight: 3, maxHeight: 6, isRepairBuilding: false, startPlane: new Vector3(-22, 10, 0), stopPlane: new Vector3(22, 10, 0), finishPlane: new Vector3(7, 0, 0)),
            new(level: 3, numberOfBuilding: 15, minHeight: 3, maxHeight: 6, isRepairBuilding: false, startPlane: new Vector3(-22, 10, 0), stopPlane: new Vector3(22, 10, 0), finishPlane: new Vector3(8, 0, 0)),
            new(level: 4, numberOfBuilding: 15, minHeight: 3, maxHeight: 7, isRepairBuilding: false, startPlane: new Vector3(-22, 11, 0), stopPlane: new Vector3(22, 11, 0), finishPlane: new Vector3(8, 0, 0)),
            new(level: 5, numberOfBuilding: 17, minHeight: 3, maxHeight: 7, isRepairBuilding: false, startPlane: new Vector3(-22, 11, 0), stopPlane: new Vector3(22, 11, 0), finishPlane: new Vector3(9, 0, 0)),
            new(level: 6, numberOfBuilding: 17, minHeight: 3, maxHeight: 7, isRepairBuilding: false, startPlane: new Vector3(-22, 11, 0), stopPlane: new Vector3(22, 11, 0), finishPlane: new Vector3(9, 0, 0)),
            new(level: 7, numberOfBuilding: 19, minHeight: 3, maxHeight: 8, isRepairBuilding: false, startPlane: new Vector3(-22, 12, 0), stopPlane: new Vector3(22, 12, 0), finishPlane: new Vector3(10, 0, 0)),
            new(level: 8, numberOfBuilding: 19, minHeight: 3, maxHeight: 8, isRepairBuilding: false, startPlane: new Vector3(-22, 12, 0), stopPlane: new Vector3(22, 12, 0), finishPlane: new Vector3(10, 0, 0)),
            new(level: 9, numberOfBuilding: 21, minHeight: 3, maxHeight: 8, isRepairBuilding: false, startPlane: new Vector3(-22, 12, 0), stopPlane: new Vector3(22, 12, 0), finishPlane: new Vector3(11, 0, 0)),
            new(level: 10, numberOfBuilding: 21, minHeight: 3, maxHeight: 9, isRepairBuilding: false, startPlane: new Vector3(-22, 13, 0), stopPlane: new Vector3(22, 13, 0), finishPlane: new Vector3(11, 0, 0)),
            new(level: 11, numberOfBuilding: 23, minHeight: 3, maxHeight: 9, isRepairBuilding: false, startPlane: new Vector3(-22, 13, 0), stopPlane: new Vector3(22, 13, 0), finishPlane: new Vector3(12, 0, 0)),
            new(level: 12, numberOfBuilding: 23, minHeight: 3, maxHeight: 9, isRepairBuilding: false, startPlane: new Vector3(-22, 13, 0), stopPlane: new Vector3(22, 13, 0), finishPlane: new Vector3(12, 0, 0)),
            new(level: 13, numberOfBuilding: 25, minHeight: 3, maxHeight: 10, isRepairBuilding: false, startPlane: new Vector3(-22, 14, 0), stopPlane: new Vector3(22, 14, 0), finishPlane: new Vector3(13, 0, 0)),
            new(level: 14, numberOfBuilding: 25, minHeight: 3, maxHeight: 10, isRepairBuilding: false, startPlane: new Vector3(-22, 14, 0), stopPlane: new Vector3(22, 14, 0), finishPlane: new Vector3(13, 0, 0)),
            new(level: 15, numberOfBuilding: 27, minHeight: 3, maxHeight: 10, isRepairBuilding: false, startPlane: new Vector3(-22, 14, 0), stopPlane: new Vector3(22, 14, 0), finishPlane: new Vector3(14, 0, 0)),
            new(level: 16, numberOfBuilding: 27, minHeight: 3, maxHeight: 11, isRepairBuilding: false, startPlane: new Vector3(-22, 15, 0), stopPlane: new Vector3(22, 15, 0), finishPlane: new Vector3(14, 0, 0)),
            new(level: 17, numberOfBuilding: 29, minHeight: 3, maxHeight: 11, isRepairBuilding: false, startPlane: new Vector3(-22, 15, 0), stopPlane: new Vector3(22, 15, 0), finishPlane: new Vector3(15, 0, 0)),
            new(level: 18, numberOfBuilding: 29, minHeight: 3, maxHeight: 11, isRepairBuilding: false, startPlane: new Vector3(-22, 15, 0), stopPlane: new Vector3(22, 15, 0), finishPlane: new Vector3(15, 0, 0)),
            new(level: 19, numberOfBuilding: 31, minHeight: 3, maxHeight: 12, isRepairBuilding: false, startPlane: new Vector3(-22, 16, 0), stopPlane: new Vector3(22, 16, 0), finishPlane: new Vector3(16, 0, 0)),
            new(level: 20, numberOfBuilding: 31, minHeight: 3, maxHeight: 12, isRepairBuilding: false, startPlane: new Vector3(-22, 16, 0), stopPlane: new Vector3(22, 16, 0), finishPlane: new Vector3(16, 0, 0)),
            new(level: 21, numberOfBuilding: 39, minHeight: 6, maxHeight: 12, isRepairBuilding: false, startPlane: new Vector3(-22, 16, 0), stopPlane: new Vector3(22, 16, 0), finishPlane: new Vector3(20, 0, 0))
        };
    }
}
