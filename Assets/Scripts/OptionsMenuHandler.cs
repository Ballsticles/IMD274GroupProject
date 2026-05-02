using System;
using UnityEngine;
using UnityEngine.UI;

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
        QualitySettings.vSyncCount = Convert.ToInt32(vsync);
        fpsChange.interactable = !vsync;
    }

    public void SetFPSLimit(int fpsIndex)
    {
        float fpsLimit = 1f;
        switch (fpsIndex) 
        {
            case 0:
                fpsLimit = 30; break;
            case 1:
                fpsLimit = 60; break;
            case 2:
                fpsLimit = 120; break;

        }

        Application.targetFrameRate = (int)fpsLimit;

    }





}
