using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
using Steamworks;

public class ButtonOffline : MonoBehaviour
{
    public void GoOffline()
    {
        if (LocalGameManager.Instance)
        {
            SceneManager.LoadScene("Lobby Scene");
            Destroy(LocalGameManager.Instance.gameObject);
            Destroy(TurnsHandler.Instance.gameObject);
            Destroy(Board.Instance.gameObject);
        }
        else
        {
            
        }
    }
}
