using UnityEngine;
using System.Collections;
using FairyGUI;
using DG.Tweening;

public class ScrollPaneMain : MonoBehaviour {

    GComponent _mainView;
    GList _list;
    GameObject go;
    Object prefab;
    GGraph holder;

    void Awake()
    {
        Application.targetFrameRate = 60;
        Stage.inst.onKeyDown.Add(OnKeyDown);
    }

	// Use this for initialization
	void Start () {
        _mainView = this.GetComponent<UIPanel>().ui;

        _list = _mainView.GetChild("list").asList;
        _list.itemRenderer = RenderListItem;
        _list.SetVirtual();
        _list.numItems = 1000;
        _list.onTouchBegin.Add(OnClickList);
        _list.onClickItem.Add(OnClickListItem);

        _mainView.GetChild("box").asCom.onDrop.Add(OnDrop);

        LongPressGesture gesture = new LongPressGesture(_list);
        gesture.once = true;
        gesture.trigger = 1f;
        gesture.onAction.Add(OnLongPress);

        holder = _mainView.GetChild("holder").asGraph;
        prefab = Resources.Load("Particles/Fire/Flame");
        
        go = (GameObject)Object.Instantiate(prefab);
        holder.SetNativeObject(new GoWrapper(go));
	}

    void RenderListItem(int index, GObject obj)
    {
        GButton item = obj.asButton;
        item.title = "Item " + index;
        item.scrollPane.posX = 0; //reset scroll pos  list item, item是要可以滑动的

        //请注意，RenderListItem是重复调用的，不要使用Add增加帧听
        item.GetChild("b0").onClick.Set(OnClickStick);
        item.GetChild("b1").onClick.Set(OnClickDelete);
    }

    void OnClickList(EventContext context)
    {
        //find out if there is an item in edit status
        int cnt = _list.numChildren;
        for (int i = 0; i < cnt;  i++)
        {
            GButton item = _list.GetChildAt(i).asButton;
            if (item.scrollPane.posX != 0)
            {
                //check if clicked on the button
                if (item.GetChild("b0").asButton.IsAncestorOf(GRoot.inst.touchTarget) || item.GetChild("b1").asButton.IsAncestorOf(GRoot.inst.touchTarget)) {
                        return;
                }
                item.scrollPane.SetPosX(0, true);
                //取消滚动面板可能发生的拉动
                item.scrollPane.CancelDragging();
                _list.scrollPane.CancelDragging();
                break;
            }
        }
    }

    void OnClickListItem(EventContext context)
    {
        GButton item = (GButton)context.data;
        _mainView.GetChild("txt").text = "clicked " + item.title;

        holder.SetNativeObject(null);

    }

    void OnDrop(EventContext context)
    {
        _mainView.GetChild("txt").text = "Drop " + (string)context.data;

        go = (GameObject)Object.Instantiate(prefab);
        holder.SetNativeObject(new GoWrapper(go));
    }

    void OnClickStick(EventContext context)
    {
        _mainView.GetChild("txt").text = "Stick " + (((GObject)context.sender).parent).text;
    }

    void OnClickDelete(EventContext context)
    {
        _mainView.GetChild("txt").text = "Delete " + (((GObject)context.sender).parent).text;
    }

    void OnLongPress(EventContext context)
    {
        //逐层网上知道查到点击了那个item
        GObject obj = GRoot.inst.touchTarget;
        GObject p = obj.parent;
        while (p != null)
        {
            if (p == _list)
            {
                break;
            }
            p = p.parent;
        }

        if (p == null)
        {
            return;
        }

        DragDropManager.inst.StartDrag(obj, obj.icon, obj.text);
    }

    void OnKeyDown(EventContext context)
    {
        if (context.inputEvent.keyCode == KeyCode.Escape)
        {
            Application.Quit();
        }
    }
}
