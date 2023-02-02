using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseCheckpoints : MonoBehaviour
{
	public event EventHandler OnPlayerCorrectCheckpoint;
	public event EventHandler OnPlayerWrongCheckpoint;

	private List<CheckpointSingle> checkpointSingleList;
	private int nextCheckpointSingleIndex;

	

	private void Awake()
	{
		Transform checkpointsTransform = transform.Find("Checkpoints");

		checkpointSingleList = new List<CheckpointSingle>();
		foreach (Transform checkpointsSingleTransform in checkpointsTransform)
		{
			CheckpointSingle checkpointSingle = checkpointsSingleTransform.GetComponent<CheckpointSingle>();
			checkpointSingle.SetCourseCheckpoints(this);
			checkpointSingleList.Add(checkpointSingle);
		}
		nextCheckpointSingleIndex = 0;
	}

	public void PlayerThroughCheckpoint(CheckpointSingle checkpointSingle)
	{
		if (checkpointSingleList.IndexOf(checkpointSingle) == checkpointSingleList.Count - 1)
		{
			//Gameover state
			Game.Instance.state = Game.State.GAME_WIN;
		}

		if (checkpointSingleList.IndexOf(checkpointSingle) == nextCheckpointSingleIndex)
		{
			Debug.Log("Correct");

			CheckpointSingle correctCheckpointSingle = checkpointSingleList[nextCheckpointSingleIndex];
			correctCheckpointSingle.Hide();
			correctCheckpointSingle.PlayAudio();

			//nextCheckpointSingleIndex = (nextCheckpointSingleIndex + 1) % checkpointSingleList.Count;
			nextCheckpointSingleIndex = nextCheckpointSingleIndex + 1;
			OnPlayerCorrectCheckpoint?.Invoke(this, EventArgs.Empty);
		}
		/*else if (checkpointSingleList.IndexOf(checkpointSingle) == checkpointSingleList.Count - 1)
		{
			//Gameover state
			Game.Instance.state = Game.State.GAME_WIN;
		}*/
		else
		{
			if (checkpointSingleList.IndexOf(checkpointSingle) != nextCheckpointSingleIndex - 1)
			{
				Debug.Log("Wrong");
				OnPlayerWrongCheckpoint?.Invoke(this, EventArgs.Empty);

				CheckpointSingle correctCheckpointSingle = checkpointSingleList[nextCheckpointSingleIndex];
				correctCheckpointSingle.Show();
			}
		}
	}
}
