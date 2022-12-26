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

    [Header("Å¸ÀÌÆ²")]
    [SerializeField] GameObject circleBall;
    [SerializeField] Text passAnyKey;

    [SerializeField] Image teamBackGround;
    public bool isMouseCheck;
    bool isTeamBackGround = false;

    [Header("Å©·¹µ÷")]
    [SerializeField] Image fadeInOut;
    public Image creditBackGround;
    public GameObject creditText;
    public bool isClickCheck;
    public bool isSkipCheck;

    void Start()
    {
        TitleDirector();
        //StartCoroutine(TeamLogo_BackGround());
    }

    void Update()
    {
        //if (Input.anyKeyDown)
        //{
        //    if (isMouseCheck == false && isSkipCheck != true)
        //        Change_Scene();

        //    if (isSkipCheck == true)
        //        StartCoroutine(Credit_ESC());
        //}
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
        isMouseCheck = false;
        isClickCheck = false;
        isSkipCheck = false;
        creditBackGround.raycastTarget = false;
        fadeInOut.DOFade(0f, 0.5f);
    }

    void TitleDirector()
    {
        int timer = 3;
        int circlePos = 327;

        passAnyKey.DOFade(0, 1.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);

        circleBall.transform.GetChild(0).transform.DOLocalMoveY(circlePos, timer).SetEase(Ease.InOutCubic).SetLoops(-1, LoopType.Yoyo);
        circleBall.transform.GetChild(1).transform.DOLocalMoveY(-circlePos, timer).SetEase(Ease.InOutCubic).SetLoops(-1, LoopType.Yoyo);
    }

    #region ÆÀ ¹è°æ
    public IEnumerator TeamLogo_BackGround()
    {
        creditBackGround.raycastTarget = true;
        yield return new WaitForSeconds(3f);
        teamBackGround.DOFade(0f, 3f);
        yield return new WaitForSeconds(4f);
        creditBackGround.raycastTarget = false;
        isTeamBackGround = true;
    }
    #endregion

}
