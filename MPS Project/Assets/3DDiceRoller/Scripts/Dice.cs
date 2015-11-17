using UnityEngine;
using System.Collections;

public class Dice : MonoBehaviour {

    public ForceMode forceMode;
    public float ForceAmount = 10.0f;
    public float AngularForceAmount = 10.0f;
    public bool isMoving = true;
    public bool started = false;
    public float initialXPosition;
    public float initialYPosition;
    public float initialZPosition;
    // Use this for initialization
    void Start () {
        GetComponent<Rigidbody>().mass = 0;
        GetComponent<Rigidbody>().useGravity = started;
        initialZPosition = GetComponent<Rigidbody>().position.z;
        initialXPosition = GetComponent<Rigidbody>().position.x;
        initialYPosition = GetComponent<Rigidbody>().position.y;
    }

    // Update is called once per frame
    void Update () {
        /*if (Input.GetKeyDown("space") && (!started || true))
        {
            started = true;
            GetComponent<Rigidbody>().useGravity = started;
            GetComponent<Rigidbody>().mass = 1;
            Vector3 force = new Vector3((Random.value - 0.5f) * ForceAmount,
            (Random.value / 2 + 0.5f) * ForceAmount * 2,
            (Random.value - 0.5f) * ForceAmount);

            GetComponent<Rigidbody>().AddForce(force, forceMode);
            GetComponent<Rigidbody>().AddTorque(Random.onUnitSphere*2, forceMode);
        }*/
       
        if (isMoving && started && GetComponent<Rigidbody>().velocity == new Vector3(0,0,0) && initialZPosition != GetComponent<Rigidbody>().position.z)
        {
            print("M-am oprit!");
            isMoving = false;
            started = false;
            SendMessageUpwards("RegisterStopped");
        }
	}

    public void StartRolling()
    {
        GetComponent<Rigidbody>().transform.position = new Vector3(initialXPosition, initialYPosition, initialZPosition);// initialZPosition;
        if (!started)
        {
            started = true;
            isMoving = true;
            GetComponent<Rigidbody>().useGravity = started;
            GetComponent<Rigidbody>().mass = 1;
            Vector3 force = new Vector3((Random.value - 0.5f) * ForceAmount,
            (Random.value / 2 + 0.5f) * ForceAmount * 2,
            (Random.value - 0.5f) * ForceAmount);

            GetComponent<Rigidbody>().AddForce(force, forceMode);
            GetComponent<Rigidbody>().AddTorque(Random.onUnitSphere * 2, forceMode);
        }
    }
}
