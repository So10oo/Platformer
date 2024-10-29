using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cainos.LucidEditor;

public class Chest : Item
{
    [FoldoutGroup("Reference")]
    public Animator animator;

    [FoldoutGroup("Runtime"), ShowInInspector, DisableInEditMode]
    public bool IsOpened
    {
        get { return isOpened; }
        set
        {
            isOpened = value;
            animator.SetBool("IsOpened", isOpened);
        }
    }
    private bool isOpened;

    [FoldoutGroup("Runtime"),Button("Open"), HorizontalGroup("Runtime/Button")]
    public void Open()
    {
        IsOpened = true;
    }

    [FoldoutGroup("Runtime"), Button("Close"), HorizontalGroup("Runtime/Button")]
    public void Close()
    {
        IsOpened = false;
    }

    public override void UseItem()
    {
        if (isOpened == false) 
        {
            Close();
        }
        else
        {
            return;
        }
    }
}    

