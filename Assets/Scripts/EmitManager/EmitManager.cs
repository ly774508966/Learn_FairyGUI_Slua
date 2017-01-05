using UnityEngine;
using System.Collections;
using FairyGUI;
using System.Collections.Generic;

public class EmitManager {

    static EmitManager _instance;
    public static EmitManager inst
    {
        get
        {
            if (_instance == null)
            {
                _instance = new EmitManager();
            }
            return _instance;
        }
    }

    // 获取三种资源 字体、图片 的url
    public string hurtFontYellow;
    public string hurtFontRed;
    public string criticalSign;

    public GComponent view { get; private set; }

    private readonly Stack<EmitComponent> _componentPool = new Stack<EmitComponent>();

    public EmitManager()
    {
        hurtFontYellow = UIPackage.GetItemURL("EmitNumbers", "yellow_font");
        hurtFontRed = UIPackage.GetItemURL("EmitNumbers", "red_font");
        criticalSign = UIPackage.GetItemURL("EmitNumbers", "critical");

        view = new GComponent();
        GRoot.inst.AddChild(view);
    }

    public void Emit(Transform owner, int type, long hurt, bool critical)
    {
        EmitComponent ec;
        if (_componentPool.Count > 0)
        {
            ec = _componentPool.Pop();
        }
        else
        {
            ec = new EmitComponent();
        }
        ec.SetHurt(owner, type, hurt, critical);
    }

    public void ReturnComponent(EmitComponent com)
    {
        _componentPool.Push(com);
    }

}
