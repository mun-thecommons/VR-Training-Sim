using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<OVRPlayerController>();

        position = player.transform.position;
        rotation = player.transform.rotation;
    }

    /**********************************************************
     * Class for handling boolean (button) inputs
     * 
     * Possible parameters to check:
     * isDown: has the button been pressed?
     * isUp  : has the button been released?
     * state : the current state of the button (true=pressed, false=unpressed)
    ***********************************************************/
    public class BoolInput
    {
        private OVRInput.RawButton button = OVRInput.RawButton.None;
        private OVRInput.RawAxis1D axis1d = OVRInput.RawAxis1D.None;

        private float axisDeadzone = 0.03f;

        public bool isDown;
        public bool isUp;
        public bool state;

        public bool isEnabled = true;

        // Constructor for button input
        public BoolInput(OVRInput.RawButton b)
        {
            isDown = false;
            isUp = false;
            state = false;

            button = b;
        }

        // Constructor for float input
        public BoolInput(OVRInput.RawAxis1D a1d)
        {
            isDown = false;
            isUp = false;
            state = false;

            axis1d = a1d;
        }

        // Change button (button)
        public void ChangeButton(OVRInput.RawButton b)
        {
            axis1d = OVRInput.RawAxis1D.None;
            button = b;
        }

        // Change button (axis)
        public void ChangeButton(OVRInput.RawAxis1D a1d)
        {
            axis1d = a1d;
            button = OVRInput.RawButton.None;
        }

        // Enable or disable the input
        public void Enable()
        {
            isEnabled = true;
        }
        public void Disable()
        {
            isEnabled = false;
        }

        // Get all control states
        public void GetStates()
        {
            if (isEnabled)
            {
                if (button != OVRInput.RawButton.None)
                {
                    isDown = OVRInput.GetDown(this.button);
                    isUp = OVRInput.GetUp(this.button);
                    state = OVRInput.Get(this.button);
                }
                else
                {
                    isDown = false;
                    isUp = false;
                    state = OVRInput.Get(this.axis1d) >= axisDeadzone;
                }
            }
            else
            {
                isDown = false;
                isUp = false;
                state = false;
            }
            
        }
    }
    /**********************************************************
     * Class for handling 2D axis (joystick) inputs
     * 
     * Possible parameters to check:
     * x: the x position of the axis
     * y: the y position of the axis
     * 
    ***********************************************************/
    public class Axis2DInput
    {
        private OVRInput.RawAxis2D axis2d = OVRInput.RawAxis2D.None;

        public float x;
        public float y;

        public bool isEnabled = true;

        public Axis2DInput(OVRInput.RawAxis2D a2d)
        {
            x = 0.0f;
            y = 0.0f;
            axis2d = a2d;
        }

        // Enable or disable the input
        public void Enable()
        {
            isEnabled = true;
        }
        public void Disable()
        {
            isEnabled = false;
        }

        // Update the values of the control
        public void GetStates()
        {
            if (isEnabled)
            {
                x = OVRInput.Get(axis2d).x;
                y = OVRInput.Get(axis2d).y;
            }
            else
            {
                x = 0.0f;
                y = 0.0f;
            }
        }

    }

    public static void DisableMovement()
    {
        //teleportFunction.SetActive(false);
        playerController.EnableLinearMovement = false;
        playerController.EnableRotation = false;
    }

    public static void EnableMovement()
    {
        //teleportFunction.SetActive(true);
        playerController.EnableLinearMovement = true;
        playerController.EnableRotation = true;
    }

    // Move the player to a different position
    public static void TeleportPlayer(Vector3 position, Quaternion rotation)
    {
        player.transform.position = position;
        player.transform.rotation = rotation;
    }

    // Definition of all inputs
    public static BoolInput interactButton = new BoolInput(OVRInput.RawButton.A);             
    public static BoolInput staplerButton  = new BoolInput(OVRInput.RawAxis1D.RIndexTrigger);
    public static BoolInput menuButton     = new BoolInput(OVRInput.RawButton.LThumbstick);

    public static BoolInput selectButton   = new BoolInput(OVRInput.RawButton.Y);

    public static Axis2DInput movementAxis = new Axis2DInput(OVRInput.RawAxis2D.LThumbstick);
    public static Axis2DInput lookAxis     = new Axis2DInput(OVRInput.RawAxis2D.RThumbstick);

    // Update is called once per frame
    void Update()
    {
        interactButton.GetStates();
        staplerButton.GetStates();
        menuButton.GetStates();

        selectButton.GetStates();

        movementAxis.GetStates();
        lookAxis.GetStates();

        position = player.transform.position;
        rotation = player.transform.rotation;
    }
}
