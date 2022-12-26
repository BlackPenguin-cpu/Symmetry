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
        Title_Manager.instnace.isMouseCheck = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Title_Manager.instnace.isClickCheck == false)
            Title_Manager.instnace.isMouseCheck = false;
    }

    #region Å©·¹µ÷ ¹öÆ°
    public void LogoClick()
    {
        SoundManager.instance.PlaySoundClip("SFX_Button_Click", SoundType.SFX);
        StartCoroutine(Logo_Credit());
    }

    public IEnumerator Logo_Credit()
    {
        if (Title_Manager.instnace.isMouseCheck == true)
        {
            SoundManager.instance.PlaySoundClip("BGM_Editor", SoundType.BGM);
            Title_Manager.instnace.isClickCheck = true;
            Title_Manager.instnace.creditBackGround.DOFade(1f, 0.5f);
            Title_Manager.instnace.creditBackGround.raycastTarget = true;
            yield return new WaitForSeconds(0.5f);

            Title_Manager.instnace.creditText.transform.DOLocalMoveY(4702f, 50f).SetEase(Ease.Linear);

            yield return new WaitForSeconds(2.5f);
            Title_Manager.instnace.isSkipCheck = true;

            yield return new WaitForSeconds(50f);
            Title_Manager.instnace.creditBackGround.DOFade(0f, 0.5f);
            yield return new WaitForSeconds(0.5f);

            Title_Manager.instnace.creditBackGround.raycastTarget = false;
            Title_Manager.instnace.isMouseCheck = false;
            Title_Manager.instnace.isClickCheck = false;
            Title_Manager.instnace.isSkipCheck = false;
            Title_Manager.instnace.creditText.transform.localPosition = new Vector3(0f, -4764f, 0f);
        }
    }
    #endregion
}
