using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;

public class WordSpaceCanvasManager : MonoBehaviour
{
    int countdown = 0;
    public GameObject worldSpaceCanvas;

    public GameObject arrow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void canvas_Deactivate()
    {
        worldSpaceCanvas.SetActive(false);
    }
    public void ResetScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void show_arrow()
    {
        arrow.SetActive(true);
    }

  
}
 