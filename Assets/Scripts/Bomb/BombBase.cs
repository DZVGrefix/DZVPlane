using UnityEngine;


/*
    Alap bomba nem sincsál semmit csak le esik és amíg tart a pustitás hatás addig rombol
*/
public class BombBase : Bomb
{
    // Kezdő beállítások 
    public override void Setup(DropBomb value)
    {
        base.Setup(value);
    }

    // Ütközés detektálás
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.TryGetComponent(out BuildingHealth buildingHealth))
        {
            destructiveEffect -= BombHit(buildingHealth);
            if (destructiveEffect <= 0)
            {
                BombEffect();
                BombDestroy();
            }
        }
    }
}
