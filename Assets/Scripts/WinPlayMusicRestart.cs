using System.Collections;
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

	private AudioSource audioSource;

	void Start(){
		audioSource = GetComponent<AudioSource>();
	}

	public  void OnGameWin(){
		audioSource.clip = winMusic[Random.Range(0, winMusic.Length)];
		audioSource.Play();
		//TODO show stats
		endScreen = Instantiate(endScreen, Vector3.zero, Quaternion.identity, gameObject.transform);
		endScreen.SetTitle(title.ToUpper());
		endScreen.SetSubtitle(messages[Random.Range(0, messages.Length)].ToUpper());
		endScreen.AddButton("TRY AGAIN", ()=>SceneManager.LoadScene(SceneManager.GetActiveScene().name) );
		endScreen.AddButton("BACK TO MAIN MENU", ()=>SceneManager.LoadScene((int)Scenes.MainMenu) );
	}
}