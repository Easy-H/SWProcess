using System.Collections.Generic;
using UnityEngine;
using System.Xml;
public class StageMetaData {
    public string name;
    public string value;
}

public struct UnitInfor {
    public string type;
    public Vector3 pos;
}

public class LevelData {

    public UnitInfor[] units;

    public LevelData()
    {
        string stage = StageManager.Instance.GetStageMetaData().value;

        _DataSet(stage);
    }

    public LevelData(string value)
    {
        _DataSetTemp(value);
    }
    void _DataSetTemp(string value)
    {
        XmlDocument xmlDoc = new XmlDocument();

        xmlDoc.Load(value + ".xml");

        XmlNodeList nodes = xmlDoc.SelectNodes("StageData/Unit");

        units = new UnitInfor[nodes.Count];

        for (int i = 0; i < nodes.Count; i++)
        {
            string unitValue = nodes[i].Attributes["value"].Value;
            int x = int.Parse(nodes[i].Attributes["xPos"].Value);
            int y = int.Parse(nodes[i].Attributes["yPos"].Value);

            units[i].type = unitValue;
            units[i].pos = new Vector3(x, y);
        }

    }

    void _DataSet(string value)
    {
        XmlDocument xmlDoc = AssetOpener.ReadXML("StageData/" + value);

        XmlNodeList nodes = xmlDoc.SelectNodes("StageData/Unit");

        units = new UnitInfor[nodes.Count];

        for (int i = 0; i < nodes.Count; i++)
        {
            string unitValue = nodes[i].Attributes["value"].Value;
            int x = int.Parse(nodes[i].Attributes["xPos"].Value);
            int y = int.Parse(nodes[i].Attributes["yPos"].Value);

            units[i].type = unitValue;
            units[i].pos = new Vector3(x, y);
        }

    }
}

public class StageManager : MonoSingleton<StageManager> {
    // Start is called before the first frame update

    public static int WorldCount { get; private set; }
    public static int WorldIdx = 0;
    public int StageIdx { get; set; }

    class OverWorldData {
        internal string name;
        internal int size = 0;
        internal List<StageMetaData> _stages;

        public void Read(XmlNode node)
        {
            name = node.Attributes["name"].Value;
            _stages = new List<StageMetaData>();

            XmlNodeList nodes = node.SelectNodes("Stage");
            for (int i = 0; i < nodes.Count; i++)
            {
                StageMetaData stageData = new StageMetaData();
                stageData.name = nodes[i].Attributes["name"].Value;
                stageData.value = nodes[i].Attributes["value"].Value;

                _stages.Add(stageData);

            }
            size = nodes.Count;
        }

        public StageMetaData ReturnStageData(int i) {
            return _stages[i];
        }
    }

    List<OverWorldData> _dic;

    protected override void OnCreate()
    {
        _dic = new List<OverWorldData>();
        XmlDocument xmlDoc = AssetOpener.ReadXML("StageListData");

        XmlNodeList nodes = xmlDoc.SelectNodes("StageListData/OverWorld");

        for (int i = 0; i < nodes.Count; i++)
        {
            OverWorldData overworldData = new OverWorldData();
            overworldData.Read(nodes[i]);

            _dic.Add(overworldData);
        }

        WorldCount = nodes.Count;
    }

    public int GetStageCount() {
        return _dic[WorldIdx].size;
    }

    public string GetWorldName()
    {
        return _dic[WorldIdx].name;
    }

    public StageMetaData GetStageMetaData(int idx)
    {
        return _dic[WorldIdx].ReturnStageData(idx);
    }
    public StageMetaData GetStageMetaData()
    {
        return GetStageMetaData(StageIdx);
    }

    public LevelData GetStageData() {
        LevelData data = new LevelData();

        return data;

    }
    public LevelData GetStageData(string value)
    {
        LevelData data = new LevelData(value);

        return data;

    }

}
