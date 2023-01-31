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

	public void OnResume() 
    {
		Debug.Log("Resume Pressed");
		Pause.Instance.PauseGame();
    }

	public void OnOptions()
    {
		optionsUI.SetActive(true);
	}

	public void OnBack()
    {
		if(optionsUI != null) optionsUI.SetActive(false);
	}

	public void NormalGameMode()
    {

    }

	public void TimeTrialGameMode()
    {

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
