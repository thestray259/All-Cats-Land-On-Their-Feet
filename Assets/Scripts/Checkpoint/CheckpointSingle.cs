using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSingle : MonoBehaviour
{
	//git test
	private CourseCheckpoints courseCheckpoints;
	private MeshRenderer meshRenderer;
	[SerializeField] AudioSource audioSource;

	private void Awake()
	{
		meshRenderer = GetComponent<MeshRenderer>();
	}

	private void Start()
	{
		Hide();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent<PlayerController>(out PlayerController player))
		{
			courseCheckpoints.PlayerThroughCheckpoint(this);
		}
	}

	public void SetCourseCheckpoints(CourseCheckpoints courseCheckpoints)
	{
		this.courseCheckpoints = courseCheckpoints;
	}

	public void Show()
	{
		meshRenderer.enabled = true;
	}

	public void Hide()
	{
		meshRenderer.enabled = false;
		
	}
	public void PlayAudio()
	{
		audioSource.Play();
	}

}
