using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderOnMouseOverFill : MonoBehaviour {

    private Slider buttonSlider;

	// Use this for initialization
	void Start () {
        buttonSlider = gameObject.GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnMouseOver()
    {
        Debug.Log("TWST");
        
    }
}
