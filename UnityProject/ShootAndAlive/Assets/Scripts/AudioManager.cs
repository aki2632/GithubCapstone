using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer; // Audio Mixer 연결
    public Slider musicSlider; // 배경 음악 슬라이더
    public Slider soundEffectSlider; // 효과음 슬라이더

    private float initialMusicVolume = 0.5f; // 초기 배경 음악 볼륨
    private float initialSoundEffectVolume = 0.5f; // 초기 효과음 볼륨

    private void Awake()
    {
        // 초기값을 PlayerPrefs에서 불러오고 없을 경우 초기값 설정
        initialMusicVolume = PlayerPrefs.GetFloat("MusicVolume", initialMusicVolume);
        initialSoundEffectVolume = PlayerPrefs.GetFloat("SoundEffectVolume", initialSoundEffectVolume);
    }

    private void Start()
    {
        // 슬라이더 초기값 설정
        musicSlider.value = initialMusicVolume;
        soundEffectSlider.value = initialSoundEffectVolume;

        // 슬라이더 값이 변경될 때 이벤트 리스너 설정
        musicSlider.onValueChanged.AddListener(UpdateMusicVolume);
        soundEffectSlider.onValueChanged.AddListener(UpdateSoundEffectVolume);

        // 초기 설정 즉시 적용
        UpdateMusicVolume(initialMusicVolume);
        UpdateSoundEffectVolume(initialSoundEffectVolume);
    }

    // 배경 음악 볼륨 업데이트 함수
    private void UpdateMusicVolume(float volume)
    {
        // Audio Mixer에서 배경 음악 볼륨 업데이트
        audioMixer.SetFloat("MusicVolume", ConvertToDecibel(volume));
        // 변경된 볼륨을 PlayerPrefs에 저장
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    // 효과음 볼륨 업데이트 함수
    private void UpdateSoundEffectVolume(float volume)
    {
        // Audio Mixer에서 효과음 볼륨 업데이트
        audioMixer.SetFloat("SoundEffectVolume", ConvertToDecibel(volume));
        // 변경된 볼륨을 PlayerPrefs에 저장
        PlayerPrefs.SetFloat("SoundEffectVolume", volume);
    }

    // 선형 볼륨 값을 데시벨로 변환
    private float ConvertToDecibel(float volume)
    {
        return Mathf.Log10(Mathf.Max(0.0001f, volume)) * 20;
    }
}