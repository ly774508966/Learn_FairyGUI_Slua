using UnityEngine;
using System.Collections;
using FairyGUI;


public class TestPartical : MonoBehaviour {

    GComponent _mainView;

    GGraph slot_holder;


	// Use this for initialization
	void Start () {
        Application.targetFrameRate = 60;
        GRoot.inst.SetContentScaleFactor(1920, 1080);
        _mainView = this.GetComponent<UIPanel>().ui;

        slot_holder = _mainView.GetChild("holder").asGraph;

        Object prefab = Resources.Load("FX_UI_Glow");
        GameObject go = (GameObject)Object.Instantiate(prefab);
        slot_holder.SetNativeObject(new GoWrapper(go));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
