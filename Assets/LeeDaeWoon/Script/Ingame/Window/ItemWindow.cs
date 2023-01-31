using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemWindow : MonoBehaviour
{
    float timer = 0;

    [Header("Ã¢")]
    [SerializeField] RectTransform leftWindow;
    [SerializeField] RectTransform amongWindow;
    [SerializeField] RectTransform rightWindow;

    const int windowWidth = 545;
    const int windowHeight = 890;

    [Header("ºÀ")]
    [SerializeField] GameObject leftBarUp;
    [SerializeField] GameObject leftBarDown;
    [SerializeField] GameObject amongBarUp;
    [SerializeField] GameObject amongBarDown;
    [SerializeField] GameObject rightBarUp;
    [SerializeField] GameObject rightBarDown;

    const int barPos = 450;
    const float barSpeed = 0.38f;

    void Start()
    {
        OpenWindow();
    }

    void Update()
    {

    }

    void OpenWindow()
    {
        Time.timeScale = 0;
        Fade.instance.fadeInOut.DOFade(0.5f, 0.2f).SetEase(Ease.Linear).SetUpdate(true);

        leftWindow.DOSizeDelta(new Vector2(windowWidth, windowHeight), barSpeed).SetEase(Ease.Linear).SetUpdate(true);
        amongWindow.DOSizeDelta(new Vector2(windowWidth, windowHeight), barSpeed).SetEase(Ease.Linear).SetUpdate(true);
        rightWindow.DOSizeDelta(new Vector2(windowWidth, windowHeight), barSpeed).SetEase(Ease.Linear).SetUpdate(true);

        leftBarUp.transform.DOLocalMoveY(barPos, barSpeed).SetEase(Ease.Linear).SetUpdate(true);
        leftBarDown.transform.DOLocalMoveY(-barPos, barSpeed).SetEase(Ease.Linear).SetUpdate(true);

        amongBarUp.transform.DOLocalMoveY(barPos, barSpeed).SetEase(Ease.Linear).SetUpdate(true);
        amongBarDown.transform.DOLocalMoveY(-barPos, barSpeed).SetEase(Ease.Linear).SetUpdate(true);

        rightBarUp.transform.DOLocalMoveY(barPos, barSpeed).SetEase(Ease.Linear).SetUpdate(true);
        rightBarDown.transform.DOLocalMoveY(-barPos, barSpeed).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
        {
            CardManager.instance.isItemClick = true;
        });
    }
}
