using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadExtras : MonoBehaviour
{
    Gamepad gamepad; 
    bool pressed = false;
    bool held = false;
    bool released = false; 

    void Start()
    {
        gamepad = GetComponent<Gamepad>(); 
    }

    void Update()
    {
        
    }


}
