using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public void ClickSStart()
    {
        Debug.Log("�ε�");
        SceneManager.LoadScene("Single");
    }

    public void ClickMStart()
    {
        Debug.Log("�ε�");
        SceneManager.LoadScene("Lobby");
    }

    public void ClickExit()
    {
        Debug.Log("���� ����");
        Application.Quit();
    }
}