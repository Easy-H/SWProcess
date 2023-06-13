using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class UIManager : Singleton<UIManager> {

    List<GUICustomFullScreen> uiStack;

    public GUICustomFullScreen NowPopUp { get; set; }
    
    public void EnrollmentGUI(GUICustomFullScreen newData)
    {
        SoundManager.Instance.PlayAudio("Change");
        if (NowPopUp == null)
        {
            NowPopUp = newData;
            return;

        }
        else
        {
            NowPopUp.gameObject.SetActive(false);
            uiStack.Add(NowPopUp);
            uiStack.Add(newData);

        }
        Pop();
    }

    public void Pop()
    {
        if (uiStack.Count < 1)
            return;

        NowPopUp = uiStack[uiStack.Count - 1];
        uiStack.RemoveAt(uiStack.Count - 1);
        NowPopUp.Pop();

        SoundManager.Instance.PlayAudio("Change");
    }

    class GUIData {
        internal string name;
        internal string path;

        internal void Read(XmlNode node)
        {
            name = node.Attributes["name"].Value;
            path = node.Attributes["path"].Value;
        }
    }

    Dictionary<string, GUIData> _dic;

    protected override void OnCreate()
    {
        NowPopUp = null;
        uiStack = new List<GUICustomFullScreen>();

        _dic = new Dictionary<string, GUIData>();
        XmlDocument xmlDoc = AssetOpener.ReadXML("GUIInfor");

        XmlNodeList nodes = xmlDoc.SelectNodes("GUIInfor/GUIData");

        for (int i = 0; i < nodes.Count; i++)
        {
            GUIData guiData = new GUIData();
            guiData.Read(nodes[i]);

            _dic.Add(guiData.name, guiData);
        }

    }
    public static T OpenGUI<T>(string guiName)
    {
        string path = Instance._dic[guiName].path;
        T result = AssetOpener.Import<GameObject>(path).GetComponent<T>();

        return result;
    }
}
