using UnityEngine;

public class WalkAnimation : MonoBehaviour
{
    private Animator m_animator;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_animator.SetFloat("Forward", 1.0f);
    }
}
