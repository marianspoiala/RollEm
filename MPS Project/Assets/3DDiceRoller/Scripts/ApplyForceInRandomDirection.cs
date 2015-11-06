using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ApplyForceInRandomDirection : MonoBehaviour {

    public ForceMode forceMode;
    public float ForceAmount = 10.0f;
    public float AngularForceAmount = 10.0f;
    public bool isMoving = true;
    public bool started = false;

    public string scoreFileName = "score.json";
    public static List<UserDTO> users;
    GameUtilsIF gameUtils;

    // Use this for initialization
    void Start() {
        GetComponent<Rigidbody>().mass = 0;
        GetComponent<Rigidbody>().useGravity = started;

        //Initialise game data
        gameUtils = new GameUtils();
        users = gameUtils.loadUsersDetails(scoreFileName);
        //gameUtils.startTimer(5000);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown("space") && !started)
        {
            started = true;
            GetComponent<Rigidbody>().useGravity = started;
            GetComponent<Rigidbody>().mass = 1;
            Vector3 force = new Vector3((Random.value - 0.5f) * ForceAmount,
            0,
            (Random.value - 0.5f) * ForceAmount);

            GetComponent<Rigidbody>().AddForce(force, forceMode);
            GetComponent<Rigidbody>().AddTorque(Random.onUnitSphere, forceMode);
        }

        if (isMoving && started && GetComponent<Rigidbody>().velocity == new Vector3(0, 0, 0))
        {
            print("M-am oprit!");
            isMoving = false;
        }
    }

    bool GUIKeyDown(KeyCode key)
    {
        if (Event.current.type == EventType.KeyDown)
            return (Event.current.keyCode == key);
        return false;
    }

    void OnGUI()
    {
        //On button S press, the game data will be saved
        if (GUIKeyDown(KeyCode.S))
        {
            //Sample JSON - to be deleted
            UserDTO user1 = new UserDTO("Test1", 2);
            UserDTO user2 = new UserDTO("Test2", 65);
            users.Add(user1);
            users.Add(user2);

            gameUtils.saveUsersDetails(users, scoreFileName);
        }
    }
}
