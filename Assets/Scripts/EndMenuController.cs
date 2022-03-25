using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndMenuController : MonoBehaviour
{
	[SerializeField]
	private Button menuItemPrefab;
	[SerializeField]
	private Text title;
	[SerializeField]
	private Text subtitle;
	[SerializeField]
	private LayoutGroup layout;

	//private List<Button> buttons = new List<Button>();
	private Button TryAgainButton, BackToMainMenuButton, RageQuitButton;


	private GameObject menu;
    //This function is always called before any Start functions and also just after a prefab is instantiated.
    void Awake()
    {
        //menu = gameObject;
		TryAgainButton = AddButton("TRY AGAIN", ()=>SceneManager.LoadScene(SceneManager.GetActiveScene().name) );
		TryAgainButton.gameObject.SetActive(false);

		BackToMainMenuButton = AddButton("BACK TO MAIN MENU", ()=>SceneManager.LoadScene((int)Scenes.MainMenu) );
		BackToMainMenuButton.gameObject.SetActive(false);

		RageQuitButton = AddButton("RAAAAAAAGE QUIT!", ()=>Application.Quit() );
		RageQuitButton.gameObject.SetActive(false);
    }

	public Button AddButton(string text, UnityAction x){
		Button button = Instantiate(menuItemPrefab, Vector3.zero, Quaternion.identity, layout.transform);
		button.GetComponentInChildren<Text>().text = text;
		button.onClick.AddListener( x );
		return button;
	}

	public void SetTitle(string t){
		title.text = t;
	}

	public void SetSubtitle(string t){
		subtitle.text = t;
	}

	public void EnableButton(EndMenuButtons button, bool enable){
		switch(button){
			case EndMenuButtons.BackToMainMenu:
				BackToMainMenuButton.gameObject.SetActive(enable);
				break;
			case EndMenuButtons.TryAgain:
				TryAgainButton.gameObject.SetActive(enable);
				break;
			case EndMenuButtons.RageQuit:
				RageQuitButton.gameObject.SetActive(enable);
				break;
			default:
				break;
		}
	}



	/* public void Display(bool display){
		menu.SetActive(display);
	} */
}
public enum EndMenuButtons{
	BackToMainMenu,
	TryAgain,
	RageQuit
}