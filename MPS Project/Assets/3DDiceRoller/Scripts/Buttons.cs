using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//This script is used for every button and input on the scene
public class Buttons : MonoBehaviour {

    GameObject startBtn;
    GameObject wordBtn;
    GameObject wordInput;
    GameObject userNameInput;
    GameObject wordsListView;
    GameObject wordsListText;

    // Use this for initialization
    void Start () {

        //Initialize buttons and inputs
        startBtn = GameObject.Find("StartBtn");
        wordBtn = GameObject.Find("WordBtn");
        wordInput = GameObject.Find("WordInput");
        userNameInput = GameObject.Find("UserNameInput");
        wordsListView = GameObject.Find("WordsListView");
        wordsListText = GameObject.Find("WordsListText");
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

    void StartFunction()// your listener calls this function
    {
        InputField userName = userNameInput.GetComponent<InputField>();
        print("Start game! " + userName.text);

        //Activate word input and button
        wordBtn.SetActive(true);
        wordInput.SetActive(true);
        wordsListView.SetActive(true);
        //Resetting words from words list view
        wordsListText.GetComponent<Text>().text = "";

        //Disable start button and user name input after clicking Start Game
        startBtn.SetActive(false);
        startBtn.GetComponent<Button>().onClick.RemoveAllListeners();
        userNameInput.SetActive(false);
    }

    void WordFunction()// your listener calls this function
    {
        InputField word = wordInput.GetComponent<InputField>();

        //Adding word to words list view on GUI
        if (wordsListText.GetComponent<Text>().text == "")
        {
            wordsListText.GetComponent<Text>().text = word.text;
        }
        else
        {
            wordsListText.GetComponent<Text>().text += "\n" + word.text;
        }

        //Sending word to MainCanvas to be saved in file

        print("Word enter button pressed! " + word.text);
    }
}
