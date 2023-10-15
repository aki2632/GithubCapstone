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
        // �ش� ������ ID�� ���� ������ ����
        switch (data.statId)
        {
            case 0: // Attack Damage
                if (level < data.damages.Length)
                {
                    // Gun.cs�� damage ���� �ش� ������ damage ������ ����
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
                    // Gun.cs�� timeBetFire ���� �ش� ������ aSpeed ������ ����
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
                    // PlayerMovement.cs�� moveSpeed ���� �ش� ������ mSpeed ������ ����
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

        // ���� ������ �ش� �迭�� ���̿� �����ϸ� ��ư ��Ȱ��ȭ
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

