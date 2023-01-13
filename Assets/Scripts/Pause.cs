using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class Pause : Singleton<Pause>
{
    [SerializeField] GameObject pauseUI;

    bool isPaused = false;

    public bool paused
    {
        get { return isPaused; }
        set
        {
            isPaused = value;
            pauseUI.SetActive(isPaused);
            Time.timeScale = (isPaused) ? 0 : 1;
        }
    }

    public void PauseGame()
    {
        paused = !paused; 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Gamepad.current.startButton.wasPressedThisFrame) // if no gamepad is connected, throws null reference on title screen 
        {
            PauseGame(); 
        }
    }
}