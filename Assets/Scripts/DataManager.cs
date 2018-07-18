using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class DataManager
{
    public static List<ARObjectData> saveData;

    public static void Save(List<ARObjectData> lstARObject)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/data.dat");

        bf.Serialize(file, lstARObject);
        file.Close();

        saveData = lstARObject.ToList();
    }

    public static List<ARObjectData> Load()
    {
        List<ARObjectData> lstARObject = new List<ARObjectData>();

        if (File.Exists((Application.persistentDataPath + "/data.dat")))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/data.dat", FileMode.Open);

            lstARObject = (List<ARObjectData>)bf.Deserialize(file);
            file.Close();
            saveData = lstARObject.ToList();
        }

        return lstARObject;
    }
}
