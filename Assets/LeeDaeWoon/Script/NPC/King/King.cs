using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class King : MonoBehaviour
{
    public static King Inst { get; private set; }
    void Awake() => Inst = this;

    [Header("왕의 사념")]
    public SpriteRenderer King_NPC;

    [Header("대화 시작 시 필요없는 오브젝트")]
    public GameObject Player_HP;
    public GameObject Player_Skill;

    [Header("대화 시작 시 필요한 오브젝트")]
    public GameObject Camera_obj;
    public GameObject Credit_Bar01;
    public GameObject Credit_Bar02;
    public Image F_Button;
    public bool FBtn_Check;
    public bool Dialogue_Exit;

    [Header("대화")]
    public List<string> Dialogue = new List<string>();
    public Text Dialogue_Text;
    public int Sequence_Text = 0;

    [Header("충돌 확인")]
    public BoxCollider2D Area01_Box;
    public BoxCollider2D Area02_Box;
    public BoxCollider2D Area03_Box;
    public BoxCollider2D Area04_Box;

    [Header("마법진 생성")]
    public bool Magic_Creation = false;

    public bool Dialogue_Skip = false;


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
        UI_Manager.Inst.PlayerMove_control = true; // 플레이어의 움직임을 멈춘다.
        Player.Instance.state = PlayerState.Idle; // 플레이어의 움직임을 서있는 상태로 냅둔다.
        Camera_obj.GetComponent<CameraManager>().enabled = false; // CameraManager를 꺼둔다.

        // 플레이어 UI는 왼쪽으로 치운다.
        Player_HP.transform.DOLocalMoveX(-260f, 0.7f).SetEase(Ease.Linear);
        Player_Skill.transform.DOLocalMoveX(-1221f, 0.7f).SetEase(Ease.Linear);

        // 크레딧바는 위와 아래에서 나온다.
        Credit_Bar01.transform.DOLocalMoveY(479f, 0.6f).SetEase(Ease.Linear);
        Credit_Bar02.transform.DOLocalMoveY(-479f, 0.6f).SetEase(Ease.Linear);

        for (float i = 5; i > 4f; i -= 0.1f)
        {
            Camera.main.orthographicSize = i;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public IEnumerator Shrinking() // 줌 축소
    {
        // 플레이어 UI는 제자리로 돌려준다.
        Player_HP.transform.DOLocalMoveX(-6f, 0.6f).SetEase(Ease.Linear);
        Player_Skill.transform.DOLocalMoveX(-967f, 0.6f).SetEase(Ease.Linear);

        // 크레딧바는 위와 아래로 돌려준다.
        Credit_Bar01.transform.DOLocalMoveY(680f, 0.6f).SetEase(Ease.Linear);
        Credit_Bar02.transform.DOLocalMoveY(-680f, 0.6f).SetEase(Ease.Linear);

        for (float i = 4; i < 5f; i += 0.1f)
        {
            Camera.main.orthographicSize = i;
            yield return new WaitForSeconds(0.05f);
        }
        Camera_obj.GetComponent<CameraManager>().enabled = true; // CameraManager를 켜둔다.
        yield return new WaitForSeconds(0.5f);
        UI_Manager.Inst.PlayerMove_control = false; // 플레이어의 움직임을 정상작동 시킨다.

    }
    #endregion
}
