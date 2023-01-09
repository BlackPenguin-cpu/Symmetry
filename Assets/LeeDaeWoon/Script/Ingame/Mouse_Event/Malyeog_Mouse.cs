using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class Malyeog_Mouse : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public enum Malyeog
    {
        Magic,
        Body
    }

    [Header("마력 강화")]
    [SerializeField] Malyeog malyeog;
    public int malyeogNum = 0; // 마력 강화 순서
    public int malyeogUpgrade = 0; // 마력 강화 업그레이드
    public Text malyeogUpgradeText; // 마력 강화 텍스트

    [Header("스킬 잠금해제")]
    public bool isBodyOpenCheck = false;
    public bool isMagicOpenCheck = false;

    Foundation foundation;

    void Start()
    {
        foundation = Foundation.instance;
    }

    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.instance.PlaySoundClip("SFX_Button_Over", SoundType.SFX);

        foundation.purchase.transform.GetChild(0).gameObject.SetActive(true);
        foundation.purchase.transform.GetChild(1).gameObject.SetActive(false);
        foundation.title.text = foundation.malyeog[malyeogNum].name;
        foundation.explanation.text = foundation.malyeog[malyeogNum].upgradeExplanation[malyeogUpgrade];


        if (malyeogUpgrade == foundation.malyeog[malyeogNum].upgradeExplanation.Count - 1)
            foundation.dimensionalPrice.text = "Max";
        else
        {
            switch (malyeogNum)
            {
                case 0:
                    malyeogUpgradeText.text = malyeogUpgrade + "/4";
                    foundation.dimensionalPrice.text = (600 + malyeogUpgrade * 150).ToString();
                    break;

                case 1:
                    malyeogUpgradeText.text = malyeogUpgrade + "/4";
                    foundation.dimensionalPrice.text = (1000 + malyeogUpgrade * 200).ToString();
                    break;

                case 2:
                    malyeogUpgradeText.text = malyeogUpgrade + "/4";
                    foundation.dimensionalPrice.text = (1500 + malyeogUpgrade * 250).ToString();
                    break;

                case 3:
                    malyeogUpgradeText.text = malyeogUpgrade + "/2";
                    foundation.dimensionalPrice.text = (3000 + malyeogUpgrade * 3000).ToString();
                    break;

                case 4:
                    malyeogUpgradeText.text = malyeogUpgrade + "/4";
                    foundation.dimensionalPrice.text = (700 + malyeogUpgrade * 150).ToString();
                    break;

                case 5:
                    malyeogUpgradeText.text = malyeogUpgrade + "/4";
                    foundation.dimensionalPrice.text = (1100 + malyeogUpgrade * 200).ToString();
                    break;

                case 6:
                    malyeogUpgradeText.text = malyeogUpgrade + "/4";
                    foundation.dimensionalPrice.text = (1700 + malyeogUpgrade * 250).ToString();
                    break;

                case 7:
                    malyeogUpgradeText.text = malyeogUpgrade + "/2";
                    foundation.dimensionalPrice.text = (3300 + malyeogUpgrade * 3000).ToString();
                    break;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        foundation.purchase.transform.GetChild(0).gameObject.SetActive(false);
        foundation.purchase.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        switch (malyeogNum)
        {
            case 4:
                if (GameManager.Instance.crystal >= (700 + malyeogUpgrade * 150) && foundation.dimensionalPrice.text != "Max")
                {
                    SoundManager.instance.PlaySoundClip("SFX_Enforce", SoundType.SFX);

                    malyeogUpgrade++;
                    ++Player.Instance.stat.magicPower.invisibleHand;

                    GameManager.Instance.crystal -= (700 + malyeogUpgrade * 150);
                }
                else
                    SoundManager.instance.PlaySoundClip("SFX_Error", SoundType.SFX);
                break;

            case 5:
                if (GameManager.Instance.crystal >= (1100 + malyeogUpgrade * 200) && foundation.dimensionalPrice.text != "Max" && foundation.Magic_Open >= malyeogNum)
                {
                    SoundManager.instance.PlaySoundClip("SFX_Enforce", SoundType.SFX);

                    malyeogUpgrade++;
                    ++Player.Instance.stat.magicPower.sharpEye;

                    GameManager.Instance.crystal -= (1100 + malyeogUpgrade * 200);
                }
                else
                    SoundManager.instance.PlaySoundClip("SFX_Error", SoundType.SFX);
                break;

            case 6:
                if (GameManager.Instance.crystal >= (1700 + malyeogUpgrade * 250) && foundation.dimensionalPrice.text != "Max" && foundation.Magic_Open >= malyeogNum)
                {
                    SoundManager.instance.PlaySoundClip("SFX_Enforce", SoundType.SFX);

                    malyeogUpgrade++;
                    ++Player.Instance.stat.magicPower.timeQuick;

                    GameManager.Instance.crystal -= (1700 + malyeogUpgrade * 250);
                }
                else
                    SoundManager.instance.PlaySoundClip("SFX_Error", SoundType.SFX);
                break;

            case 7:
                if (GameManager.Instance.crystal >= (3300 + malyeogUpgrade * 3000) && foundation.dimensionalPrice.text != "Max" && foundation.Magic_Open >= malyeogNum)
                {
                    SoundManager.instance.PlaySoundClip("SFX_Enforce", SoundType.SFX);

                    malyeogUpgrade++;
                    ++Player.Instance.stat.magicPower.thaumcraft;

                    GameManager.Instance.crystal -= (3300 + malyeogUpgrade * 3000);
                }
                else
                    SoundManager.instance.PlaySoundClip("SFX_Error", SoundType.SFX);
                break;
        }

        switch (malyeogNum)
        {
            case 0:
                if (GameManager.Instance.crystal >= (600 + malyeogUpgrade * 150) && foundation.dimensionalPrice.text != "Max")
                {
                    SoundManager.instance.PlaySoundClip("SFX_Enforce", SoundType.SFX);

                    malyeogUpgrade++;
                    ++Player.Instance.stat.magicPower.silpidLeap;

                    GameManager.Instance.crystal -= (600 + malyeogUpgrade * 150);
                }
                else
                    SoundManager.instance.PlaySoundClip("SFX_Error", SoundType.SFX);
                break;

            case 1:
                if (GameManager.Instance.crystal >= (1000 + malyeogUpgrade * 200) && foundation.dimensionalPrice.text != "Max" && foundation.Body_Open >= malyeogNum)
                {
                    SoundManager.instance.PlaySoundClip("SFX_Enforce", SoundType.SFX);

                    malyeogUpgrade++;
                    ++Player.Instance.stat.magicPower.giantPower;

                    GameManager.Instance.crystal -= (1000 + malyeogUpgrade * 200);
                }
                else
                    SoundManager.instance.PlaySoundClip("SFX_Error", SoundType.SFX);
                break;

            case 2:
                if (GameManager.Instance.crystal >= (1500 + malyeogUpgrade * 250) && foundation.dimensionalPrice.text != "Max" && foundation.Body_Open >= malyeogNum)
                {
                    SoundManager.instance.PlaySoundClip("SFX_Enforce", SoundType.SFX);

                    GameManager.Instance.crystal -= (1500 + malyeogUpgrade * 250);

                    malyeogUpgrade++;
                    malyeogUpgradeText.text = malyeogUpgrade + "/4";
                    Player.Instance.stat.magicPower.ironSkin++;
                    switch (malyeogUpgrade)
                    {
                        case 1:
                            foundation.dimensionalPrice.text = (1500 + malyeogUpgrade * 250).ToString();
                            foundation.explanation.text = "강철같은 피부를 얻어 방어력이 5 / " + "<color=#877D78>" + "10" + "</color>" + " / " + "<color=#877D78>" + "15" + "</color>" + " / " + "<color=#877D78>" + "20" + "</color>" + " 상승한다.";
                            break;

                        case 2:
                            foundation.dimensionalPrice.text = (1500 + malyeogUpgrade * 250).ToString();
                            foundation.explanation.text = "강철같은 피부를 얻어 방어력이 " + "<color=#877D78>" + "5" + "</color>" + " / " + "10 / " + "<color=#877D78>" + "15" + "</color>" + " / " + "<color=#877D78>" + "20" + "</color>" + " 상승한다.";
                            break;

                        case 3:
                            foundation.dimensionalPrice.text = (1500 + malyeogUpgrade * 250).ToString();
                            foundation.explanation.text = "강철같은 피부를 얻어 방어력이 " + "<color=#877D78>" + "5" + "</color>" + " / " + "<color=#877D78>" + "10" + "</color>" + " / " + "15 / " + "<color=#877D78>" + "20" + "</color>" + " 상승한다.";
                            break;

                        case 4:
                            foundation.dimensionalPrice.text = "Max";
                            foundation.explanation.text = "강철같은 피부를 얻어 방어력이 " + "<color=#877D78>" + "5" + "</color>" + " / " + "<color=#877D78>" + "10" + "</color>" + " / " + "<color=#877D78>" + "15" + "</color>" + " / " + "20" + " 상승한다.";
                            break;
                    }

                    isBodyOpenCheck = true;
                    if (isBodyOpenCheck == true && foundation.Body_Open == malyeogNum)
                        foundation.Body_Open++;
                }
                else
                    SoundManager.instance.PlaySoundClip("SFX_Error", SoundType.SFX);
                break;

            case 3:
                if (GameManager.Instance.crystal >= (3000 + malyeogUpgrade * 3000) && foundation.dimensionalPrice.text != "Max" && foundation.Body_Open >= malyeogNum)
                {
                    SoundManager.instance.PlaySoundClip("SFX_Enforce", SoundType.SFX);

                    GameManager.Instance.crystal -= (3000 + malyeogUpgrade * 3000);

                    malyeogUpgrade++;
                    malyeogUpgradeText.text = malyeogUpgrade + "/2";
                    switch (malyeogUpgrade)
                    {
                        case 1:
                            Player.Instance.stat.magicPower.magicHeart++;
                            foundation.dimensionalPrice.text = (3000 + malyeogUpgrade * 3000).ToString();
                            foundation.explanation.text = "마정석 심장이 두번째 기회를 줘 최대체력의 20% / " + "<color=#877D78>" + "50%" + "</color>" + " 상승한다.";
                            break;

                        case 2:
                            Player.Instance.stat.magicPower.magicHeart++;
                            foundation.dimensionalPrice.text = "Max";
                            foundation.explanation.text = "마정석 심장이 두번째 기회를 줘 최대체력의 " + "<color=#877D78>" + "20%" + "</color>" + " / " + "50%" + " 상승한다.";
                            break;
                    }
                }
                else
                    SoundManager.instance.PlaySoundClip("SFX_Error", SoundType.SFX);
                break;
        }
    }

}
