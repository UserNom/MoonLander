using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AltitudeDisplayScript : MonoBehaviour
{
    
	[SerializeField]
	private Text altitudeText;
	[SerializeField]
	private PlayerController player;
	
	
	// Start is called before the first frame update
    void Start()
    {
        altitudeText.text = "ALTITUDE : - M";
    }

    // Update is called once per frame
    void Update()
    {
		altitudeText.text = "ALTITUDE : " + ((player.GetAltitude() < 1000 ) ? player.GetAltitude().ToString("F1") +" M" : "ERROR" );
    }
}
