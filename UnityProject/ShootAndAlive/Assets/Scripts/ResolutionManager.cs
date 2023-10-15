using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionManager : MonoBehaviour
{
    FullScreenMode screenMode;
    public Dropdown resolutionDropdown;
    public Toggle fullscreenBtn;
    public Toggle fullscreenToggle; // �߰�: Ǯ��ũ�� ���
    public List<Resolution> resolutions = new List<Resolution>();
    public int resolutionNum;

    private void Start()
    {
        InitUI();
    }

    void InitUI()
    {
        foreach (Resolution resolution in Screen.resolutions)
        {
            if (resolution.refreshRate == 60)
                resolutions.Add(resolution);
        }

        resolutionDropdown.options.Clear();

        int optionNum = 0;

        for (int i = 0; i < resolutions.Count; i++)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = resolutions[i].width + "x" + resolutions[i].height + " " + resolutions[i].refreshRate + "hz";
            resolutionDropdown.options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
                resolutionDropdown.value = optionNum;
            optionNum++;
        }
        resolutionDropdown.RefreshShownValue();

        fullscreenBtn.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
    }



    public void DropdownOptionChange(int x) // ����: �Լ��� ����
    {
        resolutionNum = x;
    }

    public void FullScreenBtn(bool isFull)
    {
        screenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }


    public void ClickSave()
    {
        GameManager.isOption = false;

        Debug.Log("����");
        UIManager.instance.SetActiveOptionUI(false);
        UIManager.instance.SetActivePauseUI(true);

        Resolution selectedResolution = resolutions[resolutionNum];

        // ������ �ػ󵵿� Ǯ��ũ�� ��� ������ ����
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, screenMode);
    }

}