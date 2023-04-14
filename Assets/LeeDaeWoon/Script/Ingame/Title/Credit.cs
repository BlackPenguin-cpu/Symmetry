using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class Credit : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    TitleManager titleManager;

    [Header("크레딧")]
    Button creditBtn;

    const float waitTime = 0.5f;

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
        titleManager.isCredit = true;
        SoundManager.instance.PlaySoundClip("SFX_Button_Over", SoundType.SFX);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!titleManager.isCreditCheck)
            titleManager.isCredit = false;
    }

    IEnumerator Skip()
    {
        yield return new WaitForSeconds(2);
        titleManager.isSkipCheck = true;
    }

    void CreditBtn()
    {
        int creditTextPos = 4700;

        // 크레딧 버튼을 클릭을 했을 경우
        creditBtn.onClick.AddListener(() =>
        {
            titleManager.isCreditCheck = true;
            Vector3 creditPos = titleManager.creditText.transform.localPosition;

            SoundManager.instance.PlaySoundClip("BGM_Editor", SoundType.BGM);
            SoundManager.instance.PlaySoundClip("SFX_Button_Click", SoundType.SFX);

            titleManager.creditBackGround.raycastTarget = true;
            titleManager.creditBackGround.DOFade(1, 0.5f).OnComplete(() =>
            {
                StartCoroutine(Skip());
            });

            // 크레딧 이동이 지정한 만큼 이동했을 때
            titleManager.creditText.transform.DOLocalMoveY(creditTextPos, waitTime * 100).SetEase(Ease.Linear).OnComplete(() =>
            {
                SoundManager.instance.PlaySoundClip("BGM_Title", SoundType.BGM);

                titleManager.creditBackGround.DOFade(0f, waitTime).SetEase(Ease.Linear);
                titleManager.creditText.transform.localPosition = creditPos;

                titleManager.isSkipCheck = false;
                titleManager.isCreditCheck = false;
                titleManager.creditBackGround.raycastTarget = false;
            });

        });
    }
}
