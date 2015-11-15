using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//This script is used for every button and input on the scene
public class Buttons : MonoBehaviour {
    
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    //When object is enabled (at start or setActive(true))
    void OnEnable()
    {
        //Adding listeners to button
        Button button = GetComponent<Button>();
        if (button.name.Equals("StartBtn"))
        {
            button.onClick.AddListener(StartFunction);
        }
        else if (button.name.Equals("WordBtn"))
        {
            button.onClick.AddListener(WordFunction);
        }

    }

    //Start button listener function
    void StartFunction()
    {
        print("Game started!");        

        //Send message to MainCanvas to start game
        SendMessageUpwards("StartGame");
    }

    //Word button listener function
    void WordFunction()
    {
        print("Word enter button pressed!");

        //Sending word to MainCanvas to be checked if correct and added on GUI
        SendMessageUpwards("CheckWord");        
    }
}
