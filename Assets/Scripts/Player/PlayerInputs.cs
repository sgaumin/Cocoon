using System;
using UnityEngine;

// Retreive all player inputs
public class PlayerInputs : MonoBehaviour
{
    public float CameraHorizontal { get; private set; }
    public float CameraVertical { get; private set; }
    public float MovementHorizontal { get; private set; }
    public float MovementVertical { get; private set; }
    public bool Pause { get; private set; }
    public bool Interactions { get; private set; }

    // Event for game pause
    public event Action OnPause = delegate { };

    private float interactions;

    void Update()
    {
        // Camera Movements
        CameraHorizontal = Input.GetAxis("RightJoystickHorizontal");
        CameraVertical = Input.GetAxis("RightJoystickVertical");

        // Player Movements
        MovementHorizontal = Input.GetAxis("LeftJoystickHorizontal");
        MovementVertical = Input.GetAxis("LeftJoystickVertical");

        // Pause
        Pause = Input.GetButtonDown("Cancel");
        if (Pause)
            OnPause();

        // Interactions
        interactions = Input.GetAxis("LTButton");
        Interactions = IsInteracting(interactions);
    }

    // Retunr a bool instead of a float
    bool IsInteracting(float inputs)
    {
        return interactions != 0f;
    }
}
