using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void Show()
    {
        rect.localScale = Vector3.one;
        Time.timeScale = 0f;
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        Time.timeScale = 1f;
    }
}
