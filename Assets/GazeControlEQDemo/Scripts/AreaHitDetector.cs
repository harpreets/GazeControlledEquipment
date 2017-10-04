using UnityEngine;
using System.Collections;

public class AreaHitDetector : MonoBehaviour {

    public int currentActiveLightArea = -1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter() {

    }

    void OnTriggerStay(Collider other) {
        if (other.tag == "Hittable") {
            Renderer goRenderer = other.GetComponent<Renderer>();
            goRenderer.material.color = Color.green;

            currentActiveLightArea = other.GetComponent<AreaLightIDCorrelation>().lightID;
            Debug.Log("Current area = " + currentActiveLightArea.ToString());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Hittable")
        {
            Renderer goRenderer = other.GetComponent<Renderer>();
            goRenderer.material.color = Color.grey;

            currentActiveLightArea = -1;
            Debug.Log("Current area = " + currentActiveLightArea.ToString());
        }
    }
}
