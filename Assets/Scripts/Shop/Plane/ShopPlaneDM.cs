using UnityEngine;


/*
    Shop plane adatok
*/
public class ShopPlaneDM : DataManager<ShopItem>
{
    #region Singelton
    private static ShopPlaneDM _instance;
    public static ShopPlaneDM Instance()
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

    [Header("Shop plane dm settings")]
    [SerializeField] ShopPlane[] shopPlanes;                        // repcsik listája
    const string LoadShopPlaneKey = "LoadShopPlaneKey";


    // Kezdő adatok betöltése
    public void Setup()
    {
        Load(LoadShopPlaneKey);
        if (datas == null || datas.Count == 0)
        {
            SetBaseDatas();
        }
        else
        {
            OverrideBaseDatas();
        }
    }

    // Adatbázis alap adatok beállítások
    void SetBaseDatas()
    {
        for (int i = 0; i < shopPlanes.Length; i++)
        {
            datas.Add(shopPlanes[i].shopItem);
        }
    }

    // Adatok betöltése egy mentésből
    void OverrideBaseDatas()
    {
        for (int i = 0; i < shopPlanes.Length; i++)
        {
            shopPlanes[i].shopItem = datas[i];
        }
    }

    // Egy repülö kikérése a megadott index alapján
    public ShopPlane GetShopPlaneWithIndex(int index)
    {
        index = Mathf.Clamp(index, 0, datas.Count - 1);
        return shopPlanes[index];
    }

    // Repülö adatbázis méret lekérdezés
    public int GetShopPlaneCount()
    {
        return shopPlanes.Length;
    }

    // A megadott indexű repcsit beállítom alapértelmezetnek
    #warning "Egy aktiv repcsi kiválasztása"
    public void SetActivePlane(int index)
    {
        // kikapcsolok mindenkit
        for (int i = 0; i < shopPlanes.Length; i++)
        {
            datas[i].isUsePlane = false;
            shopPlanes[i].shopItem.isUsePlane = false;
        }
        datas[index].isUsePlane = true;
        shopPlanes[index].shopItem.isUsePlane = true;
        Save(LoadShopPlaneKey);
    }

    // modosított adatok mentése
    public override void SetData(ShopItem value, int index)
    {
        datas[index] = value;
        shopPlanes[index].shopItem = value;
        Save(LoadShopPlaneKey);
    }
}

[System.Serializable]
public class ShopPlane
{
    public ShopItem shopItem;
    public ShopPlaneItem shopPlaneItem;
}
