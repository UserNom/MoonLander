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

	private AudioSource audioSource;

	void Start(){
		audioSource = GetComponent<AudioSource>();
	}
	
    public void OnGameLoss(){
		audioSource.clip = loseMusic[Random.Range(0, loseMusic.Length)];
		audioSource.Play();
		endScreen = Instantiate(endScreen, Vector3.zero, Quaternion.identity, gameObject.transform);
		
		endScreen.SetTitle(title[Random.Range(0, title.Length)].ToUpper());
		endScreen.SetSubtitle(messages[Random.Range(0, messages.Length)].ToUpper());

		endScreen.AddButton("TRY AGAIN", ()=>SceneManager.LoadScene(SceneManager.GetActiveScene().name) );
		endScreen.AddButton("BACK TO MAIN MENU", ()=>SceneManager.LoadScene((int)Scenes.MainMenu) );
		endScreen.AddButton("RAAAAAAAGE QUIT!", ()=>Application.Quit() );
	}
}