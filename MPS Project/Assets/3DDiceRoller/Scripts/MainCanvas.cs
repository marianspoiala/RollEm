using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainCanvas : MonoBehaviour {

    GameObject startBtn;
    GameObject wordBtn;
    GameObject wordInput;
    GameObject userNameInput;
    GameObject wordsListView;

    // Use this for initialization
    void Start () {
        //Initialize buttons and inputs
        startBtn = GameObject.Find("StartBtn");
        wordBtn = GameObject.Find("WordBtn");
        wordInput = GameObject.Find("WordInput");
        userNameInput = GameObject.Find("UserNameInput");
        wordsListView = GameObject.Find("WordsListView");

        //Deactivate word input and button when game launches
        wordBtn.SetActive(false);
        wordBtn.GetComponent<Button>().onClick.RemoveAllListeners();
        wordInput.SetActive(false);
        wordsListView.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {	
	}
}
