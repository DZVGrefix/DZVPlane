using System.Collections;
using UnityEngine;

/*
    Egy egységet zuhan a bomba és utána vizszíntes haladást vesz fel
*/
public class BombRaket : Bomb
{
    [Header("Bomb raket settings")]
    [SerializeField] int bombFall = 1;                  // Mennyit zuhanjon függölegesen
    Vector3 _startHorizontalPos;                         // meddig zuhanjon


    // Kezdő beállítások 
    public override void Setup(DropBomb value)
    {
        base.Setup(value);
        _startHorizontalPos = new(transform.position.x, transform.position.y - bombFall, transform.position.z);
        StartCoroutine(MovingCycle());
    }

    // Ütközés detektálás
    protected override void OnTriggerEnter(Collider other)
    {
        //Debug.Log("BombBase::OnTriggerEnter");
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

    // Rakéta mozgatása
    IEnumerator MovingCycle()
    {
        while (transform.position != _startHorizontalPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, _startHorizontalPos, Time.deltaTime * fallSpeed);
            yield return null;
        }

        while (true)
        {
            transform.Translate((fallSpeed * 2) * Time.deltaTime * Vector3.right);
            yield return null;
        }
    }
}
