using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;

public class Serializer : MonoBehaviour
{
    /// <summary>
    /// This script serializes objects put into it or deserializes documents sent to it and returns them
    /// </summary>
    /// <param name="toSerialize"></param>
    /// <param name="path"></param>
    //Saves objects sent to it
    public static void Serialize(object toSerialize, string path)
    {
        XmlSerializer serializer = new XmlSerializer(toSerialize.GetType());
        StreamWriter writer = new StreamWriter(path);
        serializer.Serialize(writer.BaseStream, toSerialize);
        writer.Close();
    }

    //Loads from paths sent to it
    public static T Deserialize<T>(string path)
    {
       XmlSerializer serializer = new XmlSerializer(typeof(T));
        StreamReader reader = new StreamReader(path);
        T deserialized = (T)serializer.Deserialize(reader.BaseStream);
        reader.Close();
        return deserialized;
    }
}
