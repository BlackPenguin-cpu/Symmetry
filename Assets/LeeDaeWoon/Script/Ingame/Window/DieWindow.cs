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

    [SerializeField] Button backBtn;

    void Start()
    {
        CloseWindow();
    }

    void Update()
    {
        
    }

    void DieTimer() => timer.text = UIManager.instance.timerText.text;

    public void OpenWindow()
    {
        SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX);

        UIManager.instance.isCursorFade = true;
        DieTimer();

        upBar.transform.DOLocalMoveY(openBar, barSpeed).SetEase(Ease.Linear).SetUpdate(true);
        downBar.transform.DOLocalMoveY(-openBar, barSpeed).SetEase(Ease.Linear).SetUpdate(true);
        dieRect.DOSizeDelta(new Vector2(windowWidth, windowHeight), barSpeed).SetEase(Ease.Linear).SetUpdate(true);
    }

    void CloseWindow()
    {
        backBtn.onClick.AddListener(() =>
        {
            upBar.transform.DOLocalMoveY(closeBar, barSpeed).SetEase(Ease.Linear).SetUpdate(true);
            downBar.transform.DOLocalMoveY(-closeBar, barSpeed).SetEase(Ease.Linear).SetUpdate(true);
            dieRect.DOSizeDelta(new Vector2(windowWidth, 0), barSpeed).SetEase(Ease.Linear).SetUpdate(true);

            Fade.instance.fadeInOut.DOFade(1, barSpeed).SetEase(Ease.Linear).OnComplete(() =>
            {
                // 전체적으로 초기화시킨다.
                DOTween.KillAll();
                SceneManager.LoadScene(1);
            });
        });
    }
}
