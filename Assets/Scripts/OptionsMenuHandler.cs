using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using Unity.VisualScripting;

public class OptionsMenuHandler : MonoBehaviour
{
    [Header("Menu Panels")]
    public GameObject generalMenu;
    public GameObject audioMenu;
    public GameObject fpsCounter;

    [Header("Buttons")]
    public Selectable generalButton;
    public Selectable audioButton;
    public Selectable firstAudioSelect;
    public Selectable firstGeneralSelect;

    public Selectable fpsChange;
    [Header("General Interactables")]

    public Toggle fpsToggle;
    public Toggle vsyncToggle;
    public TMP_Dropdown framesDropdown;
    public Slider mouseX;
    public Slider mouseY;
    public Slider controllerX;
    public Slider controllerY;
    public TMP_InputField mouseXInput;
    public TMP_InputField mouseYInput;
    public TMP_InputField controllerXInput;
    public TMP_InputField controllerYInput;


    //settings variables
    private float fpsLimit;
    private bool useVSync;
    private bool showFPS;
    private int fpsIndex;
    private float mouseSensitivityX;
    private float mouseSensitivityY;
    private float controllerSensitivityX;
    private float controllerSensitivityY;

    private void Start()
    {
        GetValuesFromSettings();
        SetSelectableValues();
    }
    private void GetValuesFromSettings()
    {
        useVSync = SettingsData.UseVSync;
        showFPS = SettingsData.ShowFPSCounter;
        fpsIndex = SettingsData.FrameLimit;
        mouseSensitivityX = SettingsData.mouseSensitivityX;
        mouseSensitivityY = SettingsData.mouseSensitivityY;
        controllerSensitivityX = SettingsData.controllerSensitivityX;
        controllerSensitivityY = SettingsData.controllerSensitivityY;
        ChangeVsync(useVSync);
        ShowFPS(showFPS);
        SetFPSLimit(fpsIndex);
        
    }
    private void SetSelectableValues()
    {
        fpsToggle.isOn = showFPS;
        vsyncToggle.isOn = useVSync;
        framesDropdown.value = fpsIndex;
        mouseX.value = mouseSensitivityX;
        mouseY.value = mouseSensitivityY;
        controllerX.value = controllerSensitivityX;
        controllerY.value = controllerSensitivityY;
        mouseXInput.text = mouseSensitivityX.ToString();
        mouseYInput.text = mouseSensitivityY.ToString();
        controllerXInput.text = controllerSensitivityX.ToString();
        controllerYInput.text = controllerSensitivityY.ToString();
    }


    public void ShowGeneralMenu()
    {
        generalMenu.SetActive(true);
        audioMenu.SetActive(false);
        generalButton.interactable = false;
        audioButton.interactable = false;
    }
    
    public void ShowAudioMenu()
    {
        generalMenu.SetActive(false);
        audioMenu.SetActive(true);
        generalButton.interactable = true;
        audioButton.interactable = false;
    }

    public void ShowFPS(bool show)
    {
        SettingsData.ShowFPSCounter = show;
        if (fpsCounter != null)
        {
            fpsCounter.gameObject.SetActive(show);
        }
    }
    public void ChangeQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void ChangeVsync(bool vsync)
    {
        SettingsData.UseVSync = vsync;
        QualitySettings.vSyncCount = Convert.ToInt32(vsync);
        fpsChange.interactable = !vsync;
    }

    public void SetFPSLimit(int fpsIndex)
    {
        switch (fpsIndex) 
        {
            case 0:
                fpsLimit = 30; break;
            case 1:
                fpsLimit = 60; break;
            case 2:
                fpsLimit = 120; break;

        }
        SettingsData.FrameLimit = fpsIndex;
        Application.targetFrameRate = (int)fpsLimit;
    }

    public void SetConXSens(float sensitivity)
    {
        sensitivity = (float)Math.Round(sensitivity, 2);
        SettingsData.controllerSensitivityX = sensitivity;
        controllerSensitivityX = sensitivity;
        controllerX.value = sensitivity;
        controllerXInput.text = sensitivity.ToString();
    }
    
    public void StringMousesX(string input)
    {
        StringToFloatSens(input, 0);
    }
    
    public void StringMouseY(string input)
    {
        StringToFloatSens(input, 1);
    }
    
    public void StringControllerX(string input)
    {
        StringToFloatSens(input, 2);
    }
    
    public void StringControllerY(string input)
    {
        StringToFloatSens(input, 3);
    }

    public void StringToFloatSens(string input, int type)
    {
        if(float.TryParse(input, out float value))
        {
            switch (type)
            {
                case 0: SetMouseXSens(value); break;
                case 1: SetMouseYSens(value); break;
                case 2: SetConXSens(value); break;
                case 3: SetConYSens(value); break;
            }
        }

    }

    
    public void SetConYSens(float sensitivity)
    {
        sensitivity = (float)Math.Round(sensitivity, 2);
        SettingsData.controllerSensitivityY = sensitivity;
        controllerSensitivityY = sensitivity;
        controllerY.value = sensitivity;
        controllerYInput.text = sensitivity.ToString();
    }
    public void SetMouseXSens(float sensitivity)
    {
        sensitivity = (float)Math.Round(sensitivity, 2);
        SettingsData.mouseSensitivityX = sensitivity;
        mouseSensitivityX = sensitivity;
        mouseX.value = sensitivity;
        mouseXInput.text = sensitivity.ToString();
    }
    public void SetMouseYSens(float sensitivity)
    {
        sensitivity = (float)Math.Round(sensitivity, 2);
        SettingsData.mouseSensitivityY = sensitivity;
        mouseSensitivityY = sensitivity;
        mouseY.value = sensitivity;
        mouseYInput.text = sensitivity.ToString();
    }



}
