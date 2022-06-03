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
		Pause pause = gameObject.AddComponent<Pause>();
		pause.paused = false; 
    }

	public void OnOptions()
    {
		optionsUI.SetActive(true);
	}

	public void OnBack()
    {
		optionsUI.SetActive(false); 

		Pause pause = gameObject.AddComponent<Pause>();
		pause.paused = true;
	}
}
