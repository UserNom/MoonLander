using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

	[SerializeField]
	private Button menuItemPrefab;
	


	private GameObject menu;

	

    // Start is called before the first frame update
    void Start()
    {
		menu=gameObject;

		Button startEasyDifficulty = Instantiate(menuItemPrefab, Vector3.zero, Quaternion.identity, menu.transform);
		startEasyDifficulty.GetComponentInChildren<Text>().text = "EASY MODE, FOR UNSKILLED N00BS";
		startEasyDifficulty.onClick.AddListener( ()=> SceneManager.LoadScene((int)Scenes.EasyDifficulty));


        Button startNormalDifficulty = Instantiate(menuItemPrefab, Vector3.zero, Quaternion.identity, menu.transform);
		startNormalDifficulty.GetComponentInChildren<Text>().text = "NORMAL MODE, FOR TRUE GAMERS";
		startNormalDifficulty.onClick.AddListener( ()=> SceneManager.LoadScene((int)Scenes.NormalDifficulty));

		Button startHardDifficulty = Instantiate(menuItemPrefab, Vector3.zero, Quaternion.identity, menu.transform);
		startHardDifficulty.GetComponentInChildren<Text>().text = "HARD MODE, FOR REAL MEN (AND WOMEN, I'M NOT SEXIST)";
		startHardDifficulty.onClick.AddListener( ()=> SceneManager.LoadScene((int)Scenes.HardDifficulty));

		insertSpacer(30);
		

		Button startMission = Instantiate(menuItemPrefab, Vector3.zero, Quaternion.identity, menu.transform);
		startMission.GetComponentInChildren<Text>().text = "MISSION TO MARE TRANQUILLITATIS, FOR CAREER ASTRONAUTS";
		startMission.onClick.AddListener( ()=> SceneManager.LoadScene((int)Scenes.MissionModeEasy));

		Button startMissionSouth = Instantiate(menuItemPrefab, Vector3.zero, Quaternion.identity, menu.transform);
		startMissionSouth.GetComponentInChildren<Text>().text = "MISSION TO THE SOUTH POLE, FOR ELITE PIONEERS";
		startMissionSouth.onClick.AddListener( ()=> SceneManager.LoadScene((int)Scenes.MissionModeHard));

		insertSpacer(30);

		Button quitGameButton = Instantiate(menuItemPrefab, Vector3.zero, Quaternion.identity, menu.transform);
		quitGameButton.GetComponentInChildren<Text>().text = "QUIT, FOR HATERS, LOSERS, AND QUITTERS";
		quitGameButton.onClick.AddListener( ()=> Application.Quit() );
    }

	private void insertSpacer(float size){
		GameObject spacer = new GameObject();
		spacer.AddComponent<RectTransform>();
		//spacer.GetComponent<Text>().fontSize = size;
		//spacer.GetComponent<Text>().text = " ";
		//spacer.GetComponent<Text>().transform.
		spacer.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
		spacer.transform.parent = menu.transform;
	} 

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum Scenes{
	MainMenu,
	EasyDifficulty,
	NormalDifficulty,
	HardDifficulty,
	MissionModeEasy,
	MissionModeHard
};