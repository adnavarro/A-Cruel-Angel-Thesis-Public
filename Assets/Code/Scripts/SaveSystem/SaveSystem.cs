using System;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{

    public static void SaveData<T>(T obj, string fileName, string path)
    {
        
        var formatter = new BinaryFormatter();
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        var stream = new FileStream(path + fileName, FileMode.Create);

        formatter.Serialize(stream, obj);
        stream.Close();
    }

    public static object LoadData(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                var formatter = new BinaryFormatter();
                var stream = new FileStream(filePath, FileMode.Open);

                var loadedData = formatter.Deserialize(stream);
                stream.Close();
            
                return loadedData;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        return null;
    }
}
