using UnityEngine;

public static class SettingsData
{
    private static string mouseSensitivity_x = "Mouse Sensitivity X";
    public static float mouseSensitivityX
    {
        get => PlayerPrefs.GetFloat(mouseSensitivity_x, 3.0f);
        set => PlayerPrefs.SetFloat(mouseSensitivity_x, value);
    }
    private static string mouseSensitivity_y = "Mouse Sensitivity Y";
    public static float mouseSensitivityY
    {
        get => PlayerPrefs.GetFloat(mouseSensitivity_y, 3.0f);
        set => PlayerPrefs.SetFloat(mouseSensitivity_y, value);
    }

    private static string controllerSensitivity_x = "Controller Sensitivity X";
    public static float controllerSensitivityX
    {
        get => PlayerPrefs.GetFloat(controllerSensitivity_x, 4.0f);
        set => PlayerPrefs.SetFloat(controllerSensitivity_x, value);
    }
    private static string controllerSensitivity_y = "Controller Sensitivity Y";
    public static float controllerSensitivityY
    {
        get => PlayerPrefs.GetFloat(controllerSensitivity_y, 4.0f);
        set => PlayerPrefs.SetFloat(controllerSensitivity_y, value);

    }
    private static string useVSync = "Use VSync";
    public static bool UseVSync
    {
        get => PlayerPrefs.GetInt(useVSync, 1) == 1;
        set => PlayerPrefs.SetInt(useVSync, value ? 1 : 0);
    }

    private static string showFPSCounter = "Show FPS Counter";
    public static bool ShowFPSCounter
    {
        get => PlayerPrefs.GetInt(showFPSCounter, 0) == 1;
        set => PlayerPrefs.SetInt(showFPSCounter, value ? 1 : 0);
    }

    private static string frameLimit = "Frame Limit";
    public static int FrameLimit
    {
        get => PlayerPrefs.GetInt(frameLimit, 1);
        set => PlayerPrefs.SetInt(frameLimit, value);
    }



}
