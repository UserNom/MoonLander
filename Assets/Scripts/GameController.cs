using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
/* 	[SerializeField]
	private EndOfGame youWin, youLose; */

	private GameWin gameWin;
	//[SerializeField]
	private GameLoss gameLoss;
    // Start is called before the first frame update
    void Start()
    {
        gameWin = GetComponent<GameWin>();
		gameLoss = GetComponent<GameLoss>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)){
			RestartIn(0);
		}
		if(Input.GetKeyDown(KeyCode.Escape)){
			SceneManager.LoadScene((int)Scenes.MainMenu);
			//Application.Quit();
		}
		if(Input.GetKeyDown(KeyCode.P)){
			Time.timeScale = (Time.timeScale!=0)? 0: 1;
			//TODO: show pause menu
		}
    }

	public void Win(){
		if(gameWin!=null){
			gameWin.OnGameWin();
		}else{
			Debug.LogWarning("gameWin is null. You should attach a GameWin script to the GameController");
			RestartIn(0);
		}
	}

	public void Lose(float t = 2f){
		if(gameLoss!=null){
			WaitThen(t, ()=>gameLoss.OnGameLoss() );
		}else{
			Debug.LogWarning("gameLoss is null. You should attach a GameLoss script to the GameController");
			RestartIn(0);
		}
	}

	public void RestartIn(float t){
		StartCoroutine(ResetLevel(t));
	}

	private IEnumerator ResetLevel(float t){
		yield return new WaitForSeconds(t);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void WaitThen(float t , UnityAction x){
		StartCoroutine( _WaitThen(t, ()=>gameLoss.OnGameLoss() ) );
	}

	private IEnumerator _WaitThen(float t , UnityAction x){
		yield return new WaitForSeconds(t);
		x();
	}
}
