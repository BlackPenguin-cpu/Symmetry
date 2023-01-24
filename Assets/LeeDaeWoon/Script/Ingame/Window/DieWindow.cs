using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DieWindow : MonoBehaviour
{
    public static DieWindow instance;
    void Awake() => instance = this;

    [Header("게임오버 창")]
    public GameObject dieWindow;
    [SerializeField] GameObject upBar;
    [SerializeField] GameObject downBar;
    [SerializeField] RectTransform dieRect;
    [SerializeField] Text timer;

    const int openBar = 447;
    const int closeBar = 30;
    const float barSpeed = 0.4f;

    const int windowWidth = 1675;
    const int windowHeight = 885;

    [SerializeField] GameObject itemContent;
    [SerializeField] GameObject weaponLevel;
    [SerializeField] Button backBtn;

    void Start()
    {
        CloseWindow();
    }

    void Update()
    {

    }

    void DieTimer() => timer.text = UIManager.instance.timerText.text;

    void DieItem()
    {
        if (StopManager.instnace.itemDaHave.Count != 0)
        {
            for (int i = 0; i < itemContent.transform.childCount; i++)
            {
                var icon = itemContent.transform.GetChild(i).GetChild(0).GetComponent<Image>();
                var name = itemContent.transform.GetChild(i).GetChild(1).GetComponent<Text>();
                var explanation = itemContent.transform.GetChild(i).GetChild(2).GetComponent<Text>();

                icon.sprite = StopManager.instnace.itemDaHave[i].icon;
                name.text = StopManager.instnace.itemDaHave[i].name;
                explanation.text = StopManager.instnace.itemDaHave[i].explanation;
            }
        }
    }

    void DieWeapon()
    {
        for (int i = 0; i <= StopManager.instnace.weapon.Count; i++)
        {
            if (StopManager.instnace.weapon[i].level != 0)
                weaponLevel.transform.GetChild(i).GetChild(1).GetComponent<Text>().text = "Lv." + StopManager.instnace.weapon[i].level;
            else
            {
                weaponLevel.transform.GetChild(i).GetChild(0).GetComponent<Image>().DOColor(Color.gray, 0);
                weaponLevel.transform.GetChild(i).GetChild(1).gameObject.SetActive(false);
            }

        }
    }

    public void OpenWindow()
    {
        SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX);

        UIManager.instance.isCursorFade = true;

        upBar.transform.DOLocalMoveY(openBar, barSpeed).SetEase(Ease.Linear).SetUpdate(true);
        downBar.transform.DOLocalMoveY(-openBar, barSpeed).SetEase(Ease.Linear).SetUpdate(true);
        dieRect.DOSizeDelta(new Vector2(windowWidth, windowHeight), barSpeed).SetEase(Ease.Linear).SetUpdate(true);

        DieTimer();
        DieItem();
        DieWeapon();
    }

    void CloseWindow()
    {
        backBtn.onClick.AddListener(() =>
        {
            dieRect.DOSizeDelta(new Vector2(windowWidth, 0), barSpeed).SetEase(Ease.Linear).SetUpdate(true);
            upBar.transform.DOLocalMoveY(closeBar, barSpeed).SetEase(Ease.Linear).SetUpdate(true);
            downBar.transform.DOLocalMoveY(-closeBar, barSpeed).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
            {
                Fade.instance.fadeInOut.DOFade(1, barSpeed).SetEase(Ease.Linear).OnComplete(() =>
                {
                    // 전체적으로 초기화시킨다.
                    DOTween.KillAll();
                    SceneManager.LoadScene(1);
                });
            });
        });
    }
}
