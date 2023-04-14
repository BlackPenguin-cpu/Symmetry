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
    [SerializeField] Image teamBackGround;
    [SerializeField] Text passAnyKey;

    bool isTeamBackGround = false;

    [Header("크레딧")]
    public Image creditBackGround;
    public GameObject creditText;

    public bool isCredit = false;
    public bool isSkipCheck = false;
    public bool isCreditCheck = false;

    void Start()
    {

        StartCoroutine(Director());
        SoundManager.instance.PlaySoundClip("BGM_Title", SoundType.BGM);
    }

    void Update()
    {
        ChangeScene();
    }

    void ChangeScene()
    {
        if (Input.anyKeyDown)
        {
            if (!isCredit && !isSkipCheck && isTeamBackGround)
            {
                Fade.instance.fadeInOut.DOFade(1, 0.5f).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
                {
                    DOTween.KillAll();
                    SceneManager.LoadScene(1);
                });
            }

            else
                CreditESC();
        }
    }

    void CreditESC()
    {
        float waitTime = 0.5f;
        int creditTextPos = 4764;

        if (isSkipCheck)
        {
            SoundManager.instance.PlaySoundClip("BGM_Title", SoundType.BGM);

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

    IEnumerator Director()
    {
        int circlePos = 400;

        // passAnyKey
        passAnyKey.DOFade(0, 1.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);

        // circleBall
        circleBall.transform.GetChild(0).transform.DOLocalMoveY(circlePos, 3).SetEase(Ease.InOutCubic).SetLoops(-1, LoopType.Yoyo);
        circleBall.transform.GetChild(1).transform.DOLocalMoveY(-circlePos, 3).SetEase(Ease.InOutCubic).SetLoops(-1, LoopType.Yoyo);

        // TeamBackGround
        yield return new WaitForSeconds(3);
        teamBackGround.DOFade(0, 3).OnComplete(() =>
        {
            teamBackGround.raycastTarget = false;
            isTeamBackGround = true;
        });
    }
}
