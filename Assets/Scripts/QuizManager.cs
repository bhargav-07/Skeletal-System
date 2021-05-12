using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

//this script includes the control of PPE Panel and the Quiz section of the application
public class QuizManager : MonoBehaviour
{    
    int optionsIndex;

 

    [HideInInspector]
    public int score;

    bool reset;

    [SerializeField]
    Transform machine;

    [SerializeField]
    GameObject questionPanel;

    [SerializeField]
    Button resetButton;

    [SerializeField]
    Sprite defaultSprite;

    [SerializeField]
    Sprite correctSprite;

    [SerializeField]
    Sprite incorrectSprite;

    [SerializeField]
    Text ScoreText;

    [SerializeField]
    Transform mainCamera, cameraDefaultPosition;

    [SerializeField]
    Text question1;
    string[] q1Options;

    [SerializeField]
    Text question2;
    string[] q2Options;

    string q1CorrectAnswer;
    string q2CorrectAnswer;

    [SerializeField]
    GameObject[] q1OptionsToggle;

    [SerializeField]
    GameObject[] q2OptionsToggle;

    string[] q1OptionTexts;
    string[] q2OptionTexts;

    Transform currentView;

    Vector3 posReset = new Vector3(0, 0, 0);
    Quaternion rotReset = Quaternion.Euler(0, 0, 0);

    public Quiz[] quizs;
    private Quiz q;

    bool lookAtCamera;

    private void Awake()
    {
        foreach (Quiz q in quizs)
        {
            q.name = q.name;
            q.viewPoint = q.viewPoint;
            q.targetPart = q.targetPart;
            q.thisButton = q.thisButton;
            q.completed = false;

            q.q1 = q.q1;
            q.q1o1 = q.q1o1;
            q.q1o2 = q.q1o2;
            q.q1o3 = q.q1o3;
            q.q1correct = q.q1correct;

            q.q2 = q.q2;
            q.q2o1 = q.q2o1;
            q.q2o2 = q.q2o2;
            q.q2o3 = q.q2o3;
            q.q2correct = q.q2correct;            
        }
    }
    void InitialCheckup()
    {
        //GetComponent<TimeCounter>().enabled = true;
        //GetComponent<UIInteractionManager>().count = 21;
        //StartCoroutine(GetComponent<UIInteractionManager>().CloseQuizPanelOnTime());
    }
    //Start of QuestionAndOption Methods

        public void SelectQuiz(string name)
    {
        reset = true;
        questionPanel.SetActive(true);
        q = Array.Find(quizs, quizs => quizs.name == name);

        //InitialCheckup();
        if (q.completed)
        {
            return;
        }
        //question1
        question1.text = q.q1;
        q1Options = new string[3] { q.q1o1, q.q1o2, q.q1o3 };
        q1CorrectAnswer = q.q1correct;

        //question2
        question2.text = q.q2;
        q2Options = new string[3] { q.q2o1, q.q2o2, q.q2o3 };
        q2CorrectAnswer = q.q2correct;

        currentView = q.viewPoint;
        q.completed = true;
        resetButton.interactable = true;
        q.thisButton.interactable = false;

        #region Business Logic
        //q1 business logic

        q1OptionsToggle[0].GetComponentInChildren<Text>().text = q1Options[0];
        q1OptionsToggle[1].GetComponentInChildren<Text>().text = q1Options[1];
        q1OptionsToggle[2].GetComponentInChildren<Text>().text = q1Options[2];
        q1OptionTexts = new string[3] { q1OptionsToggle[0].GetComponentInChildren<Text>().text,
            q1OptionsToggle[1].GetComponentInChildren<Text>().text,
            q1OptionsToggle[2].GetComponentInChildren<Text>().text};


        //q2 business logic

        q2OptionsToggle[0].GetComponentInChildren<Text>().text = q2Options[0];
        q2OptionsToggle[1].GetComponentInChildren<Text>().text = q2Options[1];
        q2OptionsToggle[2].GetComponentInChildren<Text>().text = q2Options[2];
        q2OptionTexts = new string[3] { q2OptionsToggle[0].GetComponentInChildren<Text>().text,
            q2OptionsToggle[1].GetComponentInChildren<Text>().text,
            q2OptionsToggle[2].GetComponentInChildren<Text>().text };

        q1OptionsToggle[0].GetComponent<Toggle>().interactable = true;
        q1OptionsToggle[1].GetComponent<Toggle>().interactable = true;
        q1OptionsToggle[2].GetComponent<Toggle>().interactable = true;
        q2OptionsToggle[0].GetComponent<Toggle>().interactable = true;
        q2OptionsToggle[1].GetComponent<Toggle>().interactable = true;
        q2OptionsToggle[2].GetComponent<Toggle>().interactable = true;

        q1OptionsToggle[0].GetComponentInChildren<UnityEngine.UI.Image>().sprite = defaultSprite;
        q1OptionsToggle[1].GetComponentInChildren<UnityEngine.UI.Image>().sprite = defaultSprite;
        q1OptionsToggle[2].GetComponentInChildren<UnityEngine.UI.Image>().sprite = defaultSprite;
        q2OptionsToggle[0].GetComponentInChildren<UnityEngine.UI.Image>().sprite = defaultSprite;
        q2OptionsToggle[1].GetComponentInChildren<UnityEngine.UI.Image>().sprite = defaultSprite;
        q2OptionsToggle[2].GetComponentInChildren<UnityEngine.UI.Image>().sprite = defaultSprite;

        #endregion
    }

    #region Answer Validation Toggles

    public void Q1_Option1()
    {
        if (q1OptionsToggle[0].GetComponentInChildren<Text>().text == q1CorrectAnswer)
        {
            optionsIndex = Array.IndexOf(q1OptionTexts, q1CorrectAnswer);
            Correct(3, optionsIndex, "Question1");
        }

        else if (q1OptionsToggle[0].GetComponentInChildren<Text>().text != q1CorrectAnswer)
        {
            optionsIndex = Array.IndexOf(q1OptionTexts, (q1OptionsToggle[0].GetComponentInChildren<Text>().text));
            Incorrect(1, optionsIndex, "Question1");
        }

        q1OptionsToggle[0].GetComponent<Toggle>().interactable = false;

    }
    public void Q1_Option2()
    {
        if (q1OptionsToggle[1].GetComponentInChildren<Text>().text == q1CorrectAnswer)
        {


            optionsIndex = Array.IndexOf(q1OptionTexts, q1CorrectAnswer);
            Correct(3, optionsIndex, "Question1");
        }
        else if (q1OptionsToggle[1].GetComponentInChildren<Text>().text != q1CorrectAnswer)
        {
            optionsIndex = Array.IndexOf(q1OptionTexts, (q1OptionsToggle[1].GetComponentInChildren<Text>().text));
            Incorrect(1, optionsIndex, "Question1");
        }

        q1OptionsToggle[1].GetComponent<Toggle>().interactable = false;
    }
    public void Q1_Option3()
    {
        if (q1OptionsToggle[2].GetComponentInChildren<Text>().text == q1CorrectAnswer)
        {
            optionsIndex = Array.IndexOf(q1OptionTexts, q1CorrectAnswer);
            Correct(3, optionsIndex, "Question1");
        }
        else if (q1OptionsToggle[2].GetComponentInChildren<Text>().text != q1CorrectAnswer)
        {
            optionsIndex = Array.IndexOf(q1OptionTexts, (q1OptionsToggle[2].GetComponentInChildren<Text>().text));
            Incorrect(1, optionsIndex, "Question1");
        }

        q1OptionsToggle[2].GetComponent<Toggle>().interactable = false;
    }

    public void Q2_Option1()
    {
        if (q2OptionsToggle[0].GetComponentInChildren<Text>().text == q2CorrectAnswer)
        {
            optionsIndex = Array.IndexOf(q2OptionTexts, q2CorrectAnswer);
            Correct(3, optionsIndex, "Question2");
        }

        else if (q2OptionsToggle[0].GetComponentInChildren<Text>().text != q2CorrectAnswer)
        {
            optionsIndex = Array.IndexOf(q2OptionTexts, (q2OptionsToggle[0].GetComponentInChildren<Text>().text));
            Incorrect(1, optionsIndex, "Question2");
        }

        q2OptionsToggle[0].GetComponent<Toggle>().interactable = false;
    }
    public void Q2_Option2()
    {
        if (q2OptionsToggle[1].GetComponentInChildren<Text>().text == q2CorrectAnswer)
        {
            optionsIndex = Array.IndexOf(q2OptionTexts, q2CorrectAnswer);
            Correct(3, optionsIndex, "Question2");
        }
        else if (q2OptionsToggle[1].GetComponentInChildren<Text>().text != q2CorrectAnswer)
        {
            optionsIndex = Array.IndexOf(q2OptionTexts, (q2OptionsToggle[1].GetComponentInChildren<Text>().text));
            Incorrect(1, optionsIndex, "Question2");
        }

        q2OptionsToggle[1].GetComponent<Toggle>().interactable = false;
    }
    public void Q2_Option3()
    {
        if (q2OptionsToggle[2].GetComponentInChildren<Text>().text == q2CorrectAnswer)
        {
            optionsIndex = Array.IndexOf(q2OptionTexts, q2CorrectAnswer);
            Correct(3, optionsIndex, "Question2");
        }
        else if (q2OptionsToggle[2].GetComponentInChildren<Text>().text != q2CorrectAnswer)
        {
            optionsIndex = Array.IndexOf(q2OptionTexts, (q2OptionsToggle[2].GetComponentInChildren<Text>().text));
            Incorrect(1, optionsIndex, "Question2");
        }

        q2OptionsToggle[2].GetComponent<Toggle>().interactable = false;
    }

    #endregion

    void Correct(int marks, int index, string question)
    {
        if (question == "Question1")
        {
            q1OptionsToggle[index].GetComponentInChildren<Image>().sprite = correctSprite;
        }
        else if (question == "Question2")
        {
            q2OptionsToggle[index].GetComponentInChildren<Image>().sprite = correctSprite;
        }
        score = score + marks;

    }
    void Incorrect(int marks, int index, string question)
    {
        if (question == "Question1")
        {
            //StartCoroutine(ExplainTextAppear(question));
            q1OptionsToggle[index].GetComponentInChildren<Image>().sprite = incorrectSprite;
        }
        if (question == "Question2")
        {
            //StartCoroutine(ExplainTextAppear(question));
            q2OptionsToggle[index].GetComponentInChildren<Image>().sprite = incorrectSprite;
        }
        score = score - marks;
    }

    public void ResetAll()
    {
        currentView = cameraDefaultPosition;
        reset = false;
        lookAtCamera = true;
        questionPanel.SetActive(false);        
        resetButton.interactable = false;
    }

    private void LateUpdate()
    {
        ScoreText.text = "Score: " + score.ToString();       

        if (currentView)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.position, currentView.position, Time.deltaTime * 10);

            Vector3 currentAngle = new Vector3(
                Mathf.LerpAngle(mainCamera.transform.rotation.eulerAngles.x, currentView.transform.rotation.eulerAngles.x, Time.deltaTime * 10),
                Mathf.LerpAngle(mainCamera.transform.rotation.eulerAngles.y, currentView.transform.rotation.eulerAngles.y, Time.deltaTime * 10),
                Mathf.LerpAngle(mainCamera.transform.rotation.eulerAngles.z, currentView.transform.rotation.eulerAngles.z, Time.deltaTime * 10));

            mainCamera.transform.eulerAngles = currentAngle;


            mainCamera.LookAt(machine);                

        }

        if (reset)
        {
            mainCamera.LookAt(q.targetPart);
            machine.position = Vector3.Lerp(machine.position, posReset, Time.deltaTime * 10);
            machine.rotation = Quaternion.Slerp(machine.rotation, rotReset, Time.deltaTime * 10);
        }

    }
}
