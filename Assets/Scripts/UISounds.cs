using UnityEngine;

public class UISounds : MonoBehaviour
{
    [Header("Sounds")]
    [SerializeField] private AudioClip buttonClickSound;
    [SerializeField] private AudioClip loadLevelSound;
    [SerializeField] private AudioClip backSound;
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioSource audioSource;


    private void OnEnable()
    {
        UIHighlightIsSelect.OnSelectEvent += HoverSound;
    }
    private void OnDisable()
    {
        UIHighlightIsSelect.OnSelectEvent -= HoverSound;
    }

    public void HoverSound()
    {
        audioSource.PlayOneShot(hoverSound);
    }
    public void ButtonClickSound()
    {
        audioSource.PlayOneShot(buttonClickSound);
    }
    public void LoadLevelSound()
    {
        audioSource.PlayOneShot(loadLevelSound);
    }
    public void BackSound()
    {
        audioSource.PlayOneShot(backSound);
    }

}

