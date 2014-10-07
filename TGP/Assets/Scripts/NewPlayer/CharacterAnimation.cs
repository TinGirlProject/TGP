using UnityEngine;
using System.Collections;

public class CharacterAnimation : MonoBehaviour 
{
    private Animator m_animator;
    
    void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    void SetSpeed(float amount)
    {
        m_animator.SetFloat("Speed", amount);
    }
    
    void Jumped()
    {
        m_animator.SetBool("Jumping", true);
    }

}