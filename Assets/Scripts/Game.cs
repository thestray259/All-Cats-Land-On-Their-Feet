using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; 

public class Game : Singleton<Game>
{
	public enum State
	{
		TITLE,
		PLAYER_START,
		GAME,
		GAME_OVER,
		GAME_WIN
	}

	public enum GameMode
	{ 
		COLLECT,
		TIME_TRIAL,
		ALT_GRAVITY
	}

	[SerializeField] ScreenFade screenFade;
	[SerializeField] SceneLoader sceneLoader;
	[SerializeField] GameObject gameOverScreen;

	public GameData gameData;
	[SerializeField] TMP_Text percentUI;
	[SerializeField] TMP_Text timerUI;



	public bool timeTrial = false;
	public float timer = 30;

	float stateTimer = 3; 

	public State state = State.TITLE;
	public GameMode gameMode = GameMode.COLLECT;

	public int percentage { set { if (percentUI == null) return; percentUI.text = value.ToString() + "%"; } }

	private void Start()
	{
		gameData.intData["RatCount"] = 4;
		gameData.intData["TreatCount"] = 6;

		//timerUI.enabled = false;

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

		switch (gameMode)
		{
			case GameMode.COLLECT:
				CollectMode();
				break;
			case GameMode.TIME_TRIAL:
				TimeTrialMode();
				break;
			case GameMode.ALT_GRAVITY:
				AltGravityMode();
				break;
			default:
				break;
		}

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
				gameOverScreen.SetActive(true);
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
/*		gameData.intData["Lives"]--; 

		if (gameData.intData["Lives"] == 0)
        {
			OnLoadScene("Title"); 
        }
		else
        {
			OnLoadScene(SceneManager.GetActiveScene().name);
        }*/
    }

	void OnSceneWasLoaded(Scene current, Scene next)
    {
		InitScene(); 
    }

	public void OnRatFound()
    {
		gameData.intData["RatCount"]--;
		CalculatePercentage(); 
	}

	public void OnTreatFound()
    {
		gameData.intData["TreatCount"]--;
		CalculatePercentage(); 
	}

	void CalculatePercentage()
    {
        int origRatCount = 4;
		int origTreatCount = 6; 

		int currRatCount = gameData.intData["RatCount"];
		int currTreatCount = gameData.intData["TreatCount"];
		float ratModifier = 1.5f;

		float numerator = (currRatCount * ratModifier) + currTreatCount;
		float denomerator = (origRatCount * ratModifier) + origTreatCount;
        float percentageCalc = 100 - (numerator / denomerator * 100);

		gameData.intData["Percentage"] = (int)percentageCalc;

		int percentValue = 0;
		gameData.Load("Percentage", ref percentValue);
		percentage = percentValue;

		if (percentageCalc == 100)
        {
			// show game win / change to win state / go to next level 
			//gameOverScreen.SetActive(true);
			state = State.GAME_WIN;
		}
	}

	public void CollectMode()
	{

	}

	public void TimeTrialMode()
	{
		if (timeTrial == true)
		{
			timer -= Time.deltaTime;
			timerUI.enabled = true;
		}

		if (timer <= 0)
		{
			state = State.GAME_OVER;
		}
	}

	public void AltGravityMode()
	{

	}
}
