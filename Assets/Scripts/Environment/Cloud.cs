using System.Collections;
using UnityEngine;

/*
    FELHŐ BEÁLLÍTÁSOK
*/
public class Cloud : MonoBehaviour
{
    //ObjectPooler _objectPooler;
    //float _cloudSpeed;
    //Vector3 _endPos;

    // felhők indítása
    public void Setup(ObjectPooler objectPooler, float speed, float endX)
    {
        //_objectPooler = objectPooler;
        //_cloudSpeed = speed;
        //_endPos = new Vector3(endPos, transform.position.y, transform.position.z);

        StartCoroutine(Moving(objectPooler, speed, new Vector3(endX, transform.position.y, transform.position.z)));
    }

    IEnumerator Moving(ObjectPooler objectPooler, float speed, Vector3 endPos)
    {
        while (Vector3.Distance(transform.position, endPos) > .01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
            yield return null;
        }
        objectPooler.ObjectToPool(gameObject);
    }
}
