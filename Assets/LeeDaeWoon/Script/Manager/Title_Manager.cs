using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class Title_Manager : MonoBehaviour
{
    public static Title_Manager instnace { get; private set; }

    [Header("타이틀")]
    public GameObject Title_Logo;
    public GameObject Title_Circle01;
    public GameObject Title_Circle02;
    public Text Fade_Text;

    public Image Team_BackGround;
    private bool Team_BackGround_Check;

    public bool MouseCheck;

    [Header("크레딧")]
    public Image FadeInout;
    public Image Credit_BackGround;
    public GameObject Credit_Text;
    public bool click_Check;
    public bool Skip_Check;

    void Start() => StartCoroutine(TeamLogo_BackGround());

    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (MouseCheck == false && Skip_Check != true)
                Change_Scene();

            if (Skip_Check == true)
                StartCoroutine(Credit_ESC());
        }
    }

    private void Awake()
    {
        instnace = this;
        Sart_Coroutine();
    }

    public void Sart_Coroutine()
    {
        StartCoroutine(Circle01());
        StartCoroutine(FadeText_Full());
    }

    public void Change_Scene()
    {
        if (Team_BackGround_Check == true)
        {
            DOTween.PauseAll();
            SceneManager.LoadScene("Main");
        }

    }

    public IEnumerator Credit_ESC()
    {
        FadeInout.DOFade(1f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        Credit_Text.transform.DOPause();
        Credit_BackGround.DOFade(0f, 0f);
        Credit_Text.transform.localPosition = new Vector3(0f, -4764f, 0f);
        MouseCheck = false;
        click_Check = false;
        Skip_Check = false;
        Credit_BackGround.raycastTarget = false;
        FadeInout.DOFade(0f, 0.5f);
    }

    #region 타이틀 원
    public IEnumerator Circle01()
    {
        Title_Circle01.transform.DOLocalMoveY(327f, 3f).SetEase(Ease.InOutCubic);
        Title_Circle02.transform.DOLocalMoveY(-327f, 3f).SetEase(Ease.InOutCubic);
        yield return new WaitForSeconds(3f);
        StartCoroutine(Circle02());
    }

    public IEnumerator Circle02()
    {
        Title_Circle01.transform.DOLocalMoveY(142f, 3f).SetEase(Ease.InOutCubic);
        Title_Circle02.transform.DOLocalMoveY(-142f, 3f).SetEase(Ease.InOutCubic);
        yield return new WaitForSeconds(3f);
        StartCoroutine(Circle01());
    }
    #endregion

    #region FadeInOut
    public IEnumerator FadeText_Full()
    {
        Fade_Text.DOFade(0f, 1.5f);
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(FadeText_Zero());
    }

    public IEnumerator FadeText_Zero()
    {
        Fade_Text.DOFade(1f, 1.5f);
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(FadeText_Full());
    }
    #endregion

    #region 팀 배경
    public IEnumerator TeamLogo_BackGround()
    {
        Credit_BackGround.raycastTarget = true;
        yield return new WaitForSeconds(3f);
        Team_BackGround.DOFade(0f, 3f);
        yield return new WaitForSeconds(4f);
        Credit_BackGround.raycastTarget = false;
        Team_BackGround_Check = true;
    }
    #endregion

}
