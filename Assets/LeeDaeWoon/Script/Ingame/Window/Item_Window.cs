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
    public GameObject Left_Pole_01;
    public GameObject Left_Pole_02;
    public RectTransform Left_RectMask;

    [Header("가운데 창")]
    public GameObject Among_Pole_01;
    public GameObject Among_Pole_02;
    public RectTransform Among_RectMask;


    [Header("오른쪽 창")]
    public GameObject Right_Pole_01;
    public GameObject Right_Pole_02;
    public RectTransform Right_RectMask;


    [Header("빛")]
    public Image Left_Light;
    public Image Among_Light;
    public Image Right_Light;

    public int Light_num;


    void Start()
    {
        StartCoroutine(itemWindow());
    }

    void Update()
    {

    }

    private IEnumerator itemWindow()
    {
        Card_Manager.instance.Fade.DOFade(0.5f, 0.5f);
        UI_Manager.instance.isCursorFade = true;

        Card_Manager.instance.ItemCard_OpenCheck = true;
        Window_timer = 0;

        #region 창 연출(위, 아래 봉)

        Left_Pole_01.transform.DOLocalMoveY(460.9912f, 0.55f);
        Left_Pole_02.transform.DOLocalMoveY(-457f, 0.55f);

        Among_Pole_01.transform.DOLocalMoveY(361f, 0.55f);
        Among_Pole_02.transform.DOLocalMoveY(-554f, 0.55f);

        Right_Pole_01.transform.DOLocalMoveY(384f, 0.55f);
        Right_Pole_02.transform.DOLocalMoveY(-538f, 0.55f);

        #endregion

        while (Window_timer < 1)
        {
            Left_RectMask.sizeDelta = new Vector2(559.2f, Mathf.Lerp(0, 890f, Window_timer));
            Among_RectMask.sizeDelta = new Vector2(522.6044f, Mathf.Lerp(0, 890f, Window_timer));
            Right_RectMask.sizeDelta = new Vector2(574.5f, Mathf.Lerp(0, 890f, Window_timer));

            Window_timer += Time.deltaTime * 3f;
            yield return null;
        }
        Card_Manager.instance.ItemCard_OpenCheck = false;
    }
}
