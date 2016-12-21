using UnityEngine;
using System.Collections;
using FairyGUI;

public class Extension : MonoBehaviour {

    GComponent _mainView;
    GList _list;

	// Use this for initialization
	void Start () {
        Application.targetFrameRate = 60;
        _mainView = this.GetComponent<UIPanel>().ui;

        _list = _mainView.GetChild("n0").asList;
        for (int i = 0; i < 10; i++)
        {
            GButton item = (GButton)_list.AddItemFromPool();
            item.title = "hi " + i;
            item.visible = false;
        }

        _list.EnsureBoundsCorrect();

        for (int i = 0; i < 10; i++)
        {
            GButton item = (GButton)_list.GetChildAt(i);
            if (_list.IsChildInView(item))
            {
                Transition trans = item.GetTransition("t0");
                trans.Play(1, 2 * i, null);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
