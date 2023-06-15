using UnityEngine;

[System.Serializable]
public class LevelMaker : MonoBehaviour
{
    public Transform _parent = null;
    public bool _testScene = false;

    private void _Clear() {
        for (int i = 0; i < _parent.childCount; i++) {
            _parent.GetChild(i).gameObject.SetActive(false);
            Destroy(_parent.GetChild(i).gameObject);
        }
        UnitManager.Instance.DestroyAllUnit();
    }

    // Start is called before the first frame update
    public void MakeLevel() {
        LevelData stage;
        if (!_testScene)
            stage = StageManager.Instance.GetStageData();
        else
            stage = StageManager.Instance.GetStageData("temp");

        _Clear();
        _CreateWorld(stage);
    }

    public void MakeLevel(string value)
    {
        LevelData stage = StageManager.Instance.GetStageData(value);

        _CreateWorld(stage);
    }

    void _CreateWorld(LevelData stage)
    {

        for (int i = 0; i < stage.units.Length; i++)
        {
            UnitInfor data = stage.units[i];

            Unit unit = UnitManager.Instance.CreateUnit(data.type, data.pos);
            UnitController cntl = UnitManager.Instance.CreateUnitController(unit);

            cntl.transform.SetParent(_parent);
        }

    }
}
