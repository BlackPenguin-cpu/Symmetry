using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OverBtnSound : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventDatas)
    {
        SoundManager.instance.PlaySoundClip("SFX_Button_Over", SoundType.SFX);
    }
}