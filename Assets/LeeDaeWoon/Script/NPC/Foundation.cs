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
    const float speed = -10; // 마법진 돌아가는 속도
    [SerializeField] SpriteRenderer magicCircle; // 마법진

    [Header("업그레이드 버튼")]
    [SerializeField] Image fBtn; // 상호작용 버튼
    [SerializeField] GameObject upGrade; // 상호작용 오브젝트
    [SerializeField] Text upGradeText; // 상호작용 텍스트
    [SerializeField] bool isCollisionCheck = true; // 충돌 했는지 체크
    
    [Header("마력강화 창")]
    float timer; // 창 열리는 속도
    [SerializeField] GameObject pole01; // 봉_01
    [SerializeField] GameObject pole02; // 봉_02
    [SerializeField] GameObject malyeogWindow; // 창 오브젝트
    [SerializeField] RectTransform malyeogRectWindow; // 창2
    bool iswindowOpenCheck = false;

    [SerializeField] Image fadeInOut;

    public Text Title; // 마력 이름
    public Text Explanation; // 마력 설명
    public GameObject Close_Btn; // 닫기 버튼
    public GameObject Price_obj; // 가격 오브젝트
    public Text Dimensional_Price; // 마력 가격

    public int Magic_Open;
    public int Body_Open;

    void Start()
    {
        upGradeText.DOFade(0f, 0f);
        fBtn.DOFade(0f, 0f);
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

    private void UpgradeTransformChange(Vector3 vec) => upGrade.transform.localPosition = Camera.main.WorldToScreenPoint(gameObject.transform.localPosition + vec);

    private bool SceneNameEquals(string name) => SceneManager.GetActiveScene().name.Equals(name);
    public void MagicCircle_Rotation()
    {
        if (SceneManager.GetActiveScene().name.Equals("Main"))
            magicCircle.DOFade(1f, 1f);

        magicCircle.transform.Rotate(new Vector3(0, 0, speed * Time.deltaTime));
    }

    public void Foundation_Click()
    {
        if (Input.GetKeyDown(KeyCode.F) && isCollisionCheck == false && iswindowOpenCheck == false)
        {
            fadeInOut.DOFade(0.5f, 1f);
            UIManager.instance.isPlayerControl = true;
            StartCoroutine(Open_Window());
            iswindowOpenCheck = true;
        }
    }

    #region 창 연출
    public void Close() => StartCoroutine(Close_Window());

    public IEnumerator Open_Window()
    {
        SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX);
        UIManager.instance.isCursorFade = true;
        malyeogWindow.SetActive(true);
        timer = 0f;
        pole01.transform.DOLocalMoveY(452, 0.5f);
        pole02.transform.DOLocalMoveY(-452, 0.5f);

        while (timer < 1)
        {
            malyeogRectWindow.sizeDelta = new Vector2(1696.425f, Mathf.Lerp(0, 931.6482f, timer));
            timer += Time.deltaTime * 3f;
            yield return null;
        }
    }

    public IEnumerator Close_Window()
    {
        SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX);
        UIManager.instance.isCursorFade = false;
        timer = 0f;
        pole01.transform.DOLocalMoveY(30, 0.5f);
        pole02.transform.DOLocalMoveY(-30, 0.5f);
        fadeInOut.DOFade(0f, 1f);
        while (timer < 1)
        {
            malyeogRectWindow.sizeDelta = new Vector2(1696.425f, Mathf.Lerp(931.6482f, 0, timer));
            timer += Time.deltaTime * 3f;
            yield return null;
        }
        UIManager.instance.isPlayerControl = false;
        malyeogWindow.SetActive(false);
        iswindowOpenCheck = false;
    }
    #endregion

    #region 충돌 체크
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.GetComponent<ITypePlayer>() != null)
        {
            isCollisionCheck = false;
            upGradeText.DOFade(1f, 0.5f);
            fBtn.DOFade(1f, 0.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.GetComponent<ITypePlayer>() != null)
        {
            isCollisionCheck = true;
            upGradeText.DOFade(0f, 0.5f);
            fBtn.DOFade(0f, 0.5f);
        }
    }
    #endregion
}
