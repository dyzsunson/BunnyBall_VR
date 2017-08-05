using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotChanged : MonoBehaviour {
    public static NotChanged context;
    public int Level_Current = 0;
    public bool is_single_player = false;
    public bool is_auto_start = false;

    private void Awake() {
        if (context == null) {
            context = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
