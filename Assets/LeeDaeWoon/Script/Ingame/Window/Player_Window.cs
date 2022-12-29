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
        StopManager.instnace.Player_Item_Name.text = StopManager.instnace.ItemDA_Have[Item_Log].Itme_Name;
        StopManager.instnace.Player_Item_Icon.sprite = StopManager.instnace.ItemDA_Have[Item_Log].Item_Icon;
        StopManager.instnace.Player_Item_Explanation.text = StopManager.instnace.ItemDA_Have[Item_Log].Item_Explanation;
    }
}
