using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MainCanvas : MonoBehaviour {

    public enum GameState { Started, Stopped, DicesRolling };

    GameObject startBtn;
    GameObject wordBtn;
    GameObject wordInput;
    GameObject userNameInput;
    GameObject wordsListView;
    GameObject wordsListText;
    GameObject nameText;
    GameObject scoreText;
    GameObject topUsersListText;

    const float GAME_TIME = 10.0f;
    const string FILE_PATH = "users.json";

    float timeLeft = 0;
    GameState gameState;
    List<string> wordsList;
    string currentLetters; 

    GameUtilsIF gameUtils;
    UserDTO currentUser;
    List<UserDTO> topUsersList;

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
        topUsersListText = GameObject.Find("TopUsersListText");

        //Make gameUtils services available
        gameUtils = new GameUtils();
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

        //Initialise
        timeLeft = GAME_TIME;
        gameState = GameState.Stopped;
        wordsList = new List<string>();
        currentUser = new UserDTO();

        //Initialise unity components
        wordInput.GetComponent<InputField>().text = "";
        wordsListText.GetComponent<Text>().text = "";
        userNameInput.GetComponent<InputField>().text = "";
        nameText.GetComponent<Text>().text = "";
        scoreText.GetComponent<Text>().text = "";
        topUsersListText.GetComponent<Text>().text = "";

        //Get top users list
        topUsersList = gameUtils.loadUsersDetails(FILE_PATH);
        topUsersList.Sort(delegate (UserDTO x, UserDTO y)
        {
            return y.GetScore().CompareTo(x.GetScore());
        });
        PrintTopUsersList();
    }

    public void PrintTopUsersList()
    {
        foreach(UserDTO user in topUsersList) {
            topUsersListText.GetComponent<Text>().text += "\n" + user.GetName() + " " + user.GetScore();
        }
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
            SaveGame();
            ResetGame();
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 150, 100), timeLeft.ToString());
    }

    public void StartGame()
    {
        //Roll the dices
        GameObject dices = GameObject.Find("Dices");
        dices.SendMessage("RollDices");
        gameState = GameState.DicesRolling;

        //Set username and score
        currentUser.SetName(userNameInput.GetComponent<InputField>().text);
        currentUser.SetScore(0);
        nameText.GetComponent<Text>().text = "Name : " + currentUser.GetName();
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

    //Daca toate zarurile s-au oprit, se porneste timerul si se preiau literele de pe fetele superioare
    public void DicesStoppedMoving()
    {
        gameState = GameState.Started;
        currentLetters = getLetters();
        print(currentLetters);
    }

    //Verificare daca exista cuvant in TRIE si returnare scor asociat.
    //returnare -1 in caz ca nu exista;
    public int CheckInTrie(string word)
    {
        
        return word.Length;
    }

    public void CheckWord()
    {
        int wordScore;
        string word = wordInput.GetComponent<InputField>().text;

        //Se face check-ul in TRIE
        wordScore = CheckInTrie(word);

        // Daca exisa cuvantul se va face update la score si se va adauga in lista din GUI
        if (wordScore >= 0)
        {
            //Am facut si o lista string de C# in caz ca avem nevoie sa salvam asta
            wordsList.Add(word);

            //Update score pt userul curent
            currentUser.SetScore(currentUser.GetScore() + wordScore);
            scoreText.GetComponent<Text>().text = "Score : " + currentUser.GetScore().ToString();

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
    }

    public void SaveGame()
    {
        //Adaugare user curent in lista de salvat si sortare dupa scor
        topUsersList.Add(currentUser);
        topUsersList.Sort(delegate (UserDTO x, UserDTO y)
        {
            return y.GetScore().CompareTo(x.GetScore());
        });
        //Stergere ultim user din lista astfel incat sa se salveze mereu maxim 10
        if (topUsersList.Count > 10)
        {
            topUsersList.RemoveAt(topUsersList.Count-1);
        }
        gameUtils.saveUsersDetails(topUsersList, FILE_PATH);
    }

    string getLetters()
    {
        //Should be called only if there isn't movemenet!
        string result = "";

        for (int i = 1; i < 10; i++)
        {
            Rigidbody x = GameObject.Find("Dice" + i.ToString()).GetComponent<Rigidbody>();

            Transform[] faces = x.GetComponentsInChildren<Transform>();
            int highestFace = 0;
            float highest = -99f;
            for (int j = 0; j < faces.Length; j++)
            {
                if (faces[j].transform.position.y >= highest)
                {
                    highest = faces[j].transform.position.y;
                    highestFace = j;
                }
            }

            //x.transform.rotation.Set(x.transform.rotation.x, 0, x.transform.rotation.z, x.transform.rotation.w);
            x.transform.rotation.Set(1, 1, 1, 0.5f);
            result += faces[highestFace].name;

        }
        return result;
    }
}
