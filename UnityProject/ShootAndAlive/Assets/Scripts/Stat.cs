using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour
{
    public StatData data;
    public int level;

    Text textLevel;

    void Awake()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
    }

    void LateUpdate()
    {
        textLevel.text = "Lv." + level;
    }

    public void OnClick()
    {
        // 해당 스탯의 ID에 따라 동작을 구분
        switch (data.statId)
        {
            case 0: // Attack Damage
                if (level < data.damages.Length)
                {
                    // Gun.cs의 damage 값을 해당 레벨의 damage 값으로 변경
                    Gun gun = FindObjectOfType<Gun>();
                    if (gun != null)
                    {
                        gun.damage = data.damages[level];
                    }
                }
                break;

            case 1: // Attack Speed
                if (level < data.aSpeed.Length)
                {
                    // Gun.cs의 timeBetFire 값을 해당 레벨의 aSpeed 값으로 변경
                    Gun gun = FindObjectOfType<Gun>();
                    if (gun != null)
                    {
                        gun.timeBetFire = data.aSpeed[level];
                    }
                }
                break;

            case 2: // Move Speed
                if (level < data.mSpeed.Length)
                {
                    // PlayerMovement.cs의 moveSpeed 값을 해당 레벨의 mSpeed 값으로 변경
                    PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
                    if (playerMovement != null)
                    {
                        playerMovement.moveSpeed = data.mSpeed[level];
                    }
                }
                break;

            default:
                break;
        }

        level++;

        // 스탯 레벨이 해당 배열의 길이에 도달하면 버튼 비활성화
        if (level >= data.damages.Length && data.statId == 0)
        {
            GetComponent<Button>().interactable = false;
        }
        else if (level >= data.aSpeed.Length && data.statId == 1)
        {
            GetComponent<Button>().interactable = false;
        }
        else if (level >= data.mSpeed.Length && data.statId == 2)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}

