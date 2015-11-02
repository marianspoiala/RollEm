using UnityEngine;
using System.Collections;

public class ApplyForceInRandomDirection : MonoBehaviour {

    public ForceMode forceMode;
    public float ForceAmount = 10.0f;
    public float AngularForceAmount = 10.0f;
    public bool isMoving = true;
    public bool started = false;
	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().mass = 0;
        GetComponent<Rigidbody>().useGravity = started;
    }

    // Update is called once per frame
    void Update () {
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
        if(isMoving && started && GetComponent<Rigidbody>().velocity == new Vector3(0,0,0))
        {
            print("M-am oprit!");
            isMoving = false;
        }
	}
}
