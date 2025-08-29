using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


/*
    Repcsi modelre kell rátanni mozgatás
*/
public class PlayerMoving : MonoBehaviour
{
    [Header("Player moving settings")]
    [SerializeField] float maxSpeed = 5f;           // Repülő sebessége



    /* player data adatból ami rám vonatkozik az a: 
        - sebesség - PlaneSpeedReduction
        - újra ugyan az a magaság - FreezeHeight
    */
    int freezeHeight;

    Vector3 _finishPos;

    Vector3 _stop;
    Vector3 _start;
    float _speed;
    PlayerController _playerController;
    IEnumerator _playerMoving;



    // Kezdő értékek beállítása
    public void Setup(PlayerController playerController, BuildingLevel buildingLevel, PlayerData playerData)
    {
        PlayerData p = PlayerDM.Instance().GetPlayerData();
        freezeHeight = playerData.FreezeHeight;

        _finishPos = buildingLevel.finishPlane;

        _start = buildingLevel.startPlane;
        _stop = buildingLevel.stopPlane;
        transform.position = _start;

        _speed = maxSpeed * playerData.PlaneSpeedReduction;
        _playerController = playerController;

        _playerMoving = Moving();
        StartCoroutine(_playerMoving);
    }

    public void StopMoving()
    {
        StopCoroutine(_playerMoving);
    }

    IEnumerator Moving()
    {
        while (Vector3.Distance(transform.position, _finishPos) > .1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, _stop, _speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _stop) < .001f)
            {
                UpdateMoving();
            }

            yield return null;
        }
        _playerController.PlayerWinner();
    }

    // új mozgási adatok számolása
    void UpdateMoving()
    {
        if (freezeHeight <= 0)
        {
            _stop = new Vector3(_stop.x, _stop.y - 1, 0);
            _start = new Vector3(_start.x, _start.y - 1, 0);
        }
        else
        {
            freezeHeight--;
        }        
        transform.position = _start;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_stop, 1f);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_start, 1f);
        Gizmos.DrawWireSphere(_finishPos, 1f);
    }
}
