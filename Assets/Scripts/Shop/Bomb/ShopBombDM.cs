using System.Collections.Generic;
using UnityEngine;


/*
    bolt adatbázis adatok kezelés
*/
public class ShopBombDM : DataManager<ShopItem>
{
    #region Singelton
    private static ShopBombDM _instance;
    public static ShopBombDM Instance()
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

    [Header("Shop bomb DM settings")]
    [SerializeField] ShopBomb[] shopBombs;                          // shopBombs adatok
    const string LoadShopBombKey = "LoadShopBombKey";


    // shop bomb dm adatainak beállítása
    public void Setup()
    {
        Load(LoadShopBombKey);
        if (datas == null || datas.Count == 0)
        {
            SetBaseDatas();
        }
        else
        {
            OverrideBaseDatas();
        }
    }

    // alap adatok betöltés
    void SetBaseDatas()
    {
        for (int i = 0; i < shopBombs.Length; i++)
        {
            datas.Add(shopBombs[i].shopItem);
        }
    }

    // korábban már volt mentett adatt azt töltöm be.
    void OverrideBaseDatas()
    {
        for (int i = 0; i < shopBombs.Length; i++)
        {
            shopBombs[i].shopItem = datas[i];
        }
    }

    // egy adott bomba kikérése
    public ShopBomb GetShopBombWithIndex(int index)
    {
        index = Mathf.Clamp(index, 0, datas.Count - 1);
        return shopBombs[index];
    }

    // minden megvásárolt bomba kikérése
    public BombItem[] GetAllBuyBombItem()
    {
        List<BombItem> result = new();
        foreach (ShopBomb item in shopBombs)
        {
            if (item.shopItem.isBuy)
            {
                result.Add(item.bombItem);
            }
        }
        return result.ToArray();
    }

    // Adatbázis mérete lekérdezése
    public int GetShopBombCount()
    {
        return shopBombs.Length;
    }

    // Adatok modosítása
    public override void SetData(ShopItem value, int index)
    {
        datas[index] = value;
        shopBombs[index].shopItem = value;
        Save(LoadShopBombKey);
    }
}

[System.Serializable]
public class ShopBomb
{
    public ShopItem shopItem;
    public BombItem bombItem;
}
