using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


/*
    Repcsi modelre kell rátanni mozgatás
*/
public class PlayerMoving : MonoBehaviour
{
    //[Header("Player moving settings")]
    //[SerializeField] float maxSpeed = 5f;           // Repülő sebessége

    /* player data adatból ami rám vonatkozik az a: 
        - sebesség - PlaneSpeedReduction
        - újra ugyan az a magaság - FreezeHeight
    */
    int _freezeHeight;
    Vector3 _finishPos;
    Vector3 _stop;
    Vector3 _start;
    Vector3 _target;
    float _speed;
    PlayerController _playerController;
    IEnumerator _playerMoving;





    // Kezdő értékek beállítása
    public void Setup(PlayerController playerController, BuildingLevel buildingLevel, PlayerData playerData)
    {
        _freezeHeight = playerData.FreezeHeight;
        _finishPos = buildingLevel.finishPlane;

        _start = buildingLevel.startPlane;
        _stop = buildingLevel.stopPlane;
        transform.position = _start;
        _target = _stop;

        _speed = playerController.speed * playerData.PlaneSpeedReduction;
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
            transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _target) < .001f)
            {
                UpdateMoving();
            }

            yield return null;
        }
        _playerController.PlayerWinner();
    }

    // új mozgási adatok számolása
    bool isRight = true;
    void UpdateMoving()
    {
        // ha van feloldva hogy maradja egy magasságon akkor csökkentem az értéket
        if (_freezeHeight > 0)
        {
            _freezeHeight--;
        }
        else
        {
            _stop = new Vector3(_stop.x, _stop.y - 1, 0);
            _start = new Vector3(_start.x, _start.y - 1, 0);
        }
        // oda vissza menés be van kapcsolva
        if (_playerController.isBackThere)
        {
            if (isRight)
            {
                isRight = !isRight;
                transform.position = _stop;
                _target = _start;
            }
            else
            {
                isRight = !isRight;
                transform.position = _start;
                _target = _stop;
            }
        }
        else
        {
            transform.position = _start;
            _target = _stop;
        }
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
