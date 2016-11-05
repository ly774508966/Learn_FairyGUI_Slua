using UnityEngine;
using System.Collections;
using FairyGUI;
using System.Collections.Generic;

public class BagMain : MonoBehaviour {

    GComponent _mainView;
    BagWindow _bagWindow;

    void Awake()
    {
        //UIObjectFactory.SetLoaderExtension(typeof(MyGLoader));
    }

	void Start () {
        Application.targetFrameRate = 60;
        GRoot.inst.SetContentScaleFactor(1136, 640);
        _mainView = this.GetComponent<UIPanel>().ui;

        //---- long
        GComponent label_long_comp = _mainView.GetChild("label_long").asCom;
        //string label_long_ss = label_long_comp.text;
        //Debug.Log("label_long_ss : " + label_long_ss);

        //float long_length = getLength(label_long_comp);
        //Debug.Log("long_length :  " + long_length);

        //Transition long_trans = getTransition(label_long_comp);
        label_long_comp.visible = false;
        

        //---- short 
        GComponent label_short_comp = _mainView.GetChild("label_short").asCom;
        string label_short_ss = label_short_comp.text;
        Debug.Log("label_long_ss : " + label_short_ss);

        label_short_comp.text = "中华人民共和国";

        float short_length = getLength(label_short_comp);
        Debug.Log("short_length :  " + short_length);

        Transition short_trans = getTransition(label_short_comp);



        // TextField
        GComponent textFieldComp = _mainView.GetChild("textfield").asCom;
        GTextInput textfield = textFieldComp.GetChild("n1").asTextInput;
        if (textfield != null)
        {
            Debug.Log("text field ~= null");
            textfield.onChanged.Add(__changed);
            textfield.onKeyDown.Add(__onkeydown);
        }
        else
        {
            Debug.Log("nullllllllllllllll");
        }

        // image
        GImage image_origin = _mainView.GetChild("image_origin").asImage;
        NTexture texture = new NTexture(new Texture2D(100, 100) as Texture);
        image_origin.texture = texture;
        
	}

    void __changed(EventContext context)
    {
        Debug.Log("2222222222 changed");
    }

    void __onkeydown(EventContext context)
    {
        if (context.inputEvent.keyCode == KeyCode.Return)
        {
            Debug.Log("55555555555");
        }
    }

    private float getLength(GComponent textFieldComp)
    {
        GTextField text_long_field = textFieldComp.GetChild("title").asTextField;
        float long_length = text_long_field.width;
        return long_length;
    }

    private Transition getTransition(GComponent textFieldComp)
    {
        Transition long_trans = textFieldComp.GetTransition("label_move");
        float trans_length = getLength(textFieldComp);

        GGraph mask = textFieldComp.GetChild("mask").asGraph;


        long_trans.SetValue("last_pos", mask.width - trans_length, 0);
        Debug.Log("trans_length : " + trans_length);


        float timeScale = 434 / trans_length; // 434 是label的原始长度，6是动画的原始时间
        long_trans.timeScale = timeScale;

        Debug.Log("timeScale : " + timeScale);

        long_trans.Play(-1, 0, null);

        return long_trans;
    }

	// Update is called once per frame
	void Update () {
	    
	}
}
