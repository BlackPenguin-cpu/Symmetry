using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Runtime.InteropServices;

public class Foundation : MonoBehaviour
{
    public static Foundation Inst { get; private set; }
    void Awake() => Inst = this;

    [Header("제단")]
    public float Speed; // 마법진 돌아가는 속도
    public SpriteRenderer Magic_Circle; // 마법진

    [Header("업그레이드 버튼")]
    public Image F_Button; // 상호작용 버튼
    public GameObject Upgrade; // 상호작용 오브젝트
    public Text Upgrade_Text; // 상호작용 텍스트
    public bool Collision_Check = true; // 충돌 했는지 체크

    [Header("마력강화 창")]
    private float timer; // 창 열리는 속도
    public GameObject Pole_01; // 봉_01
    public GameObject Pole_02; // 봉_02
    public GameObject Malyeog_Window; // 창 오브젝트
    public RectTransform MalyeogRect_Window; // 창2
    bool WindowOpen_Check = false;

    public Image FadeInout;

    public Text Title; // 마력 이름
    public Text Explanation; // 마력 설명
    public GameObject Close_Btn; // 닫기 버튼
    public GameObject Price_obj; // 가격 오브젝트
    public Text Dimensional_Price; // 마력 가격

    public int Magic_Open;
    public int Body_Open;

    void Start()
    {
        Upgrade_Text.DOFade(0f, 0f);
        F_Button.DOFade(0f, 0f);


    }

    void Update()
    {
        Foundation_Click();
        // 나였으면 이거 함수만들어서 했음
        if (SceneNameEquals("Dimension"))
            MagicCircle_Rotation();

        #region 월드 좌표를 스크린 좌표로 변경을 해준다.
        if (SceneNameEquals("Dimension"))
            UpgradeTransformChange(new Vector3(-5.2f, -4.4f, 0));
        if (SceneNameEquals("Main"))
            UpgradeTransformChange(new Vector3(-10.8f, -7f, 0));
        #endregion
    }
    private void UpgradeTransformChange(Vector3 vec) => Upgrade.transform.localPosition = Camera.main.WorldToScreenPoint(gameObject.transform.localPosition + vec);

    private bool SceneNameEquals(string name) => SceneManager.GetActiveScene().name.Equals(name);
    public void MagicCircle_Rotation()
    {
        if (SceneManager.GetActiveScene().name.Equals("Main"))
            Magic_Circle.DOFade(1f, 1f);

        Magic_Circle.transform.Rotate(new Vector3(0, 0, Speed * Time.deltaTime));
    }

    public void Foundation_Click()
    {
        if (Input.GetKeyDown(KeyCode.F) && Collision_Check == false && WindowOpen_Check == false)
        {
            FadeInout.DOFade(0.5f, 1f);
            UI_Manager.instance.PlayerMove_control = true;
            StartCoroutine(Open_Window());
            WindowOpen_Check = true;
        }
    }

    #region 창 연출
    public void Close() => StartCoroutine(Close_Window());

    public IEnumerator Open_Window()
    {
        SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX);
        UI_Manager.instance.Cursor_Fade = true;
        Malyeog_Window.SetActive(true);
        timer = 0f;
        Pole_01.transform.DOLocalMoveY(452, 0.5f);
        Pole_02.transform.DOLocalMoveY(-452, 0.5f);

        while (timer < 1)
        {
            MalyeogRect_Window.sizeDelta = new Vector2(1696.425f, Mathf.Lerp(0, 931.6482f, timer));
            timer += Time.deltaTime * 3f;
            yield return null;
        }
    }

    public IEnumerator Close_Window()
    {
        SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX);
        UI_Manager.instance.Cursor_Fade = false;
        timer = 0f;
        Pole_01.transform.DOLocalMoveY(30, 0.5f);
        Pole_02.transform.DOLocalMoveY(-30, 0.5f);
        FadeInout.DOFade(0f, 1f);
        while (timer < 1)
        {
            MalyeogRect_Window.sizeDelta = new Vector2(1696.425f, Mathf.Lerp(931.6482f, 0, timer));
            timer += Time.deltaTime * 3f;
            yield return null;
        }
        UI_Manager.instance.PlayerMove_control = false;
        Malyeog_Window.SetActive(false);
        WindowOpen_Check = false;
    }
    #endregion

    #region 충돌 체크
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.GetComponent<ITypePlayer>() != null)
        {
            Collision_Check = false;
            Upgrade_Text.DOFade(1f, 0.5f);
            F_Button.DOFade(1f, 0.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.GetComponent<ITypePlayer>() != null)
        {
            Collision_Check = true;
            Upgrade_Text.DOFade(0f, 0.5f);
            F_Button.DOFade(0f, 0.5f);
        }
    }
    #endregion
}
