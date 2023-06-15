using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.XR;
public class GUIBuildScene : GUICustomFullScreen {

    [SerializeField] LevelMaker _setter;
    [SerializeField] GUIAnimatedOpen _editToolContainer;
    [SerializeField] Transform _container;

    enum BuildState {
        Idle, Create, Erase
    }

    BuildState _buildState = BuildState.Idle;

    string _createValue = string.Empty;

    protected override void Open()
    {
        base.Open();
        _setter.MakeLevel("Temp");
    }

    protected override void Update()
    {
        switch(_buildState)
        {
            case BuildState.Idle:
                base.Update();
                return;
            case BuildState.Create:
                _CreateRoutine();
                break;
            case BuildState.Erase:
                _EraseRoutine();
                break;
        }
    }

    void _CreateRoutine() {

        if (!Input.GetMouseButtonDown(0)) return;

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        pos.x = Mathf.Round(pos.x);
        pos.y = Mathf.Round(pos.y);

        pos.z = 0;

        Unit unit = UnitManager.Instance.CreateUnit(_createValue, pos);
        UnitController cntl = UnitManager.Instance.CreateUnitController(unit);

        cntl.transform.SetParent(_container);

        _editToolContainer.Open();
        _buildState = BuildState.Idle;
    }

    void _EraseRoutine() {

        if (!Input.GetMouseButtonDown(0)) return;

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        UnitController selectedUnit = UnitManager.GetUnitControllerAt(pos);

        if (selectedUnit)
            UnitManager.Instance.DestroyUnit(selectedUnit);

        _state = MotionState.Idle;
        _buildState = BuildState.Idle;

    }

    public void CreateSet(string value) {

        _buildState = BuildState.Create;
        _createValue = value;

    }

    public void EraseSet() {
        _buildState = BuildState.Erase;
    }

    public void GenerateWorld(string name)
    {

        XmlDocument Document = new XmlDocument();
        XmlElement FList = Document.CreateElement("StageData");
        Document.AppendChild(FList);

        List<Unit> units = UnitManager.Instance.GetAllUnit();

        for (int i = 0; i < units.Count; i++)
        {
            XmlElement FElement = Document.CreateElement("Unit");
            FElement.SetAttribute("value", units[i].Value);
            FElement.SetAttribute("xPos", units[i].Pos.x.ToString());
            FElement.SetAttribute("yPos", units[i].Pos.y.ToString());

            FList.AppendChild(FElement);
        }
        Document.Save(name + ".xml");

        Debug.Log(name + " Saved");

    }

}
