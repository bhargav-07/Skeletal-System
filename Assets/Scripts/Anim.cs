using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour
{
    public Animator doorAnim;

    public void DoorTrans(bool status)
    {
        doorAnim.SetBool("IsContract", status);
    }
}
