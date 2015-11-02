using UnityEngine;
using System.Collections;

public class ApplyForceInRandomDirection : MonoBehaviour {

    public ForceMode forceMode;
    public float ForceAmount = 10.0f;
    public float AngularForceAmount = 10.0f;
	// Use this for initialization
	void Start () {
        Vector3 force = new Vector3((Random.value-0.5f) * ForceAmount, 
            0, 
            (Random.value - 0.5f) * ForceAmount);

        GetComponent<Rigidbody>().AddForce(force, forceMode);
        GetComponent<Rigidbody>().AddTorque(Random.onUnitSphere, forceMode);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("space"))
        {

        }
	}
}
