using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player_Window : MonoBehaviour, IPointerEnterHandler
{
    public int Item_Log;

    void Start()
    {

    }

    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.instance.PlaySoundClip("SFX_Button_Over", SoundType.SFX, 1f);
        StopManager.instnace.itemName.text = StopManager.instnace.ItemDA_Have[Item_Log].name;
        StopManager.instnace.itemIcon.sprite = StopManager.instnace.ItemDA_Have[Item_Log].icon;
        StopManager.instnace.itemExplanation.text = StopManager.instnace.ItemDA_Have[Item_Log].explanation;
    }
}
