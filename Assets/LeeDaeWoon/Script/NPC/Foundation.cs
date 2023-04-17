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

    [TextArea(5, 10)]
    public List<string> upgradeExplanation = new List<string>();
}

public class Foundation : MonoBehaviour
{
    public static Foundation instance { get; private set; }
    void Awake() => instance = this;

    public List<Malyeog> malyeog = new List<Malyeog>();

    [Header("제단")]
    const float speed = -10; // 마법진 돌아가는 속도
    public SpriteRenderer magicCircle;

    [Header("업그레이드 버튼")]
    [SerializeField] Image fBtn;
    [SerializeField] GameObject upGrade;
    [SerializeField] Text upGradeText;
    [SerializeField] bool isCollisionCheck = true;

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
    public Text title;
    public Text explanation;
    public GameObject purchase;
    public Text dimensionalPrice;

    void Start()
    {
        upGradeText.DOFade(0, 0);
        fBtn.DOFade(0, 0);

        CloseBtn();
    }

    void Update()
    {
        Foundation_Click();
        MagicCircle();
    }

    private void ScreenVector(Vector3 vec) => upGrade.transform.localPosition = Camera.main.WorldToScreenPoint(gameObject.transform.localPosition + vec);

    public void MagicCircle()
    {
        magicCircle.transform.Rotate(new Vector3(0, 0, speed * Time.deltaTime));
        switch (CurrentScene.instance.eScene)
        {
            case EScene.Main:
                Vector2 mainPos = new Vector2(-10.8f, -7);

                ScreenVector(mainPos);
                break;

            case EScene.Dimension:
                Vector2 dimension = new Vector2(-5.2f, -4.4f);

                ScreenVector(dimension);
                break;
        }
    }

    public void Foundation_Click()
    {
        if (Input.GetKeyDown(KeyCode.F) && isCollisionCheck == false && iswindowOpenCheck == false)
        {
            Fade.instance.fadeInOut.DOFade(0.5f, 0.2f).SetEase(Ease.Linear).SetUpdate(true);

            UIManager.instance.isPlayerControl = true;
            StopManager.instnace.isEscCheck = true;
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
            StopManager.instnace.isEscCheck = false;
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
            Fade.instance.fadeInOut.DOFade(0, 0.2f).SetEase(Ease.Linear).SetUpdate(true);

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
