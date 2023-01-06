using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Healing : MonoBehaviour
{
    RectTransform healingRectPos;

    [Header("신성 창")]
    [SerializeField] GameObject healingWindow;
    [SerializeField] GameObject downBar;
    [SerializeField] RectTransform healingRect;
    [SerializeField] Text healingGoldText;

    const int openBar = 444;
    const int closeBar = 80;

    const float openSpeed = 0.25f;
    const float closeSpeed = 0.23f;

    const int windowOpen = 244;
    const int windowClose = 47;
    const int windowWidth = 620;
    const int windowHeight = 397;

    [Header("힐 이펙트")]
    [SerializeField] GameObject healingEffect;

    int healGold = 0;
    float heal = 0;

    bool isColliderCheck = false;
    bool isPurchaseCheck = false;


    void Start()
    {
        SoundManager.instance.PlaySoundClip("BGM_Store", SoundType.BGM);
        healingRectPos = healingWindow.GetComponent<RectTransform>();

        healingWindow.SetActive(false);
        Healing_Price();
    }

    void Update()
    {
        #region 월드 좌표를 스크린 좌표로 변경을 해준다.
        healingRectPos.localPosition = Camera.main.WorldToScreenPoint(transform.localPosition + new Vector3(-8.7f, -5.3f, 0));
        #endregion

        StartCoroutine(Healing_Purchase());
    }

    public void Healing_Price()
    {
        // 정상 웨이브 : 5 / 10 / 15
        switch (WaveManager.instnace.m_WaveNum)
        {
            case 3:
                healGold = 560;
                heal = 50f;
                break;

            case 5:
                healGold = 1230;
                heal = 80f;
                break;

            case 15:
                healGold = 2116;
                heal = 120f;
                break;
        }
        healingGoldText.text = healGold.ToString();
    }

    IEnumerator Healing_Purchase()
    {
        if (Input.GetKeyDown(KeyCode.F) && GameManager.Instance._coin >= healGold && !isPurchaseCheck && isColliderCheck)
        {
            SoundManager.instance.PlaySoundClip("SFX_God_healling", SoundType.SFX);

            GameManager.Instance._coin -= healGold;
            UIManager.instance.isPlayerControl = true;
            isPurchaseCheck = true;

            CloseWindow();

            StartCoroutine(HealingEffect());
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(true);

            if (Player.Instance.stat._hp + heal <= Player.Instance.stat._maxHp)
                Player.Instance.stat._hp += heal;
            else
                Player.Instance.stat._hp = Player.Instance.stat._maxHp;


        }
    }

    IEnumerator HealingEffect()
    {
        healingEffect.SetActive(true);
        healingEffect.transform.DOLocalMove(new Vector2(Player.Instance.transform.position.x, -0.3f), 0);
        yield return new WaitForSeconds(2f);

        healingEffect.SetActive(false);
        UIManager.instance.isPlayerControl = false;
    }

    #region 신성 창
    void OpenWindow()
    {
        downBar.transform.DOKill();
        healingRect.transform.DOKill();

        SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX);
        healingRect.DOLocalMoveY(-windowOpen, openSpeed).SetEase(Ease.Linear);
        healingRect.DOSizeDelta(new Vector2(windowWidth, windowHeight), openSpeed).SetEase(Ease.Linear);
        downBar.transform.DOLocalMoveY(-openBar, openSpeed).SetEase(Ease.Linear);
    }

    void CloseWindow()
    {
        downBar.transform.DOKill();
        healingRect.transform.DOKill();

        SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX);
        healingRect.DOLocalMoveY(-windowClose, closeSpeed).SetEase(Ease.Linear);
        healingRect.DOSizeDelta(new Vector2(windowWidth, 0), closeSpeed).SetEase(Ease.Linear);
        downBar.transform.DOLocalMoveY(-closeBar, closeSpeed).SetEase(Ease.Linear).OnComplete(() =>
        {
            healingWindow.SetActive(false);
        });
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Player") || collision.GetComponent<ITypePlayer>() != null) && !isPurchaseCheck)
        {
            isColliderCheck = true;
            healingWindow.SetActive(true);
            OpenWindow();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.CompareTag("Player") || collision.GetComponent<ITypePlayer>() != null) && !isPurchaseCheck)
        {
            isColliderCheck = false;
            CloseWindow();
        }
    }
}