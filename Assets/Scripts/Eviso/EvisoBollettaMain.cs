using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// test per trello!!!
public class EvisoBollettaMain : MonoBehaviour {

	public GameObject objButton;
	public GameObject scrollContent;
	public List<EvisoNetworkObj.DataClient> myDataClient;
	EvisoPageManager.GraphData graphData;

	void Start () {
		// mi vado a settare i dati precedentemente ricevuti per poi utilizarli qua
		myDataClient = EvisoNetworkObj.instance.dataClientList;
		// istanzio tanti bottoni quante letture ho
		for (int i = 0; i < myDataClient.Count; i++){
			Debug.Log ("ButtonInstantiate");
			GameObject instanceObj = Instantiate(Resources.Load("Button", typeof(GameObject))) as GameObject;
			instanceObj.name = "B" + i;
			instanceObj.transform.parent = scrollContent.transform;
			instanceObj.transform.GetChild (0).GetComponent<UnityEngine.UI.Text>().text = EvisoNetworkObj.instance.dataClientList[i].pod; // data 
			// vado a crearmi il dato del grafo e lo vado a settare per usare successivamente
			graphData = new EvisoPageManager.GraphData();
			graphData.f1 = EvisoNetworkObj.instance.dataClientList [i].f1;
			graphData.f2 = EvisoNetworkObj.instance.dataClientList [i].f2;
			graphData.f3 = EvisoNetworkObj.instance.dataClientList [i].f3;
			EvisoPageManager.instance.graphList.Add(graphData);
		}
	}
		
	public void BackClick ()
	{
		EvisoPageManager.instance.BackClick("EvisoChoice");
	}
}
