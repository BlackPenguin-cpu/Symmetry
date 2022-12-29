using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Item_Window : MonoBehaviour
{
    [Header("속도")]
    public float Window_timer = 0f;

    [Header("왼쪽 창")]
    [SerializeField] GameObject leftBarUp;
    [SerializeField] GameObject leftBarDown;
    [SerializeField] RectTransform leftWindow;

    [Header("가운데 창")]
    [SerializeField] GameObject amongBarUp;
    [SerializeField] GameObject amongBarDown;
    [SerializeField] RectTransform amongWindow;


    [Header("오른쪽 창")]
    [SerializeField] GameObject rightBarUp;
    [SerializeField] GameObject rightBarDown;
    [SerializeField] RectTransform rightWindow;

    public int Light_num;


    const int barPos = 450;
    const int windowPos = 890;

    const float barSpeed = 0.55f;

    void Start()
    {
        StartCoroutine(itemWindow());
    }

    void Update()
    {

    }

    private IEnumerator itemWindow()
    {
        Card_Manager.instance.fade.DOFade(0.5f, 0.5f);
        UI_Manager.instance.isCursorFade = true;

        Card_Manager.instance.isItemCardOpenCheck = true;
        Window_timer = 0;

        #region 창 연출(위, 아래 봉)

        leftBarUp.transform.DOLocalMoveY(barPos, barSpeed);
        leftBarDown.transform.DOLocalMoveY(-barPos, barSpeed);

        amongBarUp.transform.DOLocalMoveY(barPos, barSpeed);
        amongBarDown.transform.DOLocalMoveY(-barPos, barSpeed);

        rightBarUp.transform.DOLocalMoveY(barPos, barSpeed);
        rightBarDown.transform.DOLocalMoveY(-barPos, barSpeed);

        #endregion

        while (Window_timer < 1)
        {
            leftWindow.sizeDelta = new Vector2(0, Mathf.Lerp(0, windowPos, Window_timer));
            amongWindow.sizeDelta = new Vector2(0, Mathf.Lerp(0, windowPos, Window_timer));
            rightWindow.sizeDelta = new Vector2(0, Mathf.Lerp(0, windowPos, Window_timer));

            Window_timer += Time.deltaTime * 3f;
            yield return null;
        }
        Card_Manager.instance.isItemCardOpenCheck = false;
    }
}
