using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemWindow : MonoBehaviour
{
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
        BarMove();
        StartCoroutine(itemWindow());
    }

    void Update()
    {

    }

    void BarMove()
    {
        leftBarUp.transform.DOLocalMoveY(barPos, barSpeed).SetEase(Ease.Linear);
        leftBarDown.transform.DOLocalMoveY(-barPos, barSpeed).SetEase(Ease.Linear);

        amongBarUp.transform.DOLocalMoveY(barPos, barSpeed).SetEase(Ease.Linear);
        amongBarDown.transform.DOLocalMoveY(-barPos, barSpeed).SetEase(Ease.Linear);

        rightBarUp.transform.DOLocalMoveY(barPos, barSpeed).SetEase(Ease.Linear);
        rightBarDown.transform.DOLocalMoveY(-barPos, barSpeed).SetEase(Ease.Linear);
    }

    IEnumerator itemWindow()
    {
        float windowTimer = 0;

        CardManager.instance.fade.DOFade(0.5f, 0.5f);
        UI_Manager.instance.isCursorFade = true;
        CardManager.instance.isItemCardOpenCheck = true;

        while (windowTimer < 1)
        {
            leftWindow.sizeDelta = new Vector2(windowWidth, Mathf.Lerp(0, windowHeight, windowTimer));
            amongWindow.sizeDelta = new Vector2(windowWidth, Mathf.Lerp(0, windowHeight, windowTimer));
            rightWindow.sizeDelta = new Vector2(windowWidth, Mathf.Lerp(0, windowHeight, windowTimer));

            windowTimer += Time.deltaTime * 3f;
            yield return null;
        }
        CardManager.instance.isItemCardOpenCheck = false;
    }
}
