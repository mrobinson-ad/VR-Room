using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class KeypadNavigator : MonoBehaviour
{
    public float inputCooldown = 0.3f; // Cooldown time between inputs
    private float lastInputTime = 0f;  // Time of the last joystick input
    public GameObject[] keypadButtons;  // Array to hold the keypad buttons
    private int currentIndex = 0;       // Tracks which button is currently selected

    private float verticalInput, horizontalInput;
    public float inputThreshold = 0.5f;  // To control how much joystick movement is needed to register input

    public XRController controller;
    public GameObject keypadCanvas;
    

    public InputActionAsset inputActions;   // The Input Action Asset holding all action maps


    private InputActionMap leftLocomotionMap;
    private InputActionMap rightLocomotionMap;
    private InputActionMap uiActionMap;

    private bool isUIMode = false;
    private bool isGameOn = false;

    public TextMeshProUGUI passDisplay;
    public string password;

    public UnityEvent onCorrectPassword;

    void Start()
    {
        // Select the first button initially
        EventSystem.current.SetSelectedGameObject(keypadButtons[currentIndex]);

        leftLocomotionMap = inputActions.FindActionMap("XRI LeftHand Locomotion");
        rightLocomotionMap = inputActions.FindActionMap("XRI RightHand Locomotion");
        uiActionMap = inputActions.FindActionMap("XRI UI");

        // Enable the locomotion maps by default (normal movement mode)
        leftLocomotionMap.Enable();
        rightLocomotionMap.Enable();
        uiActionMap.Disable();

        // Deactivate the UI keypad by default
        keypadCanvas.SetActive(false);
    }

    void Update()
    {
        if (isUIMode && isGameOn)
        {
            HandleJoystickInput();
            if (Input.GetButtonDown("Submit"))  
                {
                    passDisplay.text += keypadButtons[currentIndex].name;
                    if (passDisplay.text.Length == 3)
                    {
                        if(passDisplay.text == password)
                        {
                            onCorrectPassword.Invoke();
                        }
                        else
                        {
                            //Wrong logic
                            passDisplay.text = "";
                        }
                    }
                }
        }
    }

    void HandleJoystickInput()
    {
        if (Time.time - lastInputTime > inputCooldown)
        {
            verticalInput = Input.GetAxis("Vertical");
            horizontalInput = Input.GetAxis("Horizontal");

            if (Mathf.Abs(verticalInput) > inputThreshold || Mathf.Abs(horizontalInput) > inputThreshold)
            {
                // Joystick moved left or right
                if (horizontalInput > inputThreshold)
                {
                    MoveRight();
                }
                else if (horizontalInput < -inputThreshold)
                {
                    MoveLeft();
                }

                // Joystick moved up or down
                if (verticalInput > inputThreshold)
                {
                    MoveUp();
                }
                else if (verticalInput < -inputThreshold)
                {
                    MoveDown();
                }

                // Record the time of this input to start the cooldown
                lastInputTime = Time.time;
            }
        }
    }

    void MoveRight()
    {
        if (currentIndex % 3 < 2)  // Ensure you don’t go out of bounds
        {
            UnLightButton(currentIndex);
            currentIndex++;
            SelectButton(currentIndex);
        }
    }

    void MoveLeft()
    {
        if (currentIndex % 3 > 0)  // Ensure you don’t go out of bounds
        {
            UnLightButton(currentIndex);
            currentIndex--;
            SelectButton(currentIndex);
        }
    }

    void MoveUp()
    {
        if (currentIndex - 3 >= 0)  // Ensure you don’t go out of bounds
        {
            UnLightButton(currentIndex);
            currentIndex -= 3;
            SelectButton(currentIndex);
        }
    }

    void MoveDown()
    {
        if (currentIndex + 3 < keypadButtons.Length)  // Ensure you don’t go out of bounds
        {
            UnLightButton(currentIndex);
            currentIndex += 3;
            SelectButton(currentIndex);
        }
    }

    void SelectButton(int index)
    {
        // Set the currently selected button in the EventSystem
        EventSystem.current.SetSelectedGameObject(keypadButtons[index]);
        HighlightButton(index);
    }

    void HighlightButton(int index)
    {
        var image = keypadButtons[index].GetComponent<Image>();
        var text = keypadButtons[index].GetComponentInChildren<TextMeshProUGUI>();
        image.color = Color.white;
        text.color = Color.white;
    }

    void UnLightButton(int index)
    {
        var image = keypadButtons[index].GetComponent<Image>();
        var text = keypadButtons[index].GetComponentInChildren<TextMeshProUGUI>();
        image.color = Color.gray;
        text.color = Color.gray;
    }

    public void ToggleInputMode()
    {
        if (isGameOn)
        {
            isUIMode = !isUIMode;

            if (isUIMode)
            {
                // Disable movement controls, enable UI navigation
                leftLocomotionMap.Disable();
                rightLocomotionMap.Disable();
                uiActionMap.Enable();
            }
            else
            {
                // Disable UI navigation, enable movement controls
                uiActionMap.Disable();
                leftLocomotionMap.Enable();
                rightLocomotionMap.Enable();
            }
        }
    }

    public void ToggleGame()
    {
        isGameOn = !isGameOn;
        keypadCanvas.SetActive(isGameOn);
    }
}