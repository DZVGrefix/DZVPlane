using System.Collections;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    ObjectPooler _objectPooler;
    float _cloudSpeed;
    Vector3 _endPos;


    public void Setup(ObjectPooler objectPooler, float speed, float endPos)
    {
        _objectPooler = objectPooler;
        _cloudSpeed = speed;
        _endPos = new Vector3(endPos, transform.position.y, transform.position.z);

        StartCoroutine(Moving());
    }

    IEnumerator Moving()
    {
        while (Vector3.Distance(transform.position, _endPos) > .01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, _endPos, _cloudSpeed * Time.deltaTime);
            yield return null;
        }
        _objectPooler.ObjectToPool(gameObject);
    }
}
