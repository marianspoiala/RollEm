using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using UnityEngine.EventSystems;

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
    GUIStyle timerStyle;
    GameObject gameTitle;
    GameObject gameTimer;
    GameObject lettersPanelText;
    GameObject lettersPanel;

    const float GAME_TIME = 60.0f;
    const string FILE_PATH = "users.json";

    float timeLeft = 0;
    GameState gameState;
    List<string> wordsList;
    string currentLetters; 

    GameUtilsIF gameUtils;
    UserDTO currentUser;
    List<UserDTO> topUsersList;

    Trie wordsTrie;
    Dictionary<char, int> lettersScore;

    void SetLettersScore()
    {
        lettersScore = new Dictionary<char, int>();
        lettersScore.Add('a', 1);
        lettersScore.Add('b',  9);
        lettersScore.Add('c', 1);
        lettersScore.Add('d', 2);
        lettersScore.Add('e', 1);
        lettersScore.Add('f', 8);
        lettersScore.Add('g', 9);
        lettersScore.Add('h', 10);
        lettersScore.Add('i', 1);
        lettersScore.Add('j', 10);
        lettersScore.Add('k', 9);
        lettersScore.Add('l', 1);
        lettersScore.Add('m', 4);
        lettersScore.Add('n', 1);
        lettersScore.Add('o', 1);
        lettersScore.Add('p', 2);
        lettersScore.Add('q', 1);
        lettersScore.Add('r', 1);
        lettersScore.Add('s', 1);
        lettersScore.Add('t', 1);
        lettersScore.Add('u', 1);
        lettersScore.Add('v', 8);
        lettersScore.Add('w', 5);
        lettersScore.Add('x', 10);
        lettersScore.Add('y', 8);
        lettersScore.Add('z', 10);

        lettersScore.Add('ă', 1);
        lettersScore.Add('ț', 1);
        lettersScore.Add('â', 1);
        lettersScore.Add('ș', 1);
        lettersScore.Add('î', 1);
    }

    void InitialiseTrie()
    {
        int counter = 0;
        string line;

        wordsTrie = new Trie();

        // Read the file and display it line by line.
        System.IO.StreamReader file =
            new System.IO.StreamReader(@"FinalListOfWords.txt");
        while ((line = file.ReadLine()) != null)
        {
            wordsTrie.AddWord(line.Trim());
            counter++;
        }

        file.Close();
        print("There were " + counter + " lines.");
    }

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
        gameTitle = GameObject.Find("GameTitle");
        gameTimer = GameObject.Find("GameTimer");
        lettersPanelText = GameObject.Find("LettersPanelText");
        lettersPanel = GameObject.Find("LettersPanel");

        //Add listener

        //Make gameUtils services available
        gameUtils = new GameUtils();
        ResetGame();

        //Initialise trie with words from file
        InitialiseTrie();
        SetLettersScore();

        //Timer style
        timerStyle = new GUIStyle();
        timerStyle.normal.textColor = Color.black;
 
        //timerStyle.font.fontSize = 18;
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
        gameTimer.SetActive(false);
        gameTitle.SetActive(true);
        lettersPanel.SetActive(false);

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
        Boolean isFirst = true;
        foreach(UserDTO user in topUsersList) {
            if (isFirst)
            {
                topUsersListText.GetComponent<Text>().text = user.GetName() + " " + user.GetScore();
                isFirst = false;
            }
            else
            {
                topUsersListText.GetComponent<Text>().text += "\n" + user.GetName() + " " + user.GetScore();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
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
        //GUI.Label(new Rect(10, 10, 150, 100), "Timp ramas : " + timeLeft.ToString(), timerStyle);
        gameTimer.GetComponent<Text>().text = timeLeft.ToString();
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
        nameText.GetComponent<Text>().text = "Nume : " + currentUser.GetName();
        scoreText.GetComponent<Text>().text = "Scor : 0";

        //Activate word input and button
        wordBtn.SetActive(true);
        wordInput.SetActive(true);
        wordsListView.SetActive(true); 

        //Disable start button and user name input after clicking Start Game
        startBtn.SetActive(false);
        startBtn.GetComponent<Button>().onClick.RemoveAllListeners();
        userNameInput.SetActive(false);

        //Activate timer si afisare titlu
        gameTitle.SetActive(false);
        gameTimer.SetActive(true);
        gameTimer.GetComponent<Text>().text = "";

        EventSystem.current.SetSelectedGameObject(wordInput.gameObject, null);

    }

    //Daca toate zarurile s-au oprit, se porneste timerul si se preiau literele de pe fetele superioare
    public void DicesStoppedMoving()
    {
        gameState = GameState.Started;
        currentLetters = getLetters();

        //Print letters on GUI
        lettersPanel.SetActive(true);
        lettersPanelText.GetComponent<Text>().text = Regex.Replace(currentLetters, ".{1}", "$0 ");
        print(currentLetters);
    }

    //Verificare daca exista cuvant in TRIE
    //returnare scor in caz ca este cuvant valid
    //returnare -1 in caz ca nu exista;
    //returnare -2 in caz ca nu e format din litere zarurilor
    public int CheckInTrie(string word)
    {        
        word = word.ToLower();
        if (wordsList.Contains(word))
        {
            return -3;
        }

        int score = 0;
        //Put letters in an array of letters
        currentLetters = currentLetters.ToLower();
        int[] diceLetters = new int[150];
        foreach (char c in currentLetters) {
            diceLetters[c]++;
        }
        
        //Verificare daca este format din literele de pe zaruri
        //Adunare scor pt fiecare litera
        foreach (char c in word)
        {
            char x = c;
            if (x== 'ă' || x== 'â')
            {
                x = 'a';
            }
            else if (x == 'ș')
            {
                x = 's';
            }
            else if (x == 'ț')
            {
                x = 't';
            }
            else if (x == 'î')
            {
                x = 'i';
            }

            if (diceLetters[x] > 0)
            {
                score += lettersScore[c];
                diceLetters[x]--;
            }
            else
            {
                return -2;
            }
        }

        //Verificare in trie
        if (!wordsTrie.LookUp(word))
            return -1; 

        return score;
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
            scoreText.GetComponent<Text>().text = "Scor : " + currentUser.GetScore().ToString();

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
        wordInput.GetComponent<InputField>().text = "";

        EventSystem.current.SetSelectedGameObject(wordInput.gameObject, null);

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
