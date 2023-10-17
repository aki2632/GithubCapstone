using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
            }
            return m_instance;
        }
    }

    private static GameManager m_instance;

    private int score = 0;
    public bool isGameover { get; private set; }
    public static bool isPause = false;
    public static bool isOption = false;

    // 경험치와 레벨 관련 변수 추가
    public int level=0;
    public int exp=0;
    public int[] nextExp = { 10, 30, 50, 70, 90, 110, 130, 150, 170, 190, 210, 230, 250, 270 }; // 다음 레벨에 필요한 경험치
    public LevelUp uiLevelUp;

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        FindObjectOfType<PlayerHealth>().onDeath += EndGame;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPause)
                PauseGame();
            else
                ResumeGame();
        }
    }

    public void AddScore(int newScore)
    {
        if (!isGameover)
        {
            score += newScore;
            UIManager.instance.UpdateScoreText(score);
        }
    }

    public void GetExp()
    {
        if (!isGameover)
        {
            exp += 10;

            // 경험치가 다음 레벨에 필요한 경험치와 같거나 더 많아지면 레벨 증가
            if (exp >= nextExp[level - 1])
            {
                level++;
                exp = 0;
                uiLevelUp.Show();
            }
        }
    }

    public void EndGame()
    {
        isGameover = true;
        UIManager.instance.SetActiveGameoverUI(true);
    }

    public void PauseGame()
    {
        if (isGameover)
        {
            return;
        }

        isPause = true;
        UIManager.instance.SetActivePauseUI(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        if (isPause && !isOption)
        {
            isPause = false;
            UIManager.instance.SetActivePauseUI(false);
            Time.timeScale = 1f;
        }
    }

    public void ClickOption()
    {
        isOption = true;
        UIManager.instance.SetActiveOptionUI(true);
        UIManager.instance.SetActivePauseUI(false);
    }
}
