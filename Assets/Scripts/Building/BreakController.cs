using UnityEngine;

/*
    Ezt a szétrombolt model parentjére teszemrá
*/
public class BreakController : MonoBehaviour
{
    Transform _parentItem;              // Parent ami tartalmazza a törött Modeleket

    
    // Ezzel indítom el a törött modelek animálását
    public void Setup()
    {
        _parentItem = transform.GetChild(0);
        for (int i = 0; i < _parentItem.childCount; i++)
        {
            _parentItem.GetChild(i).GetComponent<IHouseBreak>().OnHouseBreak();
        }
    }

    // Ha minden elem eltönt elpusztítom ezt is.
    void Update()
    {
        if (_parentItem.childCount == 0)
        {
            Destroy(gameObject);
        }
    }
}
