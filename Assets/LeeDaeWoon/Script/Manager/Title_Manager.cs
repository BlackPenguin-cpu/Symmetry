using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class Title_Manager : MonoBehaviour
{
    public static Title_Manager instnace { get; private set; }
    void Awake() => instnace = this;

    [Header("타이틀")]
    [SerializeField] GameObject circleBall;
    [SerializeField] Text passAnyKey;

    [SerializeField] Image teamBackGround;
    bool isTeamBackGround = false;

    [Header("크레딧")]
    [SerializeField] Button creditBtn;
    [SerializeField] Image fadeInOut;
    public Image creditBackGround;
    public GameObject creditText;

    public bool isCreditOut = false;
    public bool isSkipCheck = false;
    public bool isCreditCheck = false;

    const int timer = 3;

    void Start()
    {
        TitleDirector();
        StartCoroutine(TeamBackGround());
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (isCreditOut == false && isSkipCheck == false)
                Change_Scene();
            //else
                //StartCoroutine(Credit_ESC());
        }
    }

    public void Change_Scene()
    {
        if (isTeamBackGround == true)
        {
            DOTween.KillAll();
            SceneManager.LoadScene("Main");
        }

    }

    public IEnumerator Credit_ESC()
    {
        fadeInOut.DOFade(1f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        creditText.transform.DOPause();
        creditBackGround.DOFade(0f, 0f);
        creditText.transform.localPosition = new Vector3(0f, -4764f, 0f);
        isSkipCheck = false;
        creditBackGround.raycastTarget = false;
        fadeInOut.DOFade(0f, 0.5f);
    }

    void TitleDirector()
    {
        int circlePos = 327;

        // passAnyKey Director
        passAnyKey.DOFade(0, 1.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);

        // circleBall Director
        circleBall.transform.GetChild(0).transform.DOLocalMoveY(circlePos, timer).SetEase(Ease.InOutCubic).SetLoops(-1, LoopType.Yoyo);
        circleBall.transform.GetChild(1).transform.DOLocalMoveY(-circlePos, timer).SetEase(Ease.InOutCubic).SetLoops(-1, LoopType.Yoyo);
    }

    IEnumerator TeamBackGround()
    {
        yield return new WaitForSeconds(timer);
        teamBackGround.DOFade(0, timer).OnComplete(() =>
        {
            teamBackGround.raycastTarget = false;
            isTeamBackGround = true;
        });
    }
}
