using System.Collections.Generic;
using UnityEngine;

/*
    TÖRÖLNI MÁR NEM EZT HASZNÁLOM
*/


/*
    Shop manager - még nincs kész.
*/
public class ShopManager : MonoBehaviour
{/*
    private ShopBombItem[] shopBombItems;
    private ShopPlaneItem[] shopPlaneItems;


    // minden bomba elem kikérése - nem jó majd a repeszeket ki kell vennem belőle
    public ShopBombItem[] AllBombOutOfShop()
    {
        return shopBombItems;
    }

    // kikérem azokat az bombákat amiket megvettem
    public ShopBombItem[] BuyBombOutOfShop()
    {
        List<ShopBombItem> result = new();
        foreach (ShopBombItem item in shopBombItems)
        {
            if (item.isBuy)
            {
                result.Add(item);
            }
        }
        return result.ToArray();
    }

    // kikérem az "Index" repcsit.
    public ShopPlaneItem PlaneOutOfShop(int index)
    {
        if (index >= 0 && index < shopPlaneItems.Length)
        {
            return shopPlaneItems[index];
        }
        return null;
    }

    // kikérem az összes repcsit
    public ShopPlaneItem[] AllPlaneOutOfShop()
    {
        return shopPlaneItems;
    }*/
}
