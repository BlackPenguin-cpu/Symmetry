using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Credit_Click : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    float timer;

    void Start()
    {

    }

    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Title_Manager.instnace.MouseCheck = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Title_Manager.instnace.click_Check == false)
            Title_Manager.instnace.MouseCheck = false;
    }

    #region Å©·¹µ÷ ¹öÆ°
    public void LogoClick()
    {
        SoundManager.instance.PlaySoundClip("SFX_Button_Click", SoundType.SFX);
        StartCoroutine(Logo_Credit());
    }

    public IEnumerator Logo_Credit()
    {
        if (Title_Manager.instnace.MouseCheck == true)
        {
            SoundManager.instance.PlaySoundClip("BGM_Editor", SoundType.BGM);
            Title_Manager.instnace.click_Check = true;
            Title_Manager.instnace.Credit_BackGround.DOFade(1f, 0.5f);
            Title_Manager.instnace.Credit_BackGround.raycastTarget = true;
            yield return new WaitForSeconds(0.5f);

            Title_Manager.instnace.Credit_Text.transform.DOLocalMoveY(4702f, 50f).SetEase(Ease.Linear);

            yield return new WaitForSeconds(2.5f);
            Title_Manager.instnace.Skip_Check = true;

            yield return new WaitForSeconds(50f);
            Title_Manager.instnace.Credit_BackGround.DOFade(0f, 0.5f);
            yield return new WaitForSeconds(0.5f);

            Title_Manager.instnace.Credit_BackGround.raycastTarget = false;
            Title_Manager.instnace.MouseCheck = false;
            Title_Manager.instnace.click_Check = false;
            Title_Manager.instnace.Skip_Check = false;
            Title_Manager.instnace.Credit_Text.transform.localPosition = new Vector3(0f, -4764f, 0f);
        }
    }
    #endregion
}
