using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

/// <summary>
/// Handle the player's inputs
/// 
/// ##Script Description
/// The script contains classes for different input types: BoolInput (i.e. button)
/// and Axis2DInput (i.e. joystick). The script controls everything to do with inputs
/// such as enabling and disabling inputs, setting controls, and updating the states of the inputs
/// 
/// ## Using and checking inputs
/// To check the status of the inputs in any function, call InputHandler.{control_name}.{desired_state_to_check}
/// e.g. to check if a BooleanInput named interactButton has been pressed: InputHandler.interactButton.isDown
/// e.g. to check an Axis2DInput named movementAxis' X value: InputHandler.movementAxis.x
/// </summary>
public class InputHandler : MonoBehaviour
{

    public static OVRPlayerController playerController;
    public static GameObject player;

    public static Vector3 position;
    public static Quaternion rotation;

    private static bool isEnabled = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<OVRPlayerController>();

        position = player.transform.position;
        rotation = player.transform.rotation;
    }

    public static void DisableMovement()
    {
        //teleportFunction.SetActive(false);
        isEnabled = false;
        playerController.EnableLinearMovement = false;
        playerController.EnableRotation = false;
    }

    public static void EnableMovement()
    {
        //teleportFunction.SetActive(true);
        isEnabled = true;
        playerController.EnableLinearMovement = true;
        playerController.EnableRotation = true;
    }

    // Move the player to a different position
    public static void TeleportPlayer(Vector3 position, Quaternion rotation)
    {
        player.transform.position = position;
        player.transform.rotation = rotation;
    }

    public enum ButtonControl
    {
        InteractButton = OVRInput.RawButton.A,
        StaplerButton  = OVRInput.RawAxis1D.RIndexTrigger,
        MenuButton     = OVRInput.RawButton.LThumbstick,
        SelectButton   = OVRInput.RawButton.Y,
    }

    public enum AxisControl
    {
        MoveAxis = OVRInput.RawAxis2D.LThumbstick,
        LookAxis = OVRInput.RawAxis2D.RThumbstick
    }



    public static Vector2 GetAxis(AxisControl? axis)
    {
        if (!isEnabled) { return (new Vector2(0, 0)); }
        if (axis == null) { return (new Vector2(0,0)); }
        return (OVRInput.Get((OVRInput.RawAxis2D)axis));
    }

    public static bool GetButton(ButtonControl? button)
    {
        if (!isEnabled) { return (false); }
        if (button == null) { return(false); }
        return (OVRInput.Get((OVRInput.RawButton)button));
    }

    public static bool GetButtonDown(ButtonControl? button)
    {
        if (button == null) { return (false); }
        return (OVRInput.GetDown((OVRInput.RawButton)button));
    }

    public static bool GetButtonUp(ButtonControl? button)
    {
        if (button == null) { return (false); }
        return (OVRInput.GetUp((OVRInput.RawButton)button));
    }

    // Update is called once per frame
    void Update()
    { 

        position = player.transform.position;
        rotation = player.transform.rotation;
    }
}
