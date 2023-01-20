using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
	[SerializeField] GameObject optionsUI; 
	[SerializeField] GameObject levelSelectUI;
	[SerializeField] GameObject titleUI;


	public void OnStartScene(string sceneName)
	{
		Game.Instance.OnLoadScene(sceneName);
	}

	public void OnQuit()
	{
		Application.Quit();
	}

	public void OnSelectLevel()
    {
		titleUI.SetActive(false);
		levelSelectUI.SetActive(true);
    }

	public void OnResume() // doesn't work 
    {
		Debug.Log("Resume Pressed");

		Pause pause = new Pause(); 
		pause.PauseGame(); 
    }

	public void OnOptions()
    {
		optionsUI.SetActive(true);
	}

	public void OnBack()
    {
		if(optionsUI != null) optionsUI.SetActive(false);
		if(levelSelectUI != null) levelSelectUI.SetActive(false);
		if (titleUI != null) titleUI.SetActive(true);
	}
}
