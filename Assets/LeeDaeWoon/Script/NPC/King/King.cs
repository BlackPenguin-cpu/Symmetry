using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class King : MonoBehaviour
{
    public static King instance { get; private set; }
    void Awake() => instance = this;

    [Header("왕의 사념")]
    public SpriteRenderer kingNPC;

    [Header("대화 시작 시 필요없는 오브젝트")]
    [SerializeField] GameObject playerHP;
    [SerializeField] GameObject playerSkill;

    [Header("대화 시작 시 필요한 오브젝트")]
    [SerializeField] GameObject creditBar;
    public GameObject cameraObj;
    public Image fBtn;
    public bool isDialogueExit = false;

    const float zoomTime = 0.6f;
    const int creditExpansionPos = 479;
    const int creditShrinkingPos = 680;

    [Header("대화")]
    public List<string> Dialogue = new List<string>();
    public Text dialogueText;
    public int sequenceText = 0;

    [Header("충돌 확인")]
    public GameObject area;

    [Header("마법진 생성")]
    public bool isDialogueSkip = false;
    public bool isMagicCreation = false;

    bool isCheck = false;

    void Update()
    {
        KingCheck();
    }

    public void KingCheck()
    {
        if(UIManager.instance.isKingCheck && !isCheck)
        {
            isCheck = true;
            isMagicCreation = true;

            gameObject.SetActive(false);

            for (int i = 0; i < area.transform.childCount; i++)
                area.transform.GetChild(i).GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    #region 카메라 확대 / 축소
    public void Zoom_Expansion() => StartCoroutine(Expansion());
    public void Zoom_Shrinking() => StartCoroutine(Shrinking());

    // 줌 확대 함수
    IEnumerator Expansion()
    {
        UIManager.instance.isPlayerControl = true; // 플레이어의 움직임을 멈춘다.
        Player.Instance.state = PlayerState.Idle; // 플레이어의 움직임을 서있는 상태로 냅둔다.
        cameraObj.GetComponent<CameraManager>().enabled = false; // CameraManager를 꺼둔다.

        // 플레이어 UI는 왼쪽으로 치운다.
        playerHP.transform.DOLocalMoveX(-260, zoomTime).SetEase(Ease.Linear);
        playerSkill.transform.DOLocalMoveX(-260, zoomTime).SetEase(Ease.Linear);

        // 크레딧바는 위와 아래에서 나온다.
        creditBar.transform.GetChild(0).transform.DOLocalMoveY(creditExpansionPos, zoomTime).SetEase(Ease.Linear);
        creditBar.transform.GetChild(1).transform.DOLocalMoveY(-creditExpansionPos, zoomTime).SetEase(Ease.Linear);

        for (float i = 5; i > 4f; i -= 0.1f)
        {
            Camera.main.orthographicSize = i;
            yield return new WaitForSeconds(0.05f);
        }
    }

    // 줌 축소 함수
    IEnumerator Shrinking()
    {
        // 플레이어 UI는 제자리로 돌려준다.
        playerHP.transform.DOLocalMoveX(-6, zoomTime).SetEase(Ease.Linear);
        playerSkill.transform.DOLocalMoveX(0, zoomTime).SetEase(Ease.Linear);

        // 크레딧바는 위와 아래로 돌려준다.
        creditBar.transform.GetChild(0).transform.DOLocalMoveY(creditShrinkingPos, zoomTime).SetEase(Ease.Linear);
        creditBar.transform.GetChild(1).transform.DOLocalMoveY(-creditShrinkingPos, zoomTime).SetEase(Ease.Linear);

        for (float i = 4; i < 5f; i += 0.1f)
        {
            Camera.main.orthographicSize = i;
            yield return new WaitForSeconds(0.05f);
        }

        cameraObj.GetComponent<CameraManager>().enabled = true; // CameraManager를 켜둔다.
        yield return new WaitForSeconds(0.5f);
        UIManager.instance.isPlayerControl = false; // 플레이어의 움직임을 정상작동 시킨다.
    }
    #endregion
}
