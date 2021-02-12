using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPlaySillyMusicAndGoToMenu : MonoBehaviour, GameWin
{
	[SerializeField]
	private AudioClip[] winMusic;
	[SerializeField]
	private EndMenuController endScreen;


	private AudioSource audioSource;

	void Start(){
		audioSource = GetComponent<AudioSource>();
	}

	public  void OnGameWin(){
		Debug.Log("WINNING!");
		audioSource.clip = winMusic[Random.Range(0, winMusic.Length)];
		audioSource.Play();
		endScreen = Instantiate(endScreen, Vector3.zero, Quaternion.identity, gameObject.transform);
		endScreen.SetTitle("YOU WIN");
		endScreen.SetSubtitle("YOU WANT A MEDAL OR SOMETHING?\nGO PLAY IN NORMAL MODE YOU WUSS.");
		endScreen.AddButton("BACK TO MAIN MENU", ()=>SceneManager.LoadScene((int)Scenes.MainMenu) );
		//endScreen.Display(true);

	}
}
