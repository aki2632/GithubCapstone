using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public void ClickSStart()
    {
        Debug.Log("로딩");
        SceneManager.LoadScene("Single");
    }

    public void ClickMStart()
    {
        Debug.Log("로딩");
        SceneManager.LoadScene("Lobby");
    }

    public void ClickExit()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }
}