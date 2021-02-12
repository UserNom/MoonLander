using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelDisplayScript : MonoBehaviour
{

	[SerializeField]
	private PlayerController playerController;
	
	[SerializeField]
	private Text fuelIndicatorText;
	[SerializeField]
	private Slider fuelDisplay;
    
	// Start is called before the first frame update
    void Start()
    {
		fuelDisplay.maxValue = playerController.GetRemainingFuel();
		fuelDisplay.minValue = 0f;

		fuelIndicatorText.text = "FUEL";
    }

    // Update is called once per frame
    void Update()
    {
        fuelDisplay.value = playerController.GetRemainingFuel();
    }
}
