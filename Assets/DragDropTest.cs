using UnityEngine;
using System.Collections;
using FairyGUI;

public class DragDropTest : MonoBehaviour {

    GComponent _mainView;

	// Use this for initialization
	void Start () {
        Application.targetFrameRate = 60;

        _mainView = this.gameObject.GetComponent<UIPanel>().ui;

        GButton photo_01 = _mainView.GetChild("photo_01").asButton;
        GButton photo_02 = _mainView.GetChild("photo_02").asButton;
        GButton flower_01 = _mainView.GetChild("flower_01").asButton;
        GButton flower_02 = _mainView.GetChild("flower_02").asButton;

        photo_01.draggable = true;
        photo_01.onDragStart.Add((EventContext context) => {
            context.PreventDefault();
            MyDragDropManager.inst.StartDrag(photo_01, photo_01.icon, flower_01.icon, photo_01.icon, (int)context.data);
            GLoader loader_b = MyDragDropManager.inst.dragAgent_a;
            loader_b.fill = FillType.ScaleFree;
            loader_b.width = 100;
            loader_b.height = 30;

        });

        photo_02.icon = null;
        photo_02.onDrop.Add((EventContext context) => {
            photo_02.icon = (string)context.data;
        });

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
