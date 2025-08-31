using System;
using UnityEngine;

[RequireComponent(typeof(DropBomb))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMoving))]
[RequireComponent(typeof(PlayerTrigger))]

/*
    Repülö gépepre kell tenni
*/
public class PlayerController : MonoBehaviour
{
    [Header("Player controller setting")]
    public float speed;
    public int numberOfBombs;
    public int capacity;
    public bool isBackThere;

    DropBomb _dropBomb;                         // Hívatkozás a bomba ledobásra
    PlayerInput _playerInput;                   // Hívatkozás a player inputra
    PlayerMoving _playerMoving;                 // Hívatkozás a player mozgásra
    PlayerTrigger _playerTrigger;               // Hívatkozás a player triggerre
    GameManager _gameManager;


    // Hívatkozás begyüjtés
    void Awake()
    {
        _dropBomb = GetComponent<DropBomb>();
        _playerInput = GetComponent<PlayerInput>();
        _playerMoving = GetComponent<PlayerMoving>();
        _playerTrigger = GetComponent<PlayerTrigger>();
    }

    // Kezdő beállítások
    public void Setup(GameManager gameManager, PlayerData playerData, BuildingLevel buildingLevel, Vector3[] dropPos, BombItem[] bombItems, Action myDelegate)
    {
        _gameManager = gameManager;
        _dropBomb.Setup(this, playerData, dropPos, bombItems, myDelegate);

        _playerInput.Setup(_dropBomb);
        _playerMoving.Setup(this, buildingLevel, playerData);
        _playerTrigger.Setup(this);
    }

    // nyert a játékos PlayerMoving hívja
    public void PlayerWinner()
    {
        _gameManager.GameWinner();
    }

    // Vesztett a PlayerTrigger hívja
    public void PlayerDie()
    {
        // lehet várni kell egy kicsit és csak utána tovább menni.
        _playerMoving.StopMoving();
        _gameManager.GameOver();
    }
}
