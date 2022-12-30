using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
        while (timer < 1)
        {
            leftWindow.sizeDelta = new Vector2(windowWidth, Mathf.Lerp(0, windowHeight, timer));
            amongWindow.sizeDelta = new Vector2(windowWidth, Mathf.Lerp(0, windowHeight, timer));
            rightWindow.sizeDelta = new Vector2(windowWidth, Mathf.Lerp(0, windowHeight, timer));

            timer += Time.deltaTime * 3f;
            yield return null;
        }
    }
}
