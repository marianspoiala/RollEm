using UnityEngine;
using System.Collections;
using System;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    float rotation = 0.0f;
    void Update () {
        if(!isThereMovement())
        {
            print(getLetters());
        }
        
	}

    bool isThereMovement()
    {
        for(int i = 1; i < 10; i++)
        {
            Rigidbody x = GameObject.Find("Dice" + i.ToString()).GetComponent<Rigidbody>();
            if (x.velocity.sqrMagnitude > 0.1f)
            {
                print(x.velocity.sqrMagnitude);
                return true;
            }
        }
        print("No movement");
        return false;
    }

    String getLetters()
    {
        //Should be called only if there isn't movemenet!
        string result = "";
        for(int i = 1; i < 10; i++)
        {
            Rigidbody x = GameObject.Find("Dice" + i.ToString()).GetComponent<Rigidbody>();

            Transform[] faces = x.GetComponentsInChildren<Transform>();
            int highestFace = 0;
            float highest = -99f;
            for(int j = 0; j < faces.Length; j++) {
                if(faces[j].transform.position.y >= highest)
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
