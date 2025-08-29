using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


/*
    DataManager adatok mentése, betöltése, modósítás
*/
public abstract class DataManager<T> : MonoBehaviour
{
    protected List<T> datas = new();


    /* LOAD RÉSZ */
    protected void Load(string key)
    {
        string path = Application.persistentDataPath + $"/{key}";
        if (File.Exists(path))
        {
            LoadExtension(path);
        }
    }
    protected void Load(string key, T value)
    {
        string path = Application.persistentDataPath + $"/{key}";
        if (File.Exists(path))
        {
            LoadExtension(path);
        }
        else
        {
            datas.Add(value);
        }
    }
    protected void Load(string key, T[] values)
    {
        string path = Application.persistentDataPath + $"/{key}";
        if (File.Exists(path))
        {
            LoadExtension(path);
        }
        else
        {
            datas = values.ToList<T>();
        }
    }
    void LoadExtension(string path)
    {
        BinaryFormatter formatter = new();
        FileStream stream = new FileStream(path, FileMode.Open);
        datas = formatter.Deserialize(stream) as List<T>;
        stream.Close();
    }
    /* LOAD RÉSZ VÉGE */

    // Save 
    protected void Save(string key)
    {
        BinaryFormatter formatter = new();
        string path = Application.persistentDataPath + $"/{key}";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, datas);
        stream.Close();
    }

    // modosítás minden osztály maga valósítja meg.
    public abstract void SetData(T value, int index);
}
