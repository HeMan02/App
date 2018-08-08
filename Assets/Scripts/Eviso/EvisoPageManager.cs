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

	// ================================ CONNESSIONE AL DB ===================================================

//	public void CheckPassMailLogInConnection(){
//		StartCoroutine ("CheckPassMailLogIn");
//	}
//
//	public void CheckPassMailRegisterConnection(){
//		StartCoroutine ("CheckPassMailRegister");
//	}

//	IEnumerator CheckPassMailLogIn ()
//	{
//		WWW itemsData = new WWW ("http://togeathosting.altervista.org/Query.php");
//		yield return itemsData;
//		string itemsDataString = itemsData.text;
//		items = itemsDataString.Split (';');
//		// prendo i dati in modo corretto ma pensare come fare check, una è una coroutine e non è sincronizzata
//		mailCheck = false;
//		passcheck = false;
//		// scandisco tutti i nomi delle mail e delle pass e controllo se almeno una fa check
//		for (int i = 0; i < items.Length -1; i++ ){
//			string[] mailAndPass = items[i].Split('|');
//			string[] mailTotal = mailAndPass[0].Split (':');
//			string mail = null;
//			if (mailTotal[1] != null) {
//				mail = mailTotal [1];
//								Debug.Log("MailClient " +  mailClient + mailClient.Length + " == " + mail + mail.Length);
//				if (string.Compare (mailClient, mail) == 0) {
//					mailCheck = true;
//				}
//			}
//			string[] passTotal = mailAndPass[1].Split (':');
//			string pass = null;
//			if (passTotal [1] != null) {
//				pass = passTotal [1];
//								Debug.Log("PassClient " +  passClient + " == " + pass);
//				if (string.Compare (passClient, pass) == 0) {
//					passcheck = true;
//				}
//			}
//		}
//		//		Debug.Log("mailCheck " +  mailCheck + " passcheck " + passcheck);
//		if (mailCheck && passcheck) {
//			// mi vado a prendere il riferimentop o vedo come dagli l'input per settare a ok e andare avanti se no no
//			Debug.Log ("PASS GIUSTA ");
//			EvisoMainPage.instance.OpenLoginPage ();
//		} else {
//			Debug.Log ("PASS SBAGLIATA ");
//			EvisoMainPage.instance.PrintInfoText ("PASS SBAGLIATA");
//		}
//	}

//	IEnumerator CheckPassMailRegister ()
//	{
//		WWW itemsData = new WWW ("http://togeathosting.altervista.org/Query.php");
//		yield return itemsData;
//		string itemsDataString = itemsData.text;
//		items = itemsDataString.Split (';');
//		// prendo i dati in modo corretto ma pensare come fare check, una è una coroutine e non è sincronizzata
//		mailCheck = false;
//		passcheck = false;
//		// scandisco tutti i nomi delle mail e delle pass e controllo se almeno una fa check
//		for (int i = 0; i < items.Length -1; i++ ){
//			string[] mailAndPass = items[i].Split('|');
//			string[] mailTotal = mailAndPass[0].Split (':');
//			string mail = null;
//			if (mailTotal[1] != null) {
//				mail = mailTotal [1];
//				//				Debug.Log("MailClient " +  mailClient + mailClient.Length + " == " + mail + mail.Length);
//				if (string.Compare (mailClient, mail) == 0) {
//					mailCheck = true;
//				}
//			}
//			string[] passTotal = mailAndPass[1].Split (':');
//			string pass = null;
//			if (passTotal [1] != null) {
//				pass = passTotal [1];
//				//				Debug.Log("PassClient " +  passClient + " == " + pass);
//				if (string.Compare (passClient, pass) == 0) {
//					passcheck = true;
//				}
//			}
//		}
//		//		Debug.Log("mailCheck " +  mailCheck + " passcheck " + passcheck);
//		if (mailCheck && passcheck) {
//			// mi vado a prendere il riferimentop o vedo come dagli l'input per settare a ok e andare avanti se no no
//			Debug.Log ("PASS già presente, non ti puoi registrare ");
//			Register.instance.PrintInfoText ("PASS già presente, non ti puoi registrare ");
//		} else {
//			Debug.Log ("PASS non presente ti registro ");
//			Register.instance.PrintInfoText ("PASS non presente ti registro ");
//			CreateUser (mailClient, passClient); // AGGIUNTO 
//			// CHIAMARE l'insert nel DB
//		}
//	}

	//	IEnumerator InsertData ()
	//	{
	////		WWW itemsData = new WWW ("http://togeathosting.altervista.org/Insert.php");
	////		yield return itemsData;
	//	}

	//  INSERT
//	public void CreateUser(string mail, string pass){
//		WWWForm form = new WWWForm();
//		form.AddField("mailclientPost",mail);
//		form.AddField("passClientPost",pass);
//		WWW www = new WWW (CreateUserUrl, form);
//	}

//	IEnumerator CiccioConnect ()
//	{
//		WWW itemsData = new WWW ("http://togeathosting.altervista.org/Ciccio.php");
//		yield return itemsData;
//	}

//	string GetDataValue (string data, string index)
//	{
//		string value = data.Substring (data.IndexOf (index) + index.Length);
//		//        value = value.Remove(value.IndexOf("|"));
//		return value;
//	}

}
