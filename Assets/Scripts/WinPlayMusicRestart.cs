using System.Collections;
using System.Globalization;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPlayMusicRestart : MonoBehaviour, GameWin
{
	[SerializeField]
	private EndMenuController endScreen;
	[SerializeField]
	private AudioClip[] winMusic;
	[SerializeField]
	private string title;
	[SerializeField] [Multiline]
	private string[] messages;

	[Header("Win Buttons")]
	[SerializeField]
	bool showTryAgainButton=true;
	[SerializeField]
	bool showBackToMainMenuButton=true;
	[SerializeField]
	bool showRageQuitButton=false;

	private AudioSource audioSource;

	void Start(){
		audioSource = GetComponent<AudioSource>();
	}

	public  void OnGameWin(){
		GameResults results = GameController.GetLastGameResults();

		if(winMusic.Length>0){
			audioSource.clip = winMusic[Random.Range(0, winMusic.Length)];
			audioSource.Play();
		}

		
		endScreen = Instantiate(endScreen, Vector3.zero, Quaternion.identity, gameObject.transform);
		endScreen.SetTitle(title.ToUpper());
		
		string m=messages[Random.Range(0, messages.Length)];
		m = m.Replace("{GAMETYPE}", results.gameType.ToString())
			.Replace("{FUEL_LEFT}", results.fuelLeft.ToString("F2"));

		endScreen.SetSubtitle(m.ToUpper());
		
		endScreen.EnableButton(EndMenuButtons.TryAgain, showTryAgainButton);
		endScreen.EnableButton(EndMenuButtons.BackToMainMenu, showBackToMainMenuButton);
		endScreen.EnableButton(EndMenuButtons.RageQuit, showRageQuitButton);
		//endScreen.AddButton("TRY AGAIN", ()=>SceneManager.LoadScene(SceneManager.GetActiveScene().name) );
		//endScreen.AddButton("BACK TO MAIN MENU", ()=>SceneManager.LoadScene((int)Scenes.MainMenu) );
	}
}