using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapCamController : MonoBehaviour
{
	public Light[] minimapLights;
	public RawImage minimap;
	public Terrain map;
	private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
		cam=GetComponent<Camera>();
        cam.aspect=map.terrainData.size.x/map.terrainData.size.y;
		int minimapHeight=128;
		int minimapWidth=(int)(minimapHeight*cam.aspect);
		cam.orthographicSize = map.terrainData.size.y / 2;
		cam.transform.position=new Vector3(0f,cam.orthographicSize,cam.transform.position.z);
		cam.targetTexture.height=minimapHeight;
		cam.targetTexture.width=minimapWidth;
		minimap.SetNativeSize();
    }

    // Update is called once per frame
    void Update()
    {

    }

	void OnPreCull(){
		foreach(Light l in minimapLights){
			l.enabled = true;
		}
	}

	void OnPreRender(){
		foreach(Light l in minimapLights){
			l.enabled = true;
		}
	}

	void OnPostRender(){
		foreach(Light l in minimapLights){
			l.enabled = false;
		}
	}
}
