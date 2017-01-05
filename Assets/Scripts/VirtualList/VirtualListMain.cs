using UnityEngine;
using System.Collections;
using FairyGUI;

public class VirtualListMain : MonoBehaviour {


    GComponent _mainView;
    GList _list;

    GButton _select_nums_btn;
    GButton _scroll_top_btn;
    GButton _scroll_bottom_btn;

    void Awake()
    {
        UIPackage.AddPackage("VirtualList");
        UIObjectFactory.SetPackageItemExtension(UIPackage.GetItemURL("VirtualList", "mailItem"), typeof(MailItem));
    }
	// Use this for initialization
	void Start () {

        _mainView = this.GetComponent<UIPanel>().ui;

        _list = _mainView.GetChild("mailList").asList;

        _select_nums_btn = _mainView.GetChild("select_nums_btn").asButton;
        _select_nums_btn.data = "data_test";
        _select_nums_btn.onClick.Add(select_nums_click);

        _scroll_top_btn = _mainView.GetChild("top_btn").asButton;
        _scroll_top_btn.onClick.Add(() => { _list.scrollPane.ScrollTop(); });

        _scroll_bottom_btn = _mainView.GetChild("bottom_btn").asButton;
        _scroll_bottom_btn.onClick.Add(() => { _list.scrollPane.ScrollBottom();  });

        _list.itemRenderer = RenderListItem;
        _list.numItems = 10;
	}

    void RenderListItem(int index, GObject obj) {
        MailItem item = (MailItem)obj;
        item.setFetched(index % 3 == 0);
        item.setRead(index % 2 == 0);
        item.setTime("5 Nov 2015 16:24:33");
        item.title = index + "  Mail title here";

        Debug.Log(index + "   numItems ---> " + _list.numItems);
    }

    void select_nums_click(EventContext context)
    {
        _list.AddSelection(3, true);

        MailItem item = (MailItem)_list.GetChildAt(9);
        Debug.Log(item.title);

    }

	// Update is called once per frame
	void Update () {
	
	}
}
