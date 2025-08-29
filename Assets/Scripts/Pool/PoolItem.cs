using UnityEngine;

[System.Serializable]
public class PoolItem
{
    public GameObject prefab;
    public int count;
}

[System.Serializable]
public class PItemBuilding : PoolItem
{
    public BuildingType buildingType;               // ez alapján szüröm az adatokat
}

/*
[System.Serializable]
public class PItemBomb : PoolItem
{
    public BombType bombType;
}
*/