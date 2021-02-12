using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseAndRestart : MonoBehaviour, GameLoss
{
	[SerializeField]
	private EndMenuController endScreen;

	public void OnGameLoss(){
		Debug.Log("LOSING!");

		endScreen = Instantiate(endScreen, Vector3.zero, Quaternion.identity, gameObject.transform);
		endScreen.SetTitle("YOU LOSE");
		endScreen.SetSubtitle("YOU LOST. I SUGGEST YOU TRY AGAIN.");
		endScreen.AddButton("TRY AGAIN", ()=>SceneManager.LoadScene(SceneManager.GetActiveScene().name) );
		endScreen.AddButton("BACK TO MAIN MENU", ()=>SceneManager.LoadScene((int)Scenes.MainMenu) );
		endScreen.AddButton("RAAAAAAAGE QUIT!", ()=>Application.Quit() );
	}
}
