using UnityEngine;
using System.Collections;
using FairyGUI;
using DG.Tweening;

public class OneCardMain : MonoBehaviour
{

    GComponent _mainView;
    OneCard _onecard;

    void Awake()
    {
        Application.targetFrameRate = 60;

        UIPackage.AddPackage("TurnCard");
        UIObjectFactory.SetPackageItemExtension(UIPackage.GetItemURL("TurnCard", "Card_Comp"), typeof(OneCard));
    }

    // Use this for initialization
    void Start()
    {
        _mainView = this.GetComponent<UIPanel>().ui;

        _onecard = (OneCard)_mainView.GetChild("n0");
        _onecard.onClick.Add(__clickCard);
    }

    void __clickCard(EventContext context)
    {
        OneCard card = (OneCard)context.sender;
        card.Turn();
    }
}
