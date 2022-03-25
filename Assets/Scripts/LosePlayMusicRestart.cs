using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePlayMusicRestart : MonoBehaviour, GameLoss
{
	[SerializeField]
	private AudioClip[] loseMusic;
	[SerializeField]
	private EndMenuController endScreen;
	
	[SerializeField]
	private string[] title;
	[SerializeField] [Multiline]
	private string[] messages;

	[Header("Lose Buttons")]
	[SerializeField]
	bool showTryAgainButton=true;
	[SerializeField]
	bool showBackToMainMenuButton=true;
	[SerializeField]
	bool showRageQuitButton=true;

	private AudioSource audioSource;

	void Start(){
		audioSource = GetComponent<AudioSource>();
	}
	
    public void OnGameLoss(){
		GameResults results = GameController.GetLastGameResults();
		
		if(loseMusic.Length>0){
			audioSource.clip = loseMusic[Random.Range(0, loseMusic.Length)];
			audioSource.Play();
		}
		
		endScreen = Instantiate(endScreen, Vector3.zero, Quaternion.identity, gameObject.transform);
		
		endScreen.SetTitle(title[Random.Range(0, title.Length)].ToUpper());
		
		string m=messages[Random.Range(0, messages.Length)];
		m = m.Replace("{GAMETYPE}", results.gameType.ToString())
			.Replace("{FUEL_LEFT}", results.fuelLeft.ToString("F2"));

		endScreen.SetSubtitle(m.ToUpper());

		endScreen.EnableButton(EndMenuButtons.TryAgain, showTryAgainButton);
		endScreen.EnableButton(EndMenuButtons.BackToMainMenu, showBackToMainMenuButton);
		endScreen.EnableButton(EndMenuButtons.RageQuit, showRageQuitButton);
	}
}