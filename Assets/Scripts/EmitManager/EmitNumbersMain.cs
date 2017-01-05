using UnityEngine;
using System.Collections;
using FairyGUI;

public class EmitNumbersMain : MonoBehaviour {

    Transform _npc1;
    Transform _npc2;
    bool _finished;

	// Use this for initialization
	void Start () {
        _npc1 = GameObject.Find("npc1").transform;
        _npc2 = GameObject.Find("npc2").transform;

        StartCoroutine(RunTest());
	}

    void OnDisable()
    {
        _finished = true;
    }

    IEnumerator RunTest()
    {
        while (!_finished)
        {
            EmitManager.inst.Emit(_npc1, 0, UnityEngine.Random.Range(100, 100000), UnityEngine.Random.Range(0, 10) == 5);
            EmitManager.inst.Emit(_npc2, 1, UnityEngine.Random.Range(100, 100000), UnityEngine.Random.Range(0, 10) == 5);
            yield return new WaitForSeconds(0.3f);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
