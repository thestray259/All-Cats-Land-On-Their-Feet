using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] TMP_Text ratsUI;
    [SerializeField] TMP_Text treatsUI;
    //[SerializeField] TMP_Text levelUI;
	[SerializeField] GameData gameData;

	public int rats { set { if (ratsUI == null) return; ratsUI.text = value.ToString(); } }
	public int treats { set { if (treatsUI == null) return; treatsUI.text = value.ToString(); } }
	//public int level { set { if (levelUI == null) return; levelUI.text = value.ToString(); } }

	private void Update()
	{
		int ratCount = 0;
		gameData.Load("RatCount", ref ratCount);
		rats = ratCount;

		int treatCount = 0;
		gameData.Load("TreatCount", ref treatCount);
		treats = treatCount;		

		//int levelValue = 0;
		//gameData.Load("Level", ref levelValue);
		//level = levelValue;
	}
}
