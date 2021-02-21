using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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


	private GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        //menu = gameObject;
		
    }

	public void AddButton(string text, UnityAction x){
		Button button = Instantiate(menuItemPrefab, Vector3.zero, Quaternion.identity, layout.transform);
		button.GetComponentInChildren<Text>().text = text;
		button.onClick.AddListener( x );
		//buttons.Add(button);
	}

	public void SetTitle(string t){
		title.text = t;
	}

	public void SetSubtitle(string t){
		subtitle.text = t;
	}

	/* public void Display(bool display){
		menu.SetActive(display);
	} */
}
