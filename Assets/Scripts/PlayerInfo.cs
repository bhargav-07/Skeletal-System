using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField]
    InputField playerName, playerEmail;

    [SerializeField]
    GameObject playerInfoPanel;

    [SerializeField]
    Text playerNameText, playerStatusText;

    string playerStatus;

    void Start()
    {
        if (PlayerPrefs.GetString("playerName") == "")
        {
            playerInfoPanel.SetActive(true);
        }
        else
        {
            playerNameText.text = PlayerPrefs.GetString("playerName");
            playerInfoPanel.SetActive(false);
        }        
        playerStatus = PlayerPrefs.GetString("status");
        playerStatusText.text = playerStatus;        
    }
    public void SavePlayerInfo()
    {      
        PlayerPrefs.SetString("playerName", playerName.text);
        PlayerPrefs.SetString("playerEmail", playerEmail.text);

        playerNameText.text = PlayerPrefs.GetString("playerName");
        playerInfoPanel.SetActive(false);
    }
}
