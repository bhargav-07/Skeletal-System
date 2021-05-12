using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Machine
{
    public string name;
    public GameObject machinePart;
    public Transform viewPoint;

    public AudioClip audioClip;

    [TextArea]
    public string text;

    [HideInInspector]
    public AudioSource source;
}
