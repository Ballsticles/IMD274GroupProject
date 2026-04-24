using UnityEngine;

[CreateAssetMenu(menuName ="Attacks/Normal Attack")]

public class AttackSO : ScriptableObject
{
    public int damage;
    public AnimatorOverrideController animatorOV;
    public Weapon hurtCollider;
    public AudioClip attackSound;
}
