using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LandingZoneController : MonoBehaviour
{
    
	private List<ContactPoint> contacts = new List<ContactPoint>(5);
	private float landingPadLastFullContact;
	private bool isOnLandingPad;
	Dictionary<string,bool> LanderLegs = new Dictionary<string, bool>();
	private Coroutine winTimer;
	private Material padMaterial;
	private Color originalPadColor, originalPadEmissionColor;

	private static List<LandingZoneController> landingPads = new List<LandingZoneController>();

	private void Awake() {
		landingPads.Add(this);	
	}

	// Start is called before the first frame update
    void Start()
    {
        isOnLandingPad = false;
		for(int i = 1; i<=4; i++){
			LanderLegs.Add("Leg"+i, false);
		}
		padMaterial = GetComponent<Renderer>().material;
		originalPadColor = padMaterial.color;
		originalPadEmissionColor = padMaterial.GetColor("_EmissionColor");
		
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	IEnumerator WinTimer(float t){
		yield return new  WaitForSeconds(t);
		if(landingPads.Count>1 && landingPads.Contains(this)){
			landingPads.Remove(this);
			Destroy(this);
			yield break;
		}
		Debug.Log("U Win!!!!!11!");
		GetComponentInParent<GameController>().Win();
	}


	void OnCollisionStay(Collision collision){
		
		if(isOnLandingPad ){
			/* Debug.Log("Lander on Landing pad for "+(Time.time - landingPadLastFullContact));
			if(Time.time - landingPadLastFullContact > 2f){
				GetComponentInParent<GameController>().RestartIn(0.5f);
			} */
			
			
		}else{
			
			collision.GetContacts(contacts);
			
			for(int i = 0; i<collision.contactCount; i++){
				Debug.Log(contacts[i].thisCollider.name+" collided with "+contacts[i].otherCollider.name);
				
				
				if(contacts[i].otherCollider.name.StartsWith("Leg")){
					
					LanderLegs[contacts[i].otherCollider.name]=true;
					
				}
			}
			for(int i = 0; i<LanderLegs.Count; i++){
				Debug.Log(LanderLegs.ElementAt(i).Key + " on landing pad: " + LanderLegs.ElementAt(i).Value);
			}
			if(LanderLegs.Values.All(x=>x==true)){
				//landingPadLastFullContact=Time.time;
				isOnLandingPad=true;
				padMaterial.color=new Color(0,1,0);
				padMaterial.SetColor("_EmissionColor", new Color(0.01f,0.3f,0));
				winTimer=StartCoroutine(WinTimer(1.5f));
			}
		}

	}

	void OnCollisionExit(Collision collision){
		Debug.Log(contacts[0].thisCollider.name+" no longer touching "+contacts[0].otherCollider.name);
		collision.GetContacts(contacts);
		//for(int i = 0; i<collision.contactCount; i++){
		if(contacts[0].otherCollider.name.StartsWith("Leg")){
			//contacts[i].otherCollider.name.Substring(-1);
			isOnLandingPad = false;
			padMaterial.color=originalPadColor;
			padMaterial.SetColor("_EmissionColor", originalPadEmissionColor);
			if(winTimer!=null){
				StopCoroutine(winTimer);
				Debug.Log("STOP! NO WINNING! ABORT!");
			}
			LanderLegs[contacts[0].otherCollider.name]=false;
		}
		//}
	}

	void OnDestroy(){
		Destroy(padMaterial);
		if(landingPads.Contains(this)){
			landingPads.Remove(this);
		}
	}
}
