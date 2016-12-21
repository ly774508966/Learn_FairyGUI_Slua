using UnityEngine;
using System.Collections;
using FairyGUI;

public class HeadBarMain : MonoBehaviour {

    GComponent _mainView;

	// Use this for initialization
	void Start () {
        Application.targetFrameRate = 60;

        _mainView = this.GetComponent<UIPanel>().ui;

        // 主UI界面
        GRichTextField richText = _mainView.GetChild("name").asRichTextField;
        richText.text = "[color=#ff4400]hello[/color]";

        Transform npc = GameObject.Find("npc1").transform;
        UIPanel panel = npc.FindChild("HeadBar").GetComponent<UIPanel>();
        panel.ui.GetChild("name").text = "Long [color=#FFFFFF]LongName[/color][img]ui://21gc3jcpoi6k3[/img] Name";
        panel.ui.GetChild("blood").asProgress.value = 80;
        panel.ui.GetChild("sign").asLoader.url = UIPackage.GetItemURL("HeaderBar", "task");

        npc = GameObject.Find("npc2").transform;
        panel = npc.FindChild("HeadBar").GetComponent<UIPanel>();
        panel.ui.GetChild("name").text = "Man2";
        panel.ui.GetChild("blood").asProgress.value = 20;
        panel.ui.GetChild("sign").asLoader.url = UIPackage.GetItemURL("HeaderBar", "fighting");

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
