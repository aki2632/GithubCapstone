using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer; // Audio Mixer ����
    public Slider musicSlider; // ��� ���� �����̴�
    public Slider soundEffectSlider; // ȿ���� �����̴�

    private float initialMusicVolume = 0.5f; // �ʱ� ��� ���� ����
    private float initialSoundEffectVolume = 0.5f; // �ʱ� ȿ���� ����

    private void Awake()
    {
        // �ʱⰪ�� PlayerPrefs���� �ҷ����� ���� ��� �ʱⰪ ����
        initialMusicVolume = PlayerPrefs.GetFloat("MusicVolume", initialMusicVolume);
        initialSoundEffectVolume = PlayerPrefs.GetFloat("SoundEffectVolume", initialSoundEffectVolume);
    }

    private void Start()
    {
        // �����̴� �ʱⰪ ����
        musicSlider.value = initialMusicVolume;
        soundEffectSlider.value = initialSoundEffectVolume;

        // �����̴� ���� ����� �� �̺�Ʈ ������ ����
        musicSlider.onValueChanged.AddListener(UpdateMusicVolume);
        soundEffectSlider.onValueChanged.AddListener(UpdateSoundEffectVolume);

        // �ʱ� ���� ��� ����
        UpdateMusicVolume(initialMusicVolume);
        UpdateSoundEffectVolume(initialSoundEffectVolume);
    }

    // ��� ���� ���� ������Ʈ �Լ�
    private void UpdateMusicVolume(float volume)
    {
        // Audio Mixer���� ��� ���� ���� ������Ʈ
        audioMixer.SetFloat("MusicVolume", ConvertToDecibel(volume));
        // ����� ������ PlayerPrefs�� ����
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    // ȿ���� ���� ������Ʈ �Լ�
    private void UpdateSoundEffectVolume(float volume)
    {
        // Audio Mixer���� ȿ���� ���� ������Ʈ
        audioMixer.SetFloat("SoundEffectVolume", ConvertToDecibel(volume));
        // ����� ������ PlayerPrefs�� ����
        PlayerPrefs.SetFloat("SoundEffectVolume", volume);
    }

    // ���� ���� ���� ���ú��� ��ȯ
    private float ConvertToDecibel(float volume)
    {
        return Mathf.Log10(Mathf.Max(0.0001f, volume)) * 20;
    }
}