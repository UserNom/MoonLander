using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	private float mainThrusterForce = 100f;
	[SerializeField]
	private float sideThrusterForce = 15f;
	[SerializeField]
	private float fuel = 100f;
	[SerializeField]
	private float mainThrusterFuelConsumption = 10f;
	[SerializeField]
	private float sideThrusterFuelConsumption = 1f;
	[SerializeField]
	private GameObject explosion;
	[SerializeField]
	private AudioClip rocketSound, explosionSound,fatalDamageCrashSound;
	[SerializeField]
	private float instaDeathCapsuleImpactForce,instaDeathLegImpactForce,fatalDamageLegImpactForce;

	private Rigidbody rb;
	private ParticleSystem mainThruster;
	private bool mainThrusterFiring, sideThrusterFiring, isOutOfControl;
	private List<ContactPoint> contacts = new List<ContactPoint>(5);
	private int altitudeLayerMask = 1<<9 | 1<<11 | 1<<12 | 1<<13;
	private AudioSource audioSource;


	

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
		mainThruster = GetComponentInChildren<ParticleSystem>();
		audioSource = GetComponent<AudioSource>();
		mainThrusterFiring = false;
		sideThrusterFiring = false;
		isOutOfControl = false;
		// TODO: check altitude, teleport 10m above terrain if below 10m? 
		float startAlt=GetAltitude();
		if(startAlt < 10 || startAlt > 20){
			rb.position = new Vector3(rb.position.x, rb.position.y - startAlt + 15, rb.position.z); 
		}

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K)){
			StartCoroutine(LossOfControl());
		}
    }

	void FixedUpdate(){
		if(fuel > 0){
			float moveHorizontal = Input.GetAxisRaw("Horizontal");
			float moveVertical = Mathf.Clamp(Input.GetAxisRaw("Vertical"), 0f, 1f);
			

			if(sideThrusterFiring){
				fuel -= sideThrusterFuelConsumption * Time.fixedDeltaTime;
			}
			sideThrusterFiring = (moveHorizontal!=0);

			if(mainThrusterFiring){
				fuel -= mainThrusterFuelConsumption * Time.fixedDeltaTime;
			}
			mainThrusterFiring = (moveVertical!=0 || isOutOfControl);


		
			rb.AddForce(rb.transform.up * moveVertical * mainThrusterForce * Time.fixedDeltaTime);
			rb.AddTorque(Vector3.back * moveHorizontal * sideThrusterForce * Time.fixedDeltaTime);
		}else if ( !isOutOfControl ){
			//out of fuel
			sideThrusterFiring = false;
			mainThrusterFiring = false;
		}
		if(mainThrusterFiring){
			mainThruster.Play();
			if(!audioSource.isPlaying){
				audioSource.clip = rocketSound;
				audioSource.Play();
			}
		}else{
			mainThruster.Stop();
			if(audioSource.isPlaying && audioSource.clip == rocketSound){
				audioSource.Stop();
			}
		}
		
	}


	void OnCollisionEnter(Collision collision){
		
		//Check the impulse. if impulse is on capsule or impulse too big, smash the ship, end the game, U LOSE LOZER!

		collision.GetContacts(contacts);
		
		Debug.Log(contacts[0].thisCollider +" hit "
			+ collision.collider 
			+ ". Impulse: "+ collision.impulse.magnitude
			+ ". Relative velocity: " +collision.relativeVelocity.magnitude) ;

		if(contacts[0].thisCollider.name.StartsWith("Leg")){
			if(collision.impulse.magnitude > instaDeathLegImpactForce){
				Explode();
			}else if(collision.impulse.magnitude > fatalDamageLegImpactForce){
				//AudioClip.PlayOneShot(fatalDamageCrashSound) ;
				//simultaneousAudioSource.Play();
				StartCoroutine(LossOfControl());
			}
		}else if(collision.impulse.magnitude > instaDeathCapsuleImpactForce){
			Explode();
		}
		
	}




	public float GetRemainingFuel(){
		if(fuel<0){
			return 0;
		}
		return fuel;
	}

	public float GetAltitude(){
		RaycastHit hit;
		if(rb!=null && Physics.Raycast(rb.position, Vector3.down, out hit, 100f, altitudeLayerMask) ){
			return hit.distance;
		}
		else return 10000f;
	}

	public void Explode(){
		Transform t = rb.transform;
		Destroy(gameObject);
		AudioSource.PlayClipAtPoint(explosionSound, t.position);
		Instantiate(explosion, t.position, t.rotation);
		GetComponentInParent<GameController>().Lose();
	}

	IEnumerator LossOfControl(){
		isOutOfControl = true;
		float startOfLOC = Time.time;
		rb.constraints = RigidbodyConstraints.FreezeRotationY;
		float direction = Random.Range(-1f, 1f);
		float direction2 = Random.Range(-1f,1f);
		do{
			rb.AddForce(rb.transform.up * 5 * mainThrusterForce * Time.fixedDeltaTime);
			rb.AddForce(rb.transform.forward * 5 * direction * mainThrusterForce * Time.fixedDeltaTime);
			rb.AddForce(rb.transform.right * 10 * direction2 * mainThrusterForce * Time.fixedDeltaTime);
			rb.AddTorque(Vector3.right * 20 * direction * sideThrusterForce * Time.fixedDeltaTime);
			fuel -= mainThrusterFuelConsumption * Time.fixedDeltaTime * 5;
			yield return new WaitForSeconds(.1f);
		}while(fuel > 0 && (Time.time - startOfLOC) < 5);
		Explode();
	}

	

}
