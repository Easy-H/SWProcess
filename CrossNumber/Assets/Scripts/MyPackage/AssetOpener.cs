using System.Xml;
using UnityEditor;
using UnityEngine;

public class AssetOpener : MonoBehaviour
{
    public static T Import<T>(string path) where T: Object
    {
        T source = Resources.Load(path) as T;
        return Instantiate(source);
    }
    public static T ImportGameObject<T>(string path) where T : Object
    {
        GameObject source = Resources.Load(path) as GameObject;
        return Instantiate(source).GetComponent<T>();
    }

    public static XmlDocument ReadXML(string path)
    {
        TextAsset xmlData = new TextAsset();
        xmlData = (TextAsset)Resources.Load("XML/" + path, typeof(TextAsset));

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlData.text);

        return xmlDoc;
    }
}
