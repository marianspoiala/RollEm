using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MainCanvas : MonoBehaviour {

    public enum GameState { Started, Stopped };
    GameObject startBtn;
    GameObject wordBtn;
    GameObject wordInput;
    GameObject userNameInput;
    GameObject wordsListView;
    GameObject wordsListText;
    GameObject nameText;
    GameObject scoreText;
    float timeLeft = 0;
    GameState gameState;
    List<string> wordsList;
    string userName;

    // Use this for initialization
    void Start () {
        //Initialize game objects
        startBtn = GameObject.Find("StartBtn");
        wordBtn = GameObject.Find("WordBtn");
        wordInput = GameObject.Find("WordInput");
        userNameInput = GameObject.Find("UserNameInput");
        wordsListView = GameObject.Find("WordsListView");
        wordsListText = GameObject.Find("WordsListText");
        nameText = GameObject.Find("NameText");
        scoreText = GameObject.Find("ScoreText");

        ResetGame();
    }
	
    void ResetGame()
    {
        //Deactivate word input and button when game launches
        startBtn.SetActive(true);
        userNameInput.SetActive(true);
        wordBtn.SetActive(false);
        wordBtn.GetComponent<Button>().onClick.RemoveAllListeners();
        wordInput.SetActive(false);
        wordsListView.SetActive(false);

        //Resetting words from words list view
        wordsListText.GetComponent<Text>().text = "";

        //Initialise components
        timeLeft = 10.0f;
        gameState = GameState.Stopped;
        wordsList = new List<string>();
        userName = "";
        userNameInput.GetComponent<InputField>().text = "";
        nameText.GetComponent<Text>().text = "";
        scoreText.GetComponent<Text>().text = "";
    }

	// Update is called once per frame
	void Update () {
        if (timeLeft > 0 && gameState == GameState.Started)
        {
            timeLeft -= Time.deltaTime;
        }
        else if (timeLeft <= 0 && gameState == GameState.Started)
        {
            print("Time is up!");
            ResetGame();
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 150, 100), timeLeft.ToString());
    }

    public void StartGame()
    {
        gameState = GameState.Started;

        //Set username and score
        userName = userNameInput.GetComponent<InputField>().text;
        nameText.GetComponent<Text>().text = "Name : " + userName;
        scoreText.GetComponent<Text>().text = "Score : 0";

        //Activate word input and button
        wordBtn.SetActive(true);
        wordInput.SetActive(true);
        wordsListView.SetActive(true);        

        //Disable start button and user name input after clicking Start Game
        startBtn.SetActive(false);
        startBtn.GetComponent<Button>().onClick.RemoveAllListeners();
        userNameInput.SetActive(false);
    }

    public void CheckWord()
    {
        string word = wordInput.GetComponent<InputField>().text;

        //aici se face check-ul in TRIE

        //Daca exisa cuvantul 
        StoreWord(word);

        //Adding word to words list view on GUI
        if (wordsListText.GetComponent<Text>().text == "")
        {
            wordsListText.GetComponent<Text>().text = word;
        }
        else
        {
            wordsListText.GetComponent<Text>().text += "\n" + word;
        }

    }

    public void StoreWord(string word)
    {
        wordsList.Add(word);
    }
}
