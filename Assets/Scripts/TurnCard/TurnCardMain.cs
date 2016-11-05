using UnityEngine;
using System.Collections;
using FairyGUI;
using DG.Tweening;

public class TurnCardMain : MonoBehaviour {

    GComponent _mainView;
    Card _c0;
    Card _c1;

    void Awake()
    {
        Application.targetFrameRate = 60;
        Stage.inst.onKeyDown.Add(OnKeyDown);

        UIPackage.AddPackage("TurnCard");
        UIObjectFactory.SetPackageItemExtension(UIPackage.GetItemURL("TurnCard", "CardComponent"), typeof(Card));
    }

	// Use this for initialization
	void Start () {
        _mainView = this.GetComponent<UIPanel>().ui;

        _c0 = (Card)_mainView.GetChild("n0");

        _c1 = (Card)_mainView.GetChild("n1");
        _c1.SetPrespective();

        _c0.onClick.Add(__clickCard);
        _c1.onClick.Add(__clickCard);
	}

    void __clickCard(EventContext context)
    {
        Card card = (Card)context.sender;
        card.Turn();
    }

    void OnKeyDown(EventContext context)
    {
        if (context.inputEvent.keyCode == KeyCode.Escape)
        {
            Application.Quit();
        }
    }
}
