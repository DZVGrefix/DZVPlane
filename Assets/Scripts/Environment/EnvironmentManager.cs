using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class EnvironmentManager : MonoBehaviour
{
    [Header("Mountain settings")]
    [SerializeField] Mountain[] mountains;
    [SerializeField] GameObject ground;

    [Header("Street settings")]
    [SerializeField] GameObject[] streets;
    [SerializeField] int numberOfStreet;

    [Header("Cloud settings")]
    [SerializeField] GameObject[] cloudPrefabs;
    [SerializeField] int numberOfClouds;
    [SerializeField] float speedMin;
    [SerializeField] float speedMax;
    [SerializeField] float endXPosition;
    [SerializeField] float reSpawnTimer;


    Dictionary<string, Color> _colors;
    ObjectPooler _objectPooler;
    float _spawnTimer;


    public void Setup(ObjectPooler objectPooler)
    {
        _objectPooler = objectPooler;
        CreateStreet();
        CreateColors();
        CreateMountain(GetRanadomColor());
        CreateClouds();

        _spawnTimer = reSpawnTimer;
        StartCoroutine(SpawnCloud());
    }

    IEnumerator SpawnCloud()
    {
        while (true)
        {
            if (_spawnTimer < 0)
            {
                _spawnTimer = reSpawnTimer;
                NewCloud(true);
            }
            _spawnTimer -= Time.deltaTime;
            yield return null;
        }
    }


    // utcák létrehozás
    void CreateStreet()
    {
        GameObject gameObject;
        for (int i = 0; i < numberOfStreet; i++)
        {
            if (i == 0)
            {
                gameObject = _objectPooler.SpawnFromPool(streets[Random.Range(0, streets.Length)].name, new Vector3(i, 0, 0), Quaternion.identity, transform);
            }
            else
            {
                gameObject = _objectPooler.SpawnFromPool(streets[Random.Range(0, streets.Length)].name, new Vector3(i, 0, 0), Quaternion.identity, transform);
                gameObject = _objectPooler.SpawnFromPool(streets[Random.Range(0, streets.Length)].name, new Vector3(-i, 0, 0), Quaternion.identity, transform);
            }
        }
    }


    // Hegy létrehozás
    void CreateMountain(Color value)
    {
        //
        Mountain m = Instantiate(mountains[0], Vector3.zero, Quaternion.identity, transform);
        int count = m.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            Transform t = m.transform.GetChild(i);
            Renderer meshRender = t.GetComponent<Renderer>();
            meshRender.materials[0].color = value;
        }
        Renderer meshRender2 = ground.GetComponent<Renderer>();
        meshRender2.sharedMaterial.color = value;
    }



    // Felhők létrehozás
    void CreateClouds()
    {
        //
        for (int i = 0; i < numberOfClouds; i++)
        {
            NewCloud();
        }
    }
    void NewCloud(bool isNewCloud = false)
    {
        //
        Vector3 pos = RandomCloudPosition();
        if (isNewCloud)
        {
            pos.x = -endXPosition;
        }
        GameObject go = _objectPooler.SpawnFromPool(cloudPrefabs[Random.Range(0, cloudPrefabs.Length)].name, pos, Quaternion.identity, transform);
        Cloud cloud = go.GetComponent<Cloud>();
        cloud.Setup(_objectPooler, Random.Range(speedMin, speedMax), endXPosition);
    }
    Vector3 RandomCloudPosition()
    {
        // poziciok:
        /*
            x: -35 - 35
            y: 15 - 22
            Z: 10 - 25
        */
        float xCoord = Random.Range(-endXPosition, endXPosition);
        float yCoord = Random.Range(15f, 22f);
        float zCoord = Random.Range(10f, 25f);
        return new Vector3(xCoord, yCoord, zCoord);
    }


    // random szinek kezelése
    void CreateColors()
    {
        _colors = new Dictionary<string, Color>
        {
            { "BASE", new Color(132f / 255, 112f / 255, 88f / 255)},
            { "GREEN01", new Color(51f / 255, 102f / 255, 51f / 255)},
            { "GREEN02", new Color(102f / 255, 102f / 255, 0f / 255)},
            { "GREEN03", new Color(51f / 255, 102f / 255, 102f / 255)},
            { "ORANGE01", new Color(204f / 255, 102f / 255, 0f / 255)},
            //{ "ORANGE02", new Color(204f / 255, 0f / 255, 0f / 255)},
            //{ "ORANGE03", new Color(255f / 255, 51f / 255, 0f / 255)},
            { "LIGHTBROWN", new Color(255f / 255, 204f / 255, 153f / 255)}
        };
    }
    Color GetRanadomColor()
    {
        string[] keys = _colors.Keys.ToArray();
        string key = keys[Random.Range(0, keys.Length)];
        Debug.Log(key);
        return _colors[key];
    }
}
