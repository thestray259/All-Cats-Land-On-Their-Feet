using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseCheckpointsUI : MonoBehaviour
{
	[SerializeField] private CourseCheckpoints courseCheckpoints;

	private void Start()
	{
		courseCheckpoints.OnPlayerCorrectCheckpoint += CourseCheckpoints_OnPlayerCorrectCheckpoint;
		courseCheckpoints.OnPlayerWrongCheckpoint += CourseCheckpoints_OnPlayerWrongCheckpoint;

		Hide();
	}

	private void CourseCheckpoints_OnPlayerCorrectCheckpoint(object sender, System.EventArgs e)
	{
		Hide();
	}

	private void CourseCheckpoints_OnPlayerWrongCheckpoint(object sender, System.EventArgs e)
	{
		Show();
	}

	private void Show()
	{
		gameObject.SetActive(true);
	}

	private void Hide()
	{
		gameObject.SetActive(false);
	}
}
