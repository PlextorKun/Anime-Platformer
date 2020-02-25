using UnityEngine;
using System.Collections;

public class DestroyTimed : MonoBehaviour {

    public float aliveTime;

	// Use this for initialization
	void Awake () {
        Destroy(gameObject, aliveTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
