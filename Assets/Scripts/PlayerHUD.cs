using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] TMP_Text scoreUI;
    [SerializeField] TMP_Text livesUI;
	[SerializeField] TMP_Text witchesUI; 
    //[SerializeField] TMP_Text levelUI;
    //[SerializeField] TMP_Text timeUI;
	[SerializeField] GameData gameData;

	public int score { set { if (scoreUI == null) return; scoreUI.text = value.ToString("D2"); } }
	public int lives { set { if (livesUI == null) return; livesUI.text = value.ToString(); } }
	public int witches { set { if (witchesUI == null) return; witchesUI.text = value.ToString(); } }
	//public int level { set { if (levelUI == null) return; levelUI.text = value.ToString(); } }
	//public float time { set { if (timeUI == null) return; timeUI.text = "<mspace=mspace=36>" + value.ToString("0.0") + "</mspace>"; } }

	private void Update()
	{
		int scoreValue = 0;
		gameData.Load("Score", ref scoreValue);
		score = scoreValue;

		int livesValue = 0;
		gameData.Load("Lives", ref livesValue);
		lives = livesValue;

		int witchesValue = 0;
		gameData.Load("WitchesLeft", ref witchesValue);
		witches = witchesValue; 

		//int levelValue = 0;
		//gameData.Load("Level", ref levelValue);
		//level = levelValue;
	}
}
