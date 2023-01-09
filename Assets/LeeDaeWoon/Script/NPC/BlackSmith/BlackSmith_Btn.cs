using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class BlackSmith_Btn : MonoBehaviour, IPointerEnterHandler
{
    public enum MouseOver_UI
    {
        MouseOver = 0,
        None = 1
    }
    public MouseOver_UI mouseover_UI;


    void Start()
    {

    }

    void Update()
    {


    }

    #region 버튼 클릭

    public void Left_Arrow()
    {
        if (mouseover_UI == MouseOver_UI.MouseOver)
        {
            int Save_BlackSmiths = BlackSmith.instnace.BlackSmiths.Count;
            BlackSmith.instnace.BlackSmiths[0].SetActive(false);

            BlackSmith.instnace.BlackSmiths.Insert(0, BlackSmith.instnace.BlackSmiths[Save_BlackSmiths - 1]);
            BlackSmith.instnace.BlackSmiths.RemoveAt(Save_BlackSmiths);
            BlackSmith.instnace.BlackSmiths[0].SetActive(true);
        }
    }

    public void Right_Arrow()
    {
        if (mouseover_UI == MouseOver_UI.MouseOver)
        {
            BlackSmith.instnace.BlackSmiths[0].SetActive(false);

            BlackSmith.instnace.BlackSmiths.Add(BlackSmith.instnace.BlackSmiths[0]);
            BlackSmith.instnace.BlackSmiths.RemoveAt(0);
            BlackSmith.instnace.BlackSmiths[0].SetActive(true);
        }
    }

    public void Purchase_Click()
    {
        int Gold = 300;
        if (mouseover_UI == MouseOver_UI.MouseOver)
        {
            //단검
            if (BlackSmith.instnace.Weapon.transform.GetChild(1).gameObject.activeSelf == true && GameManager.Instance._coin >= Gold)
            {
                SoundManager.instance.PlaySoundClip("SFX_Buy", SoundType.SFX, 5f);
                GameManager.Instance._coin -= Gold; // 골드 차감
                BlackSmith.instnace.Dagger_Price.SetActive(false); // 구매가격 false
                BlackSmith.instnace.Dagger_Required_Gold.SetActive(true); // 강화 가격 true
                Player.Instance.stat.weaponType = PlayerWeaponType.Dagger; // 무기 단검으로 바뀜
                BlackSmith.instnace.Purchase_Btn.SetActive(false); // 구매 버튼 false
                BlackSmith.instnace.Enhance_Btn.SetActive(true); // 강화 버튼 true
            }
            else if (GameManager.Instance._coin < Gold)
                SoundManager.instance.PlaySoundClip("SFX_Error", SoundType.SFX);


            //도끼
            if (BlackSmith.instnace.Weapon.transform.GetChild(2).gameObject.activeSelf == true && GameManager.Instance._coin >= Gold)
            {
                SoundManager.instance.PlaySoundClip("SFX_Buy", SoundType.SFX, 5f);
                GameManager.Instance._coin -= Gold; // 골드 차감
                BlackSmith.instnace.Axe_Price.SetActive(false); // 구매가격 false
                BlackSmith.instnace.Axe_Required_Gold.SetActive(true); // 강화 가격 true
                Player.Instance.stat.weaponType = PlayerWeaponType.Axe; // 무기 도끼으로 바뀜
                BlackSmith.instnace.Purchase_Btn.SetActive(false); // 구매 버튼 false
                BlackSmith.instnace.Enhance_Btn.SetActive(true); // 강화 버튼 true
            }
            else if(GameManager.Instance._coin < Gold)
                SoundManager.instance.PlaySoundClip("SFX_Error", SoundType.SFX);
        }

    }

    public void JangCak_Click()
    {
        if (mouseover_UI == MouseOver_UI.MouseOver)
        {
            if (BlackSmith.instnace.Weapon.transform.GetChild(0).gameObject.activeSelf == true)
                Player.Instance.stat.weaponType = PlayerWeaponType.Sword;
            else if (BlackSmith.instnace.Weapon.transform.GetChild(1).gameObject.activeSelf == true)
                Player.Instance.stat.weaponType = PlayerWeaponType.Dagger;
            else if (BlackSmith.instnace.Weapon.transform.GetChild(2).gameObject.activeSelf == true)
                Player.Instance.stat.weaponType = PlayerWeaponType.Axe;
        }

    }

    public void Enhance_Click()
    {
        int Sword_Level = Player.Instance.stat._level[PlayerWeaponType.Sword];
        int Dagger_Level = Player.Instance.stat._level[PlayerWeaponType.Dagger];
        int Axe_Level = Player.Instance.stat._level[PlayerWeaponType.Axe];
        if (mouseover_UI == MouseOver_UI.MouseOver)
        {
            switch (Player.Instance.stat.weaponType)
            {
                case PlayerWeaponType.Sword:
                    if (BlackSmith.instnace.Weapon.transform.GetChild(0).gameObject.activeSelf == true && GameManager.Instance._coin >= (400 + (200 * Sword_Level)))
                    {
                        if (Sword_Level < 5)
                        {
                            SoundManager.instance.PlaySoundClip("SFX_Enforce", SoundType.SFX);
                            GameManager.Instance._coin -= (400 + (200 * Sword_Level));
                            Player.Instance.stat._level[PlayerWeaponType.Sword]++;
                        }
                        else
                            SoundManager.instance.PlaySoundClip("SFX_Error", SoundType.SFX);
                    }
                    else if (GameManager.Instance._coin < (400 + (200 * Sword_Level)))
                        SoundManager.instance.PlaySoundClip("SFX_Error", SoundType.SFX);

                    break;

                case PlayerWeaponType.Dagger:
                    if (BlackSmith.instnace.Weapon.transform.GetChild(1).gameObject.activeSelf == true && GameManager.Instance._coin >= (400 + (200 * Dagger_Level)))
                    {
                        if (Dagger_Level < 5)
                        {
                            SoundManager.instance.PlaySoundClip("SFX_Enforce", SoundType.SFX);
                            GameManager.Instance._coin -= (400 + (200 * Dagger_Level));
                            Player.Instance.stat._level[PlayerWeaponType.Dagger]++;
                        }
                        else
                            SoundManager.instance.PlaySoundClip("SFX_Error", SoundType.SFX);
                    }
                    else if (GameManager.Instance._coin < (400 + (200 * Sword_Level)))
                        SoundManager.instance.PlaySoundClip("SFX_Error", SoundType.SFX);
                    break;

                case PlayerWeaponType.Axe:
                    if (BlackSmith.instnace.Weapon.transform.GetChild(2).gameObject.activeSelf == true && GameManager.Instance._coin >= (400 + (200 * Axe_Level)))
                    {
                        if (Axe_Level < 5)
                        {
                            SoundManager.instance.PlaySoundClip("SFX_Enforce", SoundType.SFX);
                            GameManager.Instance._coin -= (400 + (200 * Axe_Level));
                            Player.Instance.stat._level[PlayerWeaponType.Axe]++;
                        }
                        else
                            SoundManager.instance.PlaySoundClip("SFX_Error", SoundType.SFX);

                    }
                    else if (GameManager.Instance._coin < (400 + (200 * Sword_Level)))
                        SoundManager.instance.PlaySoundClip("SFX_Error", SoundType.SFX);
                    break;
            }
        }
    }
    #endregion

    public void Close() => StartCoroutine(BlackSmith.instnace.CloseWindow());

    public void OnPointerEnter(PointerEventData eventDatas)
    {
        if (mouseover_UI == MouseOver_UI.MouseOver)
            SoundManager.instance.PlaySoundClip("SFX_Button_Over", SoundType.SFX);
    }
}
