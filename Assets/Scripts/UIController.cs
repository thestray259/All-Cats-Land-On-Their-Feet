using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
	[SerializeField] GameObject optionsUI; 

	public void OnStartScene(string sceneName)
	{
		Game.Instance.OnLoadScene(sceneName);
	}

	public void OnQuit()
	{
		Application.Quit();
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
	}

	public void MenuOn(GameObject menuToTurnOn)
    {
		menuToTurnOn.SetActive(true);
    }

	public void MenuOff(GameObject menuToTurnOff)
    {
		menuToTurnOff.SetActive(false);
    }
}
