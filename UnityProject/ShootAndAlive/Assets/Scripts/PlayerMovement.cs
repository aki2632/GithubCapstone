using UnityEngine;

// 플레이어 캐릭터를 사용자 입력에 따라 움직이는 스크립트
public class PlayerMovement : MonoBehaviour {
    public float moveSpeed = 5f; // 움직임의 속도
    public float rotateSpeed = 180f; // 좌우 회전 속도

    private Animator playerAnimator; // 플레이어 캐릭터의 애니메이터
    private PlayerInput playerInput; // 플레이어 입력을 알려주는 컴포넌트
    private Rigidbody playerRigidbody; // 플레이어 캐릭터의 리지드바디

    private void Start() {
        // 사용할 컴포넌트들의 참조를 가져오기
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }

    // FixedUpdate는 물리 갱신 주기에 맞춰 실행됨
    private void FixedUpdate() {
        // 회전 실행
        Rotate();
        // 움직임 실행
        Move();

        // 입력값에 따라 애니메이터의 Move 파라미터 값을 변경
        playerAnimator.SetFloat("Move", playerInput.move);
    }

    // 입력값에 따라 캐릭터를 앞뒤로 움직임
    private void Move() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // 입력값을 기반으로 이동 방향을 계산
        Vector3 inputDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // 이동 방향을 오른쪽으로 45도 회전
        Vector3 rotatedDirection = Quaternion.Euler(0, 45, 0) * inputDirection;

        // 이동 방향이 0이 아닌 경우에만 이동
        if (rotatedDirection != Vector3.zero)
        {
            // 원하는 방향으로 이동
            Vector3 movement = rotatedDirection * moveSpeed * Time.deltaTime;
            playerRigidbody.MovePosition(playerRigidbody.position + movement);
        }
    }

    // 입력값에 따라 캐릭터를 좌우로 회전
    private void Rotate() {
        // 마우스 커서의 스크린 좌표를 월드 좌표로 변환
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));

        // 플레이어 캐릭터를 마우스 커서 방향으로 회전
        transform.LookAt(mousePosition);

        // 수직 회전을 고정 (가로 평면 회전만 허용)
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

}