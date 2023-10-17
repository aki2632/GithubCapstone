using UnityEngine;
using Photon.Pun; // PUN 관련 코드

public class PlayerShooterPun : MonoBehaviourPun {
    public GunPun gunPun; // 사용할 총
    public Transform gunPivot; // 총 배치의 기준점
    public Transform leftHandMount; // 총의 왼쪽 손잡이, 왼손이 위치할 지점
    public Transform rightHandMount; // 총의 오른쪽 손잡이, 오른손이 위치할 지점

    private PlayerInputPun playerInputPun; // 플레이어의 입력
    private Animator playerAnimator; // 애니메이터 컴포넌트

    private void Start()
    {
        // 사용할 컴포넌트들을 가져오기
        playerInputPun = GetComponent<PlayerInputPun>();
        playerAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        // 슈터가 활성화될 때 총도 함께 활성화
        gunPun.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        // 슈터가 비활성화될 때 총도 함께 비활성화
        gunPun.gameObject.SetActive(false);
    }

    private void Update()
    {
        // 로컬 플레이어가 아닌경우 입력을 받지 않음
        if (!photonView.IsMine)
        {
            return;
        }

        // 입력을 감지하고 총 발사
        if (playerInputPun.fire)
        {
            // 발사 입력 감지시 총 발사
            gunPun.Fire();
        }
    }

    // 애니메이터의 IK 갱신
    private void OnAnimatorIK(int layerIndex)
    {
        // 총의 기준점 gunPivot을 3D 모델의 오른쪽 팔꿈치 위치로 이동
        gunPivot.position =
            playerAnimator.GetIKHintPosition(AvatarIKHint.RightElbow);

        // IK를 사용하여 왼손의 위치와 회전을 총의 오른쪽 손잡이에 맞춘다
        playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);

        playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand,
            leftHandMount.position);
        playerAnimator.SetIKRotation(AvatarIKGoal.LeftHand,
            leftHandMount.rotation);

        // IK를 사용하여 오른손의 위치와 회전을 총의 오른쪽 손잡이에 맞춘다
        playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

        playerAnimator.SetIKPosition(AvatarIKGoal.RightHand,
            rightHandMount.position);
        playerAnimator.SetIKRotation(AvatarIKGoal.RightHand,
            rightHandMount.rotation);
    }
}
