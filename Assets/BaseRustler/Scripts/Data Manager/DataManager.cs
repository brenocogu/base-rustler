using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class DataManager {
    static BinaryFormatter formatter = new BinaryFormatter();

    public static void SaveData(string name, object objtoSave) {
        if (File.Exists(Application.persistentDataPath + '/' + name))
        {
            FileStream fileStream = File.Open(Application.persistentDataPath + '/' + name, FileMode.Open);
            SerializedTypes dataHolder = new SerializedTypes();
            dataHolder.value = objtoSave;

            formatter.Serialize(fileStream, dataHolder);
            fileStream.Close();
        }
        else {
            FileStream fileStream = File.Create(Application.persistentDataPath + '/' + name);
            SerializedTypes dataHolder = new SerializedTypes();
            dataHolder.value = objtoSave;

            formatter.Serialize(fileStream, dataHolder);
            fileStream.Close();
        }
    }

    public static object LoadData(string objToload) {
        if (File.Exists(Application.persistentDataPath + '/' + objToload))
        {
            FileStream fileStream = File.Open(Application.persistentDataPath + '/' + objToload, FileMode.Open, FileAccess.Read, FileShare.Read);
            SerializedTypes dataHolder = (SerializedTypes)formatter.Deserialize(fileStream);
            return dataHolder.value;
        }
        else {
            return null;
        }
    }

    public static void ClearData(string name) {
        if (File.Exists(Application.persistentDataPath + '/' + name))
        {
            File.Delete(Application.persistentDataPath + '/' + name);
        }
    }
}

[Serializable]
class SerializedTypes {
    public object value;
}
