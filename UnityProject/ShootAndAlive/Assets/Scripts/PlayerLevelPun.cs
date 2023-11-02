using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelPun : MonoBehaviour
{
    public GameManagerPun gameManagerPun;

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
                float curExp = gameManagerPun.exp;
                if (gameManagerPun.level < gameManagerPun.nextExp.Length)
                {
                    float maxExp = gameManagerPun.nextExp[gameManagerPun.level];
                    mySlider.value = curExp / maxExp;
                }
                break;
            case InfoType.Level:
                myText.text = string.Format("Lv.{0:F0}", gameManagerPun.level);
                break;
        }
    }
}