using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리자 관련 코드
using UnityEngine.UI; // UI 관련 코드 UI 매니저

public class UIManager : MonoBehaviour {
    // 싱글톤 접근용 프로퍼티
    public static UIManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<UIManager>();
            }

            return m_instance;
        }
    }

    private static UIManager m_instance; // 싱글톤이 할당될 변수

    public Text scoreText; // 점수 표시용 텍스트
    public Text waveText; // 적 웨이브 표시용 텍스트

    public GameObject gameoverUI; // 게임 오버시 활성화할 UI
    public GameObject pauseUI; // 일시 정지시 활성화할 UI
    public GameObject optionUI; // 환경 설정시 활성화할 UI

    // 점수 텍스트 갱신
    public void UpdateScoreText(int newScore) {
        scoreText.text = "Score : " + newScore;
    }

    // 적 웨이브 텍스트 갱신
    public void UpdateWaveText(int waves, int count) {
        waveText.text = "Wave : " + waves + "\nEnemy Left : " + count;
    }

    // 게임 오버 UI 활성화
    public void SetActiveGameoverUI(bool active) {
        gameoverUI.SetActive(active);
    }

    // 일시 정지 UI 활성화
    public void SetActivePauseUI(bool active) {
        pauseUI.SetActive(active);
    }

    // 환경 설정 UI 활성화
    public void SetActiveOptionUI(bool active)
    {
        optionUI.SetActive(active);
    }

    // 게임 재시작
    public void GameRestart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // 시간을 다시 진행
        Time.timeScale = 1f;
    }

    // 타이틀 씬 호출
    public void ClickMenu() {
        Debug.Log("로딩");
        SceneManager.LoadScene("Title");

        // 시간을 다시 진행
        Time.timeScale = 1f;
    }

    // 방 떠나기
    public void LeaveRoom()
    {
        GameManagerPun.instance.OnLeftRoom();
    }

}