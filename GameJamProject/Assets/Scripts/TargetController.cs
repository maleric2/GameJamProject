using UnityEngine;
using System.Collections;

public class TargetController : MonoBehaviour {

    private HealthController healthController;
    private GameObject healthCanvas;

	// Use this for initialization
	void Start () {
        healthController = gameObject.GetComponent<HealthController>();
        healthCanvas = transform.GetChild(0).gameObject;
    }
	
	// Update is called once per frame
	void Update () {
	    if(healthController.healthSlider.value == 100.0f)
        {
            healthCanvas.SetActive(false);

            foreach (Transform target in GameManager.gm.targets)
            {
                if (this.gameObject.transform.Equals(target))
                {
                    GameManager.gm.targets.Remove(target);
                    return;
                }

            }
        }
	}
}
