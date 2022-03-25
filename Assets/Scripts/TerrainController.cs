using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour{

	public int landingPadWidth;
	public int smoothingPasses;
	//TODO https://docs.unity3d.com/ScriptReference/PropertyDrawer.html
	//https://docs.unity3d.com/ScriptReference/EditorGUILayout.MinMaxSlider.html
	//https://gist.github.com/frarees/9791517
	[SerializeField]
	public Vector2Int numLandingLocations = new Vector2Int(1,1);
	private int landingSites =1;
	public  GameObject landingPad;
	
    // Start is called before the first frame update
    void Awake()
    {
		landingSites = Random.Range(numLandingLocations.x, numLandingLocations.y);
        Terrain t = GetComponent<Terrain>();
		int hmWidth= t.terrainData.heightmapResolution;
		int hmHeight= t.terrainData.heightmapResolution;

		float[] oneDimensionalTerrain = new float[hmWidth];

		float[,] genTerrainValues = new float[hmWidth,hmHeight];

		for(int x=0; x<oneDimensionalTerrain.Length; x++){
			oneDimensionalTerrain[x]=Random.value;
		}
		//[z,x]
		//Smooth the terrain 
		for( int smooth=0; smooth < smoothingPasses; smooth++){
			
			for(int x = 1; x<oneDimensionalTerrain.Length-1; x++){
				if(oneDimensionalTerrain[x-1] < oneDimensionalTerrain[x] && oneDimensionalTerrain[x+1] < oneDimensionalTerrain[x]){
					oneDimensionalTerrain[x]= (oneDimensionalTerrain[x-1] + oneDimensionalTerrain[x+1]*2)/3;
				}else if( oneDimensionalTerrain[x+1] > oneDimensionalTerrain[x] ){
					oneDimensionalTerrain[x]= (oneDimensionalTerrain[x-1]*2 + oneDimensionalTerrain[x+1])/3;
				}
			}
		}

		//create landing pad areas in terrain profile
		int zoneWidth = (oneDimensionalTerrain.Length - 1 - landingPadWidth)/landingSites;
		int[] landingPadLocation = new int[landingSites];
		for(int lz = 0; lz<landingSites; lz++){
			int rangeStart= zoneWidth * lz + 1;
			landingPadLocation[lz] = Random.Range(rangeStart, rangeStart + zoneWidth - landingPadWidth);
			for(int i = 0; i < landingPadWidth; i++){
				oneDimensionalTerrain[landingPadLocation[lz]+i] = oneDimensionalTerrain[landingPadLocation[lz]];
			}
		}
		
		
		//cap the terrain ends
		oneDimensionalTerrain[0] = 0;
		oneDimensionalTerrain[oneDimensionalTerrain.Length-1] = 0;


		//stretch out the terrain profile to create the 3d terrain
		for(int w=0; w<genTerrainValues.GetLength(0); w++){
			for(int l=0; l<genTerrainValues.GetLength(1); l++){
				//cap the terrain ends
				if(l==0 || l==genTerrainValues.GetLength(1)-1){
					genTerrainValues[l,w] = 0f;
				}else{
					genTerrainValues[l,w] = oneDimensionalTerrain[w];
				}
			}
		}



		

		t.terrainData.SetHeights(0,0, genTerrainValues);

		//Instantiate landing pad
		Vector3 terrainOffset=t.transform.position;
		for(int lz = 0; lz<landingSites; lz++){
			float padX= (landingPadLocation[lz] + landingPadWidth/2)*t.terrainData.size.x/ oneDimensionalTerrain.Length,
				padZ=0f,
				padY=t.SampleHeight(new Vector3(padX + terrainOffset.x, 0f, padZ));
			GameObject landingPadInstance = Instantiate(landingPad, new Vector3(padX + terrainOffset.x, padY + terrainOffset.y, padZ) , Quaternion.identity, t.transform);
			landingPadInstance.AddComponent<LandingZoneController>();
		}
		//landingPadInstance

		//Debug.Log("Landing pad location: " + landingPadLocation);
		//Debug.Log("Landing pad padx: " + padX);
		//Debug.Log("sampleheight: " + t.SampleHeight(new Vector3(padX, 0f, padZ)));
		//Debug.Log("Landing pad padz: " + padZ);
		//Debug.Log("Terrain Data Size X: " + t.terrainData.size.x);
		//Debug.Log("Terrain offset: -x: " +terrainOffset.x + " -y: "+terrainOffset.y+" -z: "+terrainOffset.z);


    }
}
