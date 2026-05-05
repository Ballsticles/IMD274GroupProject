using UnityEngine;
using TMPro;



public class FPSCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text counter;

    

    private float pollingTime = 1f;
    private float time;
    private int frameCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        if(counter == null)
        {
            counter = GetComponentInChildren<TMP_Text>();
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (counter != null && counter.isActiveAndEnabled)
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


}

