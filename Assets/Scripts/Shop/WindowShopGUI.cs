using TMPro;
using UnityEngine;
using UnityEngine.UI;


/*
    Shop GUI adatok megjelenitése
*/
public class WindowShopGUI : MonoBehaviour
{
    [SerializeField] TMP_Text headerText;
    [SerializeField] TMP_Text descriptionText;
    [SerializeField] Image planeImage;
    [SerializeField] TMP_Text speedText;
    [SerializeField] Image[] speedImages;
    [SerializeField] TMP_Text dropText;
    [SerializeField] Image[] dropImages;
    [SerializeField] TMP_Text capacityText;
    [SerializeField] Image[] capacityImages;
    [SerializeField] Button button;
    [SerializeField] TMP_Text buttonText;



    // meg adatot Tipus szerint feltölöm a megjelenitendő adatokat
    public void RefreshPlaneGUI<T>(T items, int level)
    {
        if (items is ShopPlane)
        {
            headerText.text = (items as ShopPlane).shopItem.itemName;
            descriptionText.text = (items as ShopPlane).shopItem.itemDescription;
            planeImage.sprite = (items as ShopPlane).shopPlaneItem.shopImage;
            speedText.text = "Speed:";
            SetImagesWithNumber(speedImages, (int)(items as ShopPlane).shopPlaneItem.speed);
            dropText.text = "Drop bomb:";
            SetImagesWithNumber(dropImages, (items as ShopPlane).shopPlaneItem.dropBomb);
            capacityText.text = "Capacity:";
            SetImagesWithNumber(capacityImages, (items as ShopPlane).shopPlaneItem.capacity);

            RefreshPlaneButtonGUI((items as ShopPlane), level);
        }
        else if (items is ShopBomb)
        {
            headerText.text = (items as ShopBomb).shopItem.itemName;
            descriptionText.text = (items as ShopBomb).shopItem.itemDescription;
            planeImage.sprite = (items as ShopBomb).bombItem.sprite;
            speedText.text = "Speed:";
            SetImagesWithNumber(speedImages, (int)(items as ShopBomb).bombItem.speed);
            dropText.text = "Bomb dmg:";
            SetImagesWithNumber(dropImages, (items as ShopBomb).bombItem.bombDmg);
            capacityText.text = "Shrapnel dmg:";
            SetImagesWithNumber(capacityImages, (items as ShopBomb).bombItem.shrapnelDmg);

            RefreshBombButtonGUI((items as ShopBomb), level);
        }

    }

    // Gomb státusz frissítések repcsi vásárlás esetére
    void RefreshPlaneButtonGUI(ShopPlane shopPlane, int level)
    {
        if (level < shopPlane.shopItem.unlockLevel)
        {
            button.interactable = false;
            buttonText.text = $"Level {shopPlane.shopItem.unlockLevel}";
        }
        else if (shopPlane.shopItem.isBuy)
        {
            button.interactable = true;
            buttonText.text = (shopPlane.shopItem.isUsePlane ? "Active" : "Use default");
        }
        else
        {
            button.interactable = true;
            buttonText.text = $"{shopPlane.shopItem.price}";
        }
    }

    // Gomb státusz frissítések bomba vásárlás esetére
    void RefreshBombButtonGUI(ShopBomb shopBomb, int level)
    {
        if (level < shopBomb.shopItem.unlockLevel)
        {
            button.interactable = false;
            buttonText.text = $"Level {shopBomb.shopItem.unlockLevel}";
        }
        else if (shopBomb.shopItem.isBuy)
        {
            button.gameObject.SetActive(false);
        }
        else
        {
            button.interactable = true;
            buttonText.text = $"{shopBomb.shopItem.price}";
        }
    }

    // Image adatok ezt majd másképp csinálom csak rajzolni kell hozzá
    #warning "kell image-t rajzolni hozzá"
    void SetImagesWithNumber(Image[] images, int number)
    {
        for (int i = 0; i < images.Length; i++)
        {
            if (i < number)
            {
                images[i].color = Color.red;
            }
            else
            {
                images[i].color = Color.gray;
            }
        }
    }

    // repcsi gomb frissítése
    public void SetPlaneButtonText(string value)
    {
        buttonText.text = value;
    }

    // bomba gomb frissítése
    public void SetBombButtonText(string value, bool isShow = true)
    {
        button.gameObject.SetActive(isShow);
        buttonText.text = value;
    }
}
