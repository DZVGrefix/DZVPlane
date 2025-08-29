using UnityEngine;


/*
    A bomba nem pusztít amíg zuhan de ha X házon keresztűl ment akkor repeszekkel pusztít
*/
public class BombDrillStar : Bomb
{
    [Header("Bomb drill star settings")]
    [SerializeField] string poolNameKey = "Shrapnel";           // repesz neve a poolban
    ObjectPooler _objectPooler;                                 // hívatkozás a poolra


    // Kezdő beállítások 
    public override void Setup(DropBomb dropBomb)
    {
        base.Setup(dropBomb);
        _objectPooler = ObjectPooler.Instance();
    }

    // Ütközés detektálás
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BuildingHealth buildingHealth))
        {
            destructiveEffect--;
            if (destructiveEffect <= 0)
            {
                BombEffect();

                BombDestroy(other.transform.position, false);
            }
        }
        if (other.TryGetComponent(out DeathZone deathZone))
        {
            BombDestroy(transform.position + (Vector3.up / 2), false);
        }
    }

    // bomba pusztálsa előtt a repeszek létrehozása
    void BombDestroy(Vector3 position, bool isBuilding = true)
    {
        CreateShrapnel(position);
        base.BombDestroy(isBuilding);
    }

    // Repeszek létrehozása
    void CreateShrapnel(Vector3 pos)
    {
        GameObject shrapnel = _objectPooler.SpawnFromPool(poolNameKey, pos, transform.rotation, null);
        shrapnel.GetComponent<Shrapnel>().Setup(bombDrop, Vector3.right, pos);

        shrapnel = _objectPooler.SpawnFromPool(poolNameKey, pos, transform.rotation, null);
        shrapnel.GetComponent<Shrapnel>().Setup(bombDrop, Vector3.left, pos);

        shrapnel = _objectPooler.SpawnFromPool(poolNameKey, pos, transform.rotation, null);
        shrapnel.GetComponent<Shrapnel>().Setup(bombDrop, Vector3.up, pos);

        shrapnel = _objectPooler.SpawnFromPool(poolNameKey, pos, transform.rotation, null);
        shrapnel.GetComponent<Shrapnel>().Setup(bombDrop, Vector3.down, pos);
    }
}
