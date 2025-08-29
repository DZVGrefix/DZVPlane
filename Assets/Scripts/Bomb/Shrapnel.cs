using System.Collections;
using UnityEngine;

/*
    Bomba robanás után létrejön a repesz és annak a beállításai
*/
public class Shrapnel : Bomb
{
    [Header("Shrapnel settings")]
    [SerializeField] float distance = 2f;               // ez a távolság után pusztul el a repesz
    [SerializeField] float speed = 10f;                 // sebessége

    Vector3 _direction = Vector3.zero;                  // irány amerre repül a repesz
    Vector3 _startPosition;                             // ahonnan indul a repesz
    //private ObjectPooler _objectPooler;                 // Pool hívatkozás


    // Kezdő beállítások 
    public void Setup(DropBomb value, Vector3 newDirection, Vector3 startPos/*, ObjectPooler objPooler*/)
    {
        base.Setup(value);
        _direction = newDirection;
        _startPosition = startPos;
        PlayerData playerData = PlayerDM.Instance().GetPlayerData();
        distance += playerData.PlusShrapnelDmg;
        StartCoroutine(MovingCycle());
    }

    // Ütközés detektálás
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.TryGetComponent(out BuildingHealth buildingHealth))
        {
            buildingHealth.DestroyItem();
        }
    }

    // repesz mozgatása
    IEnumerator MovingCycle()
    {
        // ha pozicióm távolabb van a kezdő pontomtól akkor pusztítom el a repeszt
        while (Vector3.Distance(transform.position, _startPosition) < distance)
        {
            transform.position += speed * Time.deltaTime * _direction;
            yield return null;
        }
        BombDestroy();
    }
}
