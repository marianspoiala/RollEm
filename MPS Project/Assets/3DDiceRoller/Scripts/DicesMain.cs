using UnityEngine;
using System.Collections;

public class DicesMain : MonoBehaviour {

    int stoppedCounter;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Metoda trimite mesaj catre fiecare zar sa inceapa miscarea
    public void RollDices()
    {
        stoppedCounter = 0;
        BroadcastMessage("StartRolling");
    }

    //Inregistreaza fiecare zar atunci cand se opreste
    public void RegisterStopped()
    {
        stoppedCounter++;
        //Daca s-au oprit toate cele 9 zaruri, se porneste timerul
        //Si se preiau literele de pe fetele superioare
        if (stoppedCounter == 9)
        {
            SendMessageUpwards("DicesStoppedMoving");
        }
    }
}
