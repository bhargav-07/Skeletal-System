using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Quiz
{
    public string name;

    public Transform viewPoint;
    public Transform targetPart;
    public Button thisButton;

    public bool completed;

    public string q1;
    public string q1o1;
    public string q1o2;
    public string q1o3;
    public string q1correct;

    public string q2;
    public string q2o1;
    public string q2o2;
    public string q2o3;
    public string q2correct;   

}
