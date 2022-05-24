using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; 

public class Game : Singleton<Game>
{
	enum State
	{
		TITLE,
		PLAYER_START,
		GAME,
		GAME_OVER,
		GAME_WIN
	}

	[SerializeField] ScreenFade screenFade;
	[SerializeField] SceneLoader sceneLoader;
	[SerializeField] GameObject gameOverScreen;

	public GameData gameData;

	float stateTimer = 3; 

	State state = State.TITLE;
	int highScore;

	private void Start()
	{
		highScore = PlayerPrefs.GetInt("highscore", 0);
		highScore++;
		PlayerPrefs.SetInt("highscore", highScore);

		//PlayerPrefs.DeleteAll();
		//PlayerPrefs.DeleteKey("highscore");

		gameData.intData["Lives"] = 3; 
		gameData.intData["WitchesLeft"] = 7;
		gameData.intData["Score"] = 0; 

		InitScene();
		SceneManager.activeSceneChanged += OnSceneWasLoaded; 
	}

	void InitScene()
    {
		SceneDescriptor sceneDescriptor = FindObjectOfType<SceneDescriptor>();
		if (sceneDescriptor != null)
        {
			Instantiate(sceneDescriptor.player, sceneDescriptor.playerSpawn.position, sceneDescriptor.playerSpawn.rotation);
        }
	}

	private void Update()
	{
		stateTimer -= Time.deltaTime;

		switch (state)
		{
			case State.TITLE:
				break;
			case State.PLAYER_START:
				break;
			case State.GAME:
				break;
			case State.GAME_OVER:
				break;
			case State.GAME_WIN:
				break;
			default:
				break;
		}
	}

	public void OnLoadScene(string sceneName)
    {
		gameOverScreen.SetActive(false);
		sceneLoader.Load(sceneName); 
	}

	public void OnPlayerDead()
    {
		gameData.intData["Lives"]--; 

		if (gameData.intData["Lives"] == 0)
        {
			OnLoadScene("Title"); 
        }
		else
        {
			OnLoadScene(SceneManager.GetActiveScene().name);
        }
    }

	public void OnCoinPickup()
    {
		gameData.intData["Score"] += 25; 
    }

	public void OnWitchDead()
    {
		gameData.intData["WitchesLeft"]--;
		gameData.intData["Score"] += 100;

		if (gameData.intData["WitchesLeft"] == 0)
        {
			gameOverScreen.SetActive(true);
			stateTimer -= Time.deltaTime;
			if (stateTimer >= 0)
			{
				gameData.intData["Lives"] = 3;
				gameData.intData["WitchesLeft"] = 7;
				gameData.intData["Score"] = 0;

				OnLoadScene("Title");
			}
		}
    }

	void OnSceneWasLoaded(Scene current, Scene next)
    {
		InitScene(); 
    }
}
