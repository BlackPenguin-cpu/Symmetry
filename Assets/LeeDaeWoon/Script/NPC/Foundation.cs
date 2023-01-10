using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

[System.Serializable]
public class Malyeog
{
    public string name;

    [TextArea(5,10)]
    public List<string> upgradeExplanation = new List<string>();
}


public class Foundation : MonoBehaviour
{
    public static Foundation instance { get; private set; }
    void Awake() => instance = this;

    public List<Malyeog> malyeog = new List<Malyeog>();

    [Header("제단")]
    const float speed = -10; // 마법진 돌아가는 속도
    [SerializeField] SpriteRenderer magicCircle; // 마법진

    [Header("업그레이드 버튼")]
    [SerializeField] Image fBtn; // 상호작용 버튼
    [SerializeField] GameObject upGrade; // 상호작용 오브젝트
    [SerializeField] Text upGradeText; // 상호작용 텍스트
    [SerializeField] bool isCollisionCheck = true; // 충돌 했는지 체크

    [Header("마력강화 창")]
    [SerializeField] GameObject upBar;
    [SerializeField] GameObject downBar;
    [SerializeField] GameObject foundationWindow;
    [SerializeField] RectTransform foundationRect;
    [SerializeField] Button closeBtn;
    bool iswindowOpenCheck = false;

    const int openBar = 452;
    const int closeBar = 30;

    const float openSpeed = 0.25f;
    const float closeSpeed = 0.32f;

    const int windowWidth = 1675;
    const int windowHeight = 885;

    [Space(10)]
    [SerializeField] Image fadeInOut;
    public Text title; // 마력 이름
    public Text explanation; // 마력 설명
    public GameObject purchase;
    public Text dimensionalPrice; // 마력 가격

    public int Magic_Open;
    public int Body_Open;

    void Start()
    {
        CloseBtn();

        upGradeText.DOFade(0, 0f);
        fBtn.DOFade(0, 0);
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
            magicCircle.DOFade(1, 1).SetEase(Ease.Linear);

        magicCircle.transform.Rotate(new Vector3(0, 0, speed * Time.deltaTime));
    }

    public void Foundation_Click()
    {
        if (Input.GetKeyDown(KeyCode.F) && isCollisionCheck == false && iswindowOpenCheck == false)
        {
            fadeInOut.DOFade(0.5f, 1).SetEase(Ease.Linear);
            UIManager.instance.isPlayerControl = true;
            OpenWindow();
            iswindowOpenCheck = true;
        }
    }

    #region 창 연출
    void CloseBtn()
    {
        closeBtn.onClick.AddListener(() =>
        {
            CloseWindow();
        });
    }

    public void OpenWindow()
    {
        SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX);
        foundationWindow.SetActive(true);

        upBar.transform.DOLocalMoveY(openBar, openSpeed).SetEase(Ease.Linear);
        downBar.transform.DOLocalMoveY(-openBar, openSpeed).SetEase(Ease.Linear);

        foundationRect.DOSizeDelta(new Vector2(windowWidth, windowHeight), openSpeed).SetEase(Ease.Linear);
    }

    public void CloseWindow()
    {
        SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX);

        upBar.transform.DOLocalMoveY(closeBar, closeSpeed).SetEase(Ease.Linear); 
        downBar.transform.DOLocalMoveY(-closeBar, closeSpeed).SetEase(Ease.Linear).OnComplete(() =>
        {
            fadeInOut.DOFade(0, 1).SetEase(Ease.Linear);

            UIManager.instance.isPlayerControl = false;
            foundationWindow.SetActive(false);
            iswindowOpenCheck = false;
        });

        foundationRect.DOSizeDelta(new Vector2(windowWidth, 0), closeSpeed).SetEase(Ease.Linear);
    }
    #endregion

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.GetComponent<ITypePlayer>() != null)
        {
            isCollisionCheck = false;
            upGradeText.DOFade(1, 0.5f).SetEase(Ease.Linear);
            fBtn.DOFade(1, 0.5f).SetEase(Ease.Linear);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.GetComponent<ITypePlayer>() != null)
        {
            isCollisionCheck = true;
            upGradeText.DOFade(0, 0.5f).SetEase(Ease.Linear);
            fBtn.DOFade(0, 0.5f).SetEase(Ease.Linear);
        }
    }
}
