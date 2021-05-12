using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachineManager : MonoBehaviour
{
    [SerializeField]
    GameObject machine;

    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    string explodeParameterName;
      
    [SerializeField]
    Text descriptionText;

    [SerializeField]
    GameObject resetButton;

    [SerializeField]
    GameObject textPanel;

    float transitionSpeed = 10f;

    private AudioSource[] allAudioSources;
    public Machine[] machines;

    private Machine m;
    private Transform currentView;

    bool reset;
    Vector3 posReset = new Vector3(0, 0, 0);
    Quaternion rotReset = Quaternion.Euler(0, 0, 0);

    void Awake()
    {
        foreach (Machine m in machines)
        {
            m.machinePart = m.machinePart;
            m.viewPoint = m.viewPoint;
            m.source = gameObject.AddComponent<AudioSource>();
            m.source.clip = m.audioClip;
            m.source.volume = 1;
            m.source.pitch = 1;
            m.text = m.text;
     
        }
    }
    private void Start()
    {
        resetButton.SetActive(false);
        textPanel.SetActive(false);
    }
    public void Play(string name)
    {
        m = Array.Find(machines, machine => machine.name == name);
        StopAllAudio();
        m.source.Play();
        descriptionText.text = m.text;
        currentView = m.viewPoint;

        reset = true;
        resetButton.SetActive(true);
        textPanel.SetActive(true);
    }

    public void StopAllAudio()
    {
       allAudioSources = FindObjectsOfType<AudioSource>();

        foreach(AudioSource audio in allAudioSources)
        {
            audio.Stop();
        }
    }
    public void ResetAll()
    {
        reset = false;
        resetButton.SetActive(false);
        textPanel.SetActive(false);
    }
    public void AnimationController(bool status)
    {
        machine.GetComponent<Animator>().SetBool(explodeParameterName, status);
    }
    public void MuteAllAudio(bool status)
    {
        if (status)
        {
            AudioListener.volume = 0f;
        }
        else
        {
            AudioListener.volume = 1f;
        }
    }

    void LateUpdate()
    {
        if (currentView)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, currentView.position, Time.deltaTime * transitionSpeed);

            Vector3 currentAngle = new Vector3(
                Mathf.LerpAngle(mainCamera.transform.rotation.eulerAngles.x, currentView.transform.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
                Mathf.LerpAngle(mainCamera.transform.rotation.eulerAngles.y, currentView.transform.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
                Mathf.LerpAngle(mainCamera.transform.rotation.eulerAngles.z, currentView.transform.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed));

            mainCamera.transform.eulerAngles = currentAngle;

            mainCamera.transform.LookAt(m.machinePart.transform);
        }              
        if (reset)
        {
            machine.transform.position = Vector3.Lerp(machine.transform.position, posReset, Time.deltaTime * transitionSpeed);
            machine.transform.rotation = Quaternion.Slerp(machine.transform.rotation, rotReset, Time.deltaTime * transitionSpeed);
        }
                    
    }
}
