using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Chest : MonoBehaviour
{
     Animator animator;
    [SerializeField] GameObject text;


    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    void ChestOpened()
    {
        animator.SetBool("Over",true);
    }

    public void AnimateChest()
    {
        animator.SetBool("Clicked", true);
    }

    public void ShowContent()
    {
        text.SetActive(true);
    }

}
