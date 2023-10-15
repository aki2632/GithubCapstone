using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : MonoBehaviour
{
    public GameManager gameManager;

    public enum InfoType { Exp, Level }
    public InfoType type;

    Text myText;
    Slider mySlider;

    void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Exp:
                float curExp = gameManager.exp;
                if (gameManager.level < gameManager.nextExp.Length)
                {
                    float maxExp = gameManager.nextExp[gameManager.level];
                    mySlider.value = curExp / maxExp;
                }
                break;
            case InfoType.Level:
                myText.text = string.Format("Lv.{0:F0}", gameManager.level);
                break;
        }
    }
}