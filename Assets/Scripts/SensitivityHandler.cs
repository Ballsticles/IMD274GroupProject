using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class SensitivityHandler : MonoBehaviour
{
    float mouseSensitivityX;
    float mouseSensitivityY;
    float conSensitivityX;
    float conSensitivityY;
    [SerializeField]private PlayerInput playerInput;
    private CinemachineInputAxisController axisController;
    private void OnEnable()
    {
        OptionsMenuHandler.sensChanged += GetValuesFromSettings;
    }
    private void OnDisable()
    {
        OptionsMenuHandler.sensChanged -= GetValuesFromSettings;
    }
    private void Awake()
    {
        axisController = GetComponent<CinemachineInputAxisController>();
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        playerInput = Player.GetComponent<PlayerInput>();
        GetValuesFromSettings();
    }

    public void GetValuesFromSettings()
    {
        mouseSensitivityX = SettingsData.mouseSensitivityX;
        mouseSensitivityY = SettingsData.mouseSensitivityY;
        conSensitivityX = SettingsData.controllerSensitivityX;
        conSensitivityY = SettingsData.controllerSensitivityY;
    }


    private void Update()
    {
        if(axisController != null)
        {
            foreach(var controller in axisController.Controllers)
            {
                if(controller.Name == "Look Orbit X")
                {
                    float newGainX;
                    if (playerInput.currentControlScheme == "Gamepad")
                    {
                        newGainX = conSensitivityX;
                        
                    }
                    else
                    {
                        newGainX = mouseSensitivityX;
                    }

                    controller.Input.Gain = newGainX;
                    
                }

                else if(controller.Name == "Look Orbit Y")
                {
                    float newGainY;
                    if(playerInput.currentControlScheme == "Gamepad")
                    {
                        newGainY = -conSensitivityY;
                    }
                    else
                    {
                        newGainY = -mouseSensitivityY;
                    }
                    controller.Input.Gain = newGainY;
                    
                }
               

            }





        }




    }




}
