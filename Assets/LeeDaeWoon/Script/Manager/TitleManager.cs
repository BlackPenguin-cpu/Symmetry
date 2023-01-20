using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class TitleManager : MonoBehaviour
{
    public static TitleManager instnace { get; private set; }
    void Awake() => instnace = this;

    [Header("타이틀")]
    [SerializeField] GameObject circleBall;
    [SerializeField] Text passAnyKey;

    [SerializeField] Image teamBackGround;
    bool isTeamBackGround = false;

    [Header("크레딧")]
    [SerializeField] Button creditBtn;
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
            if (!isCreditOut && !isSkipCheck)
                ChangeScene();

            else
                CreditESC();
        }
    }

    public void ChangeScene()
    {
        if (isTeamBackGround)
        {
            Fade.instance.fadeInOut.DOFade(1, 0.5f).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
            {
                DOTween.KillAll();
                SceneManager.LoadScene(1);
            });
        }
    }

    public void CreditESC()
    {
        float waitTime = 0.5f;
        int creditTextPos = 4764;

        if (isSkipCheck)
        {
            creditText.transform.DOKill();
            creditText.transform.DOLocalMoveY(-creditTextPos, 0).SetEase(Ease.Linear);
            creditBackGround.DOFade(0, waitTime).OnComplete(() =>
            {
                isSkipCheck = false;
                isCreditCheck = false;
                creditBackGround.raycastTarget = false;
            });
        }
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
