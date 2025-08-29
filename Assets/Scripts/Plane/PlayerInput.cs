using UnityEngine;


/*
    Játékos irányitási gombok
*/
[RequireComponent(typeof(DropBomb))]
public class PlayerInput : MonoBehaviour
{
    [Header("Player Input settings")]
    [SerializeField] KeyCode keyCode = KeyCode.Space;

    DropBomb _dropBomb;

    // kezdő érték beállítása
    public void Setup(DropBomb dropBomb)
    {
        _dropBomb = dropBomb;
    }

    // irányitások gomb nyomások figyelése
    void Update()
    {
        if (_dropBomb == null) return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            _dropBomb.Drop();
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetMouseButtonDown(1))
        {
            _dropBomb.PreviousBomb();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            _dropBomb.NextBomb();
        }
    }
}