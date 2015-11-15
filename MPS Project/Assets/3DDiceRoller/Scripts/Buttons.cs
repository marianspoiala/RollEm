using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Buttons : MonoBehaviour {

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnEnable()
    {
        Button startButton = GetComponent<Button>();
        if (startButton.name.Equals("StartBtn"))
        {
            startButton.onClick.AddListener(StartFunction);
        }
        else if (startButton.name.Equals("WordBtn"))
        {
            startButton.onClick.AddListener(WordFunction);
        }

    }

    void StartFunction()// your listener calls this function
    {
        InputField userName = GameObject.Find("UserNameInput").GetComponent<InputField>();
        print("Start game! " + userName.text);
    }

    void WordFunction()// your listener calls this function
    {
        InputField word = GameObject.Find("WordInput").GetComponent<InputField>();
        print("Word enter button pressed! " + word.text);
    }
}
