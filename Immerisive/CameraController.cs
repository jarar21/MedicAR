using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;


public class CameraController : MonoBehaviour
{

    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnFocus()
    {
        animator.SetBool("focus", true);
    }

    public void OnUnfocus()
    {
        animator.SetBool("focus", false);
    }

}
