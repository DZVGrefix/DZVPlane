using UnityEngine;

/*
    Player ütközés figyelés
*/
public class PlayerTrigger : MonoBehaviour
{
    PlayerController _playerController;

    //Kezdő érték beállítása
    public void Setup(PlayerController playerController)
    {
        _playerController = playerController;
    }

    // ütközés figyelés
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BuildingItem buildingItem))
        {
            //#warning "JÁTÉKOS HALÁL EFFECT"
            _playerController.PlayerDie();
        }
    }
}
