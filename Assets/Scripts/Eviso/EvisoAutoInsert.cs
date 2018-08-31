using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EvisoAutoInsert : MonoBehaviour {
	public Text date;
	public InputField f1Input;
	public InputField f2Input;
	public InputField f3Input;

	// Use this for initialization
	void Start () {
		date.text = "" + System.DateTime.Now.ToString ("dd/MM/yyyy");
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("F1: " + f1Input.text + " F2: " + f2Input.text + " F3: " + f3Input.text);
	}
}
