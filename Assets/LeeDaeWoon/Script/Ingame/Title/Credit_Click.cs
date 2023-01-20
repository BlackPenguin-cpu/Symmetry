using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class Credit_Click : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Button creditBtn;

    const float waitTime = 0.5f;
    TitleManager titleManager;

    void Start()
    {
        titleManager = TitleManager.instnace;
        creditBtn = GetComponent<Button>();

        CreditBtn();
    }

    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        titleManager.isCreditOut = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!titleManager.isCreditCheck)
            titleManager.isCreditOut = false;
    }

    public void CreditBtn()
    {
        int creditTextPos = 4702;

        creditBtn.onClick.AddListener(() =>
        {
            titleManager.isCreditCheck = true;

            SoundManager.instance.PlaySoundClip("SFX_Button_Click", SoundType.SFX);
            SoundManager.instance.PlaySoundClip("BGM_Editor", SoundType.BGM);

            titleManager.creditBackGround.raycastTarget = true;
            titleManager.creditBackGround.DOFade(1, 0.5f).OnComplete(() =>
            {
                StartCoroutine(SkipTure());
            });

            titleManager.creditText.transform.DOLocalMoveY(creditTextPos, waitTime * 100).SetEase(Ease.Linear).OnComplete(() =>
            {
                SoundManager.instance.PlaySoundClip(null, SoundType.BGM);

                titleManager.creditBackGround.DOFade(0f, waitTime);
                titleManager.creditText.transform.localPosition = new Vector3(0f, -4764f, 0f);

                titleManager.isSkipCheck = false;
                titleManager.isCreditCheck = false;
                titleManager.creditBackGround.raycastTarget = false;
            });

        });
    }

    IEnumerator SkipTure()
    {
        yield return new WaitForSeconds(2);
        titleManager.isSkipCheck = true;
    }
}
