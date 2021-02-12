using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePlayMusicGoToMainMenu : MonoBehaviour, GameLoss
{
	[SerializeField]
	private AudioClip[] winMusic;
	[SerializeField]
	private EndMenuController endScreen;

	private AudioSource audioSource;

	void Start(){
		audioSource = GetComponent<AudioSource>();
	}
	
    public void OnGameLoss(){
		audioSource.clip = winMusic[Random.Range(0, winMusic.Length)];
		audioSource.Play();
		endScreen = Instantiate(endScreen, Vector3.zero, Quaternion.identity, gameObject.transform);
		endScreen.SetTitle("YOU MADE AN ATTEMPT");
		endScreen.SetSubtitle("BUT YOU FAILED!\nYOU WERE TRIED AND FOUND LACKING!\nGO PLAY IN NORMAL MODE, YOU GOD DAMNED WIMP.");
		endScreen.AddButton("BACK TO MAIN MENU", ()=>SceneManager.LoadScene((int)Scenes.MainMenu) );
	}
}