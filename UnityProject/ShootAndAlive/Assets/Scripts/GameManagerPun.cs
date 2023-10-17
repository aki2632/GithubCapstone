using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;


public class GameManagerPun : MonoBehaviourPunCallbacks, IPunObservable {
    public static GameManagerPun instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<GameManagerPun>();
            }
            return m_instance;
        }
    }

    private static GameManagerPun m_instance;

    public GameObject playerPrefab; // 생성할 플레이어 캐릭터 프리팹

    private int score = 0;
    public bool isGameover { get; private set; }
    public static bool isPause = false;
    public static bool isOption = false;

    // 경험치와 레벨 관련 변수 추가
    public int level=0;
    public int exp=0;
    public int[] nextExp = { 10, 30, 50, 70, 90, 110, 130, 150, 170, 190, 210, 230, 250, 270 }; // 다음 레벨에 필요한 경험치
    public LevelUp uiLevelUp;

    // 주기적으로 자동 실행되는, 동기화 메서드
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // 로컬 오브젝트라면 쓰기 부분이 실행됨
        if (stream.IsWriting)
        {
            // 네트워크를 통해 score 값을 보내기
            stream.SendNext(score);
        }
        else
        {
            // 리모트 오브젝트라면 읽기 부분이 실행됨         

            // 네트워크를 통해 score 값 받기
            score = (int)stream.ReceiveNext();
            // 동기화하여 받은 점수를 UI로 표시
            UIManager.instance.UpdateScoreText(score);
        }
    }

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // 게임 시작과 동시에 플레이어가 될 게임 오브젝트를 생성
    private void Start()
    {
        // 생성할 랜덤 위치 지정
        Vector3 randomSpawnPos = Random.insideUnitSphere * 5f;
        // 위치 y값은 0으로 변경
        randomSpawnPos.y = 0f;

        // 네트워크 상의 모든 클라이언트들에서 생성 실행
        // 단, 해당 게임 오브젝트의 주도권은, 생성 메서드를 직접 실행한 클라이언트에게 있음
        PhotonNetwork.Instantiate(playerPrefab.name, randomSpawnPos, Quaternion.identity);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PhotonNetwork.LeaveRoom();
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

    // 룸을 나갈때 자동 실행되는 메서드
    public override void OnLeftRoom()
    {
        // 룸을 나가면 로비 씬으로 돌아감
        SceneManager.LoadScene("Lobby");
    }
}
