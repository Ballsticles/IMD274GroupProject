using UnityEngine;

public class MovementAudio : MonoBehaviour
{
    [Header("SFX")]
    public AudioClip[] footstepSounds;
    public AudioClip[] jumpSounds;
    public AudioClip[] doubleJumpSounds;
    public AudioClip[] swingStartSounds;
    public AudioClip[] swingStopSounds;
    public AudioClip[] ledgeGrabSounds;
    public AudioClip[] landSounds;

    public float footstepInterval = 0.5f;

    private AudioSource source;


    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayLand()
    {
        if (source == null) return;
        if (landSounds.Length == 0) return;
        source.PlayOneShot(landSounds[Random.Range(0, landSounds.Length)]);
    }

    public void PlayJump()
    {
        if (source == null) return;
        if(jumpSounds.Length == 0) return;
        source.PlayOneShot(jumpSounds[Random.Range(0, jumpSounds.Length)]);
    }

    public void PlayDoubleJump()
    {
        if (source == null) return;
        if(doubleJumpSounds.Length == 0) return;
        source.PlayOneShot(doubleJumpSounds[Random.Range(0, doubleJumpSounds.Length)]);
    }

    public void PlayFootstep()
    {
        if (source == null) return;
        if(footstepSounds.Length == 0) return;
        source.PlayOneShot(footstepSounds[Random.Range(0, footstepSounds.Length)]);
    }

    public void PlaySwingStart()
    {
        if (source == null) return;
        if(swingStartSounds.Length == 0) return;
        source.PlayOneShot(swingStartSounds[Random.Range(0, swingStartSounds.Length)]);
    }
    public void PlaySwingStop()
    {
        if (source == null) return;
        if(swingStopSounds.Length == 0) return;
        source.PlayOneShot(swingStopSounds[Random.Range(0, swingStopSounds.Length)]);
    }
    public void PlayLedgeGrab()
    {
        if (source == null) return;
        if(ledgeGrabSounds.Length == 0) return;
        source.PlayOneShot(ledgeGrabSounds[Random.Range(0, ledgeGrabSounds.Length)]);
    }


}
