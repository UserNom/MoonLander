using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public PlayerController player;
	
	public Vector3 cameraOffset;
	public float zoomInFOV;
	public float zoomOutFOV;
	
	public float zoomOutMinVelocity=5f;
	public float zoomOutMaxVelocity=20f;

	float maxZoomInAltitude = 10f;
	float maxZoomOutAltitude = 30f;

	public float camResponsiveness=5f;
	public float camZoomSpeed=2f;


	private Rigidbody playerRigidBody;
	Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
		cam.fieldOfView = (zoomInFOV + zoomOutFOV)/2;
		cam.depthTextureMode = DepthTextureMode.Depth;

		playerRigidBody = player.GetComponent<Rigidbody>();
		
		cam.transform.position = new Vector3(
			playerRigidBody.position.x+cameraOffset.x,
			playerRigidBody.position.y+cameraOffset.y,
			playerRigidBody.position.z+cameraOffset.z
		);
    }

    // Update is called once per frame
    void Update()
    {
		if(playerRigidBody == null){return;}
		// TODO use velocity in cam responsiveness? at extreme speed increase responsiveness? it's a bit jerky
		float x = playerRigidBody.position.x;
		float y = playerRigidBody.position.y;
		float zoom = TerrainDistanceZoom();
		
		
		cam.transform.position = new Vector3(
			x+cameraOffset.x,
			y+cameraOffset.y,
			cam.transform.position.z
		);

		cam.fieldOfView = (Mathf.MoveTowards(cam.fieldOfView, zoom, camZoomSpeed * Time.deltaTime));
    }

	float TerrainDistanceZoom(){
		//float altitude;
		//Debug.Log("Alt: " + terrain.SampleHeight(playerRigidBody.position));
		return (player.GetAltitude() > maxZoomInAltitude)? 
			zoomInFOV +( Mathf.Clamp(player.GetAltitude(), 0f, maxZoomOutAltitude)/maxZoomOutAltitude * (zoomOutFOV - zoomInFOV)) :
			zoomInFOV;
	}

	float playerRigidBodySpeedZoom(){
		return (playerRigidBody.velocity.magnitude > zoomOutMinVelocity)? 
			zoomInFOV +( Mathf.Clamp(playerRigidBody.velocity.magnitude, 0f, zoomOutMaxVelocity)/zoomOutMaxVelocity * (zoomOutFOV - zoomInFOV)) :
			zoomInFOV;
	}
}
