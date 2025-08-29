[System.Serializable]
public class ShopItem
{
    public bool isShowItemInShop;                   // ha igaz akkor a shopban is megjelenik vásárlásra
    public int unlockLevel;                         // aktiv lesz a shopban ha elérem ezt a pályaszámot.
    public int price;                               // ennyibe kerül 
    public string itemName;
    public string itemDescription;
    public bool isBuy;                              // ha igaz akkor megvásároltam 
    public bool isUsePlane;                         // ezt a repcsit használom
}
