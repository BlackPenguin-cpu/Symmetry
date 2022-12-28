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
    public BoxCollider2D area01Box;
    public BoxCollider2D area02Box;
    public BoxCollider2D area03Box;
    public BoxCollider2D area04Box;

    [Header("마법진 생성")]
    public bool isMagicCreation = false;
    public bool isDialogueSkip = false;


    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
            Zoom_Expansion();
        if (Input.GetKeyDown(KeyCode.Keypad2))
            Zoom_Shrinking();

    }

    #region 카메라 확대 / 축소
    public void Zoom_Expansion() => StartCoroutine(Expansion());
    public void Zoom_Shrinking() => StartCoroutine(Shrinking());


    public IEnumerator Expansion() // 줌 확대 
    {
        UI_Manager.instance.PlayerMove_control = true; // 플레이어의 움직임을 멈춘다.
        Player.Instance.state = PlayerState.Idle; // 플레이어의 움직임을 서있는 상태로 냅둔다.
        cameraObj.GetComponent<CameraManager>().enabled = false; // CameraManager를 꺼둔다.

        // 플레이어 UI는 왼쪽으로 치운다.
        playerHP.transform.DOLocalMoveX(-260f, 0.7f).SetEase(Ease.Linear);
        playerSkill.transform.DOLocalMoveX(-1221f, 0.7f).SetEase(Ease.Linear);

        // 크레딧바는 위와 아래에서 나온다.
        creditBar.transform.GetChild(0).transform.DOLocalMoveY(creditExpansionPos, zoomTime).SetEase(Ease.Linear);
        creditBar.transform.GetChild(1).transform.DOLocalMoveY(-creditExpansionPos, zoomTime).SetEase(Ease.Linear);

        for (float i = 5; i > 4f; i -= 0.1f)
        {
            Camera.main.orthographicSize = i;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public IEnumerator Shrinking() // 줌 축소
    {
        // 플레이어 UI는 제자리로 돌려준다.
        playerHP.transform.DOLocalMoveX(-6f, zoomTime).SetEase(Ease.Linear);
        playerSkill.transform.DOLocalMoveX(-967f, zoomTime).SetEase(Ease.Linear);

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
        UI_Manager.instance.PlayerMove_control = false; // 플레이어의 움직임을 정상작동 시킨다.

    }
    #endregion
}
