using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinShowStatsAndRestart : MonoBehaviour, GameWin
{
	[SerializeField]
	private EndMenuController endScreen;
	[SerializeField]
	private PlayerController player;

	public  void OnGameWin(){
		Debug.Log("WINNING!");
		//TODO show stats
		endScreen = Instantiate(endScreen, Vector3.zero, Quaternion.identity, gameObject.transform);
		endScreen.SetTitle("YOU WIN");
		endScreen.SetSubtitle("YOU HAVE " + player.GetRemainingFuel().ToString("F2") + 
			" LITERS OF FUEL REMAINING. \n I AM PROUD OF YOU.\n YOUR NATION IS PROUD OF YOU.\n NOW DO IT AGAIN AND SAVE MORE FUEL THIS TIME.");
		endScreen.AddButton("TRY AGAIN", ()=>SceneManager.LoadScene(SceneManager.GetActiveScene().name) );
		endScreen.AddButton("BACK TO MAIN MENU", ()=>SceneManager.LoadScene((int)Scenes.MainMenu) );
	}
}
