using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour{

	public int landingPadWidth;
	public int smoothingPasses;
	public  GameObject landingPad;
	
    // Start is called before the first frame update
    void Start()
    {
        Terrain t = GetComponent<Terrain>();
		int hmWidth= t.terrainData.heightmapResolution;
		int hmHeight= t.terrainData.heightmapResolution;

		float[] oneDimensionalTerrain = new float[hmWidth];

		float[,] genTerrainValues = new float[hmWidth,hmHeight];

		for(int x=0; x<oneDimensionalTerrain.Length; x++){
			oneDimensionalTerrain[x]=Random.value;
		}
		//[z,x]
		for( int smooth=0; smooth < smoothingPasses; smooth++){
			
			for(int x = 1; x<oneDimensionalTerrain.Length-1; x++){
				if(oneDimensionalTerrain[x-1] < oneDimensionalTerrain[x] && oneDimensionalTerrain[x+1] < oneDimensionalTerrain[x]){
					oneDimensionalTerrain[x]= (oneDimensionalTerrain[x-1] + oneDimensionalTerrain[x+1]*2)/3;
				}else if( oneDimensionalTerrain[x+1] > oneDimensionalTerrain[x] ){
					oneDimensionalTerrain[x]= (oneDimensionalTerrain[x-1]*2 + oneDimensionalTerrain[x+1])/3;
				}
			}
		}

		//create landing pad area in terrain profile
		int landingPadLocation = Random.Range(1, oneDimensionalTerrain.Length - 1 - landingPadWidth);
		for(int i = 0; i < landingPadWidth; i++){
			oneDimensionalTerrain[landingPadLocation+i]=oneDimensionalTerrain[landingPadLocation];
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

		float padX= (landingPadLocation + landingPadWidth/2)*t.terrainData.size.x/ oneDimensionalTerrain.Length,
			padZ=0f,
			padY=t.SampleHeight(new Vector3(padX + terrainOffset.x, 0f, padZ));
		GameObject landingPadInstance = Instantiate(landingPad, new Vector3(padX + terrainOffset.x, padY + terrainOffset.y, padZ) , Quaternion.identity, t.transform);
		landingPadInstance.AddComponent<LandingZoneController>();
		//landingPadInstance

		//Debug.Log("Landing pad location: " + landingPadLocation);
		//Debug.Log("Landing pad padx: " + padX);
		//Debug.Log("sampleheight: " + t.SampleHeight(new Vector3(padX, 0f, padZ)));
		//Debug.Log("Landing pad padz: " + padZ);
		//Debug.Log("Terrain Data Size X: " + t.terrainData.size.x);
		//Debug.Log("Terrain offset: -x: " +terrainOffset.x + " -y: "+terrainOffset.y+" -z: "+terrainOffset.z);


    }
}
