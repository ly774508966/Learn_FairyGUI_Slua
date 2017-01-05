using UnityEngine;
using System.Collections;
using FairyGUI;

public class HeadBarMain : MonoBehaviour {

    GComponent _mainView;

    string ss = "ui://21gc3jcpoi6k4";

	// Use this for initialization
	void Start () {
        Application.targetFrameRate = 60;

        _mainView = this.GetComponent<UIPanel>().ui;

        // 主UI界面
        GRichTextField richText = _mainView.GetChild("name").asRichTextField;
        richText.text = "[color=#ff4400]hello[/color]";

        Transform npc = GameObject.Find("npc1").transform;
        UIPanel panel = npc.FindChild("HeadBar").GetComponent<UIPanel>();
        GRichTextField richTextNpc1 = panel.ui.GetChild("name").asRichTextField;
        richTextNpc1.text = "Long [color=#FFFFFF]LongName[/color]<a href='http://www.baidu.com><img src='ui://21gc3jcpoi6k4'/></a> Name";  //<a href=’xx><img src=’yy’/></a>
        richTextNpc1.onClickLink.Add(() => {
            Debug.Log("click rich text img ");
        });
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
