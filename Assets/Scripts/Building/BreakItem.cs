using System.Collections;
using UnityEngine;


/*
    ezt a töröt modelekre kell tenni
*/
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshCollider))]
public class BreakItem : MonoBehaviour, IHouseBreak
{
    [SerializeField] float upForce = 4f;                // Erő fel
    [SerializeField] float sideForce = 4f;              // erő oldalra
    [SerializeField] float scaleSpeed = 20f;            // milyen gyorsan zsugorodjon
    [SerializeField] float scaleMin = 10f;              // Méret alatt már eltüntettem inkább


    // BreakController - hívja meg és ezzel indul a kezdeti animálás
    public void OnHouseBreak()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        float xForce = Random.Range(-sideForce, sideForce);
        float yForce = Random.Range(upForce / 2f, upForce);
        float zForce = Random.Range(-sideForce, sideForce);

        Vector3 force = new(xForce, yForce, zForce);

        GetComponent<Rigidbody>().linearVelocity = force;

        StartCoroutine(BreakItemCycle());
    }

    // Méret csökkentés
    IEnumerator BreakItemCycle()
    {
        Vector3 scale = transform.localScale;
        while (IsScaleNotZero(scale))
        {
            scale = transform.localScale;
            scale.x -= Time.deltaTime * scaleSpeed;
            scale.y -= Time.deltaTime * scaleSpeed;
            scale.z -= Time.deltaTime * scaleSpeed;
            transform.localScale = scale;
            yield return null;
        }
        Destroy(gameObject);
    }

    // méret vizsgálat ha min scal alatt van akkor pusztítás
    bool IsScaleNotZero(Vector3 scale)
    {
        return (scale.x > scaleMin && scale.y > scaleMin && scale.z > scaleMin);
    }
}
