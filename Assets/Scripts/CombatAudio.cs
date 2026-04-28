using UnityEngine;

public class CombatAudio : MonoBehaviour
{
    [Header("SFX")]
    [SerializeField] private AudioClip[] hurtSounds;
    [SerializeField] private AudioClip[] healSounds;
    [SerializeField] private AudioClip deathSound;
    private AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayDeathSound()
    {
        audioSource.PlayOneShot(deathSound);
    }
    public void PlayHitSound()
    {
        audioSource.PlayOneShot(hurtSounds[Random.Range(0, hurtSounds.Length)]);
    }

    public void PlayHealSound()
    {
        audioSource.PlayOneShot(healSounds[Random.Range(0, healSounds.Length)]);
    }

    public void PlayAttackSound(AudioClip attackSound)
    {
        audioSource.PlayOneShot(attackSound, .5f);
    }

}
