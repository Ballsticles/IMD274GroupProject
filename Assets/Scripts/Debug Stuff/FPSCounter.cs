using UnityEngine;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
[InitializeOnLoad]
#endif


public class FPSCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text counter;
    public bool useVsync = true;
    public bool showFPS = true;
    
    public int targetFrameRate = 60;
    public int vsyncInt;
    private float pollingTime = 1f;
    private float time;
    private int frameCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        counter = GetComponentInChildren<TMP_Text>();
        if(!useVsync)
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = targetFrameRate;
        }
        else
        {
            QualitySettings.vSyncCount = 1;
        }
        vsyncInt = QualitySettings.vSyncCount;

    }

    // Update is called once per frame
    void Update()
    {
        if (counter != null)
        {
            UpdateCounter();
        }
    }
    public void UpdateCounter()
    {
        time += Time.deltaTime;
        frameCount++;
        if(time >= pollingTime)
        { 

            int fps = Mathf.RoundToInt(frameCount / time);
            if (counter != null)
            {
                counter.text = fps.ToString();
            }        
        time -= pollingTime;
        frameCount = 0;
        }

    }
    public void UpdateTargetFrameRate()
    {
        if (useVsync)
        {
            QualitySettings.vSyncCount = 1;
            
        }
        else
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = targetFrameRate;
        }
        vsyncInt = QualitySettings.vSyncCount;
    }
        




}

[CustomEditor(typeof(FPSCounter))]
class FPSCounterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Update Target Frame Rate"))
        {
            ((FPSCounter)target).UpdateTargetFrameRate();
        }
    }



}