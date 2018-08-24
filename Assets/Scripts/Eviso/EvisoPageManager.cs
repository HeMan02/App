using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EvisoPageManager : MonoBehaviour {

	// Deve contenere le connessioni al DB e tutte le funzioni possibili da richiamare con COROUTINE
	public static EvisoPageManager instance;
	public string[] items;
	public bool checkToEnter = false;
	public string itemsDataString;
	public string passClient = null;
	public string mailClient = null;
	public string passToConfirmClient = null;
	bool mailCheck = false;
	bool passcheck = false;
	public string CreateUserUrl = "http://togeathosting.altervista.org/Insert.php";
	public int numberButtonBills = 0;
	public GraphData graph1;
	public GraphData graph2;
	public List<GraphData> graphList;

	// utilizzo una lista di strutture dove associo ad ognuna i dati del grafico e andranno associati al tasto
	public struct GraphData{
		string data;
		public float f1;
		public float f2;
		public float f3;
	}
	
	// la lista dei grafi contiene la lista dei tasti e al sui interno per ogni tasto il contenuto di f1,f2,f3 per quella data ( dati ricevuti tutti da DB )


	void Awake()
	{
		int numGameobject = GameObject.FindGameObjectsWithTag("EvisoPageManager").Length;
		if (numGameobject > 1)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
		//		infoTextObj = GameObject.Find ("infoTextObj");
		Debug.Log("Awake");
	}
	// Use this for initialization
	void Start()
	{
		instance = this;
		graphList = new List<GraphData>();
		graph1 = new GraphData();
		graph2 = new GraphData();
		graph1.f1 = Random.Range(0f,200f);
		graph1.f2 = Random.Range(0f,200f);
		graph1.f3 = Random.Range(0f,200f);
		graph2.f1 = Random.Range(0f,200f);
		graph2.f2 = Random.Range(0f,200f);
		graph2.f3 = Random.Range(0f,200f);
		graphList.Add(graph1);
		graphList.Add(graph2);
	}

	// Update is called once per frame
	void Update()
	{
		// controllo sul tasto di ritorno back
		if (Input.GetKey (KeyCode.Escape)) { 
			var actualScene = SceneManager.GetActiveScene ();
			switch (actualScene.name) { // in base a ogni scenaq torno indietro a quella che mi serve
			case "EvisoMain":
				Debug.Log ("Sono nel Main non posso tornarte indietro");
				break;
			case "EvisoChoice":
				SceneManager.LoadScene("EvisoMain");
				break;
			case "EvisoBollettaMain":
				SceneManager.LoadScene("EvisoChoice");
				break;
			case "EvisoGraph":
				SceneManager.LoadScene("EvisoBollettaMain");
				break;
			case "EvisoAutolettura":
				SceneManager.LoadScene("EvisoChoice");
				break;
			}
		}
	}

	public void BackClick(string pageName)
	{
		SceneManager.LoadScene(pageName);
	}

	public void RegisterClick()
	{
		SceneManager.LoadScene("TogEatRegister");
	}

	public void EvisoChoiceClick()
	{
		SceneManager.LoadScene("EvisoChoice");
	}
	public void EvisoBollettaClick()
	{
		SceneManager.LoadScene("EvisoBollettaMain");
	}
	public void EvisoBollettaGraph()
	{
		SceneManager.LoadScene("EvisoBollettaGraph");
	}
	public void EvisoAutoletturaClick()
	{
		SceneManager.LoadScene("EvisoAutolettura");
	}
	public void EvisoNestoreClick()
	{
		SceneManager.LoadScene("EvisoNestore");
	}
		public void EvisoOpenGraphClick()
	{
		SceneManager.LoadScene("EvisoGraph");
	}
}
