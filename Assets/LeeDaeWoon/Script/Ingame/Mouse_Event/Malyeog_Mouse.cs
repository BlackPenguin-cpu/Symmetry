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
        foundation.explanation.text = foundation.malyeog[malyeogNum].upgradeExplanation[malyeogUpgrade + 1];

        if (malyeogUpgrade == foundation.malyeog[malyeogNum].upgradeExplanation.Count - 1)
            foundation.dimensionalPrice.text = "Max";
        else
        {
            switch (malyeogNum)
            {
                case 0:

                    if (GameManager.Instance.crystal >= (600 + malyeogUpgrade * 150) && foundation.dimensionalPrice.text != "Max")
                    {
                        Debug.Log("asdfasdf");
                        SoundManager.instance.PlaySoundClip("SFX_Enforce", SoundType.SFX);

                        malyeogUpgrade++;
                        ++Player.Instance.stat.magicPower.silpidLeap;

                        malyeogUpgradeText.text = malyeogUpgrade + "/4";
                        foundation.dimensionalPrice.text = (600 + malyeogUpgrade * 150).ToString();

                        GameManager.Instance.crystal -= (600 + malyeogUpgrade * 150);
                    }
                    else
                        SoundManager.instance.PlaySoundClip("SFX_Error", SoundType.SFX);
                    break;

                case 1:
                    if (GameManager.Instance.crystal >= (1000 + malyeogUpgrade * 200) && foundation.dimensionalPrice.text != "Max")
                    {
                        SoundManager.instance.PlaySoundClip("SFX_Enforce", SoundType.SFX);

                        malyeogUpgrade++;
                        ++Player.Instance.stat.magicPower.giantPower;

                        malyeogUpgradeText.text = malyeogUpgrade + "/4";
                        foundation.dimensionalPrice.text = (1000 + malyeogUpgrade * 200).ToString();

                        GameManager.Instance.crystal -= (1000 + malyeogUpgrade * 200);
                    }
                    else
                        SoundManager.instance.PlaySoundClip("SFX_Error", SoundType.SFX);
                    break;

                case 2:
                    if (GameManager.Instance.crystal >= (1500 + malyeogUpgrade * 250) && foundation.dimensionalPrice.text != "Max")
                    {
                        SoundManager.instance.PlaySoundClip("SFX_Enforce", SoundType.SFX);

                        malyeogUpgrade++;
                        ++Player.Instance.stat.magicPower.ironSkin;

                        malyeogUpgradeText.text = malyeogUpgrade + "/4";
                        foundation.dimensionalPrice.text = (1500 + malyeogUpgrade * 250).ToString();

                        GameManager.Instance.crystal -= (1500 + malyeogUpgrade * 250);
                    }
                    else
                        SoundManager.instance.PlaySoundClip("SFX_Error", SoundType.SFX);
                    break;

                case 3:
                    if (GameManager.Instance.crystal >= (3000 + malyeogUpgrade * 3000) && foundation.dimensionalPrice.text != "Max")
                    {
                        SoundManager.instance.PlaySoundClip("SFX_Enforce", SoundType.SFX);

                        malyeogUpgrade++;
                        ++Player.Instance.stat.magicPower.magicHeart;

                        malyeogUpgradeText.text = malyeogUpgrade + "/2";
                        foundation.dimensionalPrice.text = (3000 + malyeogUpgrade * 3000).ToString();

                        GameManager.Instance.crystal -= (3000 + malyeogUpgrade * 3000);
                    }
                    else
                        SoundManager.instance.PlaySoundClip("SFX_Error", SoundType.SFX);
                    break;

                case 4:
                    if (GameManager.Instance.crystal >= (700 + malyeogUpgrade * 150) && foundation.dimensionalPrice.text != "Max")
                    {
                        SoundManager.instance.PlaySoundClip("SFX_Enforce", SoundType.SFX);

                        malyeogUpgrade++;
                        ++Player.Instance.stat.magicPower.invisibleHand;

                        malyeogUpgradeText.text = malyeogUpgrade + "/4";
                        foundation.dimensionalPrice.text = (700 + malyeogUpgrade * 150).ToString();

                        GameManager.Instance.crystal -= (700 + malyeogUpgrade * 150);
                    }
                    else
                        SoundManager.instance.PlaySoundClip("SFX_Error", SoundType.SFX);
                    break;

                case 5:
                    if (GameManager.Instance.crystal >= (1100 + malyeogUpgrade * 200) && foundation.dimensionalPrice.text != "Max")
                    {
                        SoundManager.instance.PlaySoundClip("SFX_Enforce", SoundType.SFX);

                        malyeogUpgrade++;
                        ++Player.Instance.stat.magicPower.sharpEye;

                        malyeogUpgradeText.text = malyeogUpgrade + "/4";
                        foundation.dimensionalPrice.text = (1100 + malyeogUpgrade * 200).ToString();

                        GameManager.Instance.crystal -= (1100 + malyeogUpgrade * 200);
                    }
                    else
                        SoundManager.instance.PlaySoundClip("SFX_Error", SoundType.SFX);
                    break;

                case 6:
                    if (GameManager.Instance.crystal >= (1700 + malyeogUpgrade * 250) && foundation.dimensionalPrice.text != "Max")
                    {
                        SoundManager.instance.PlaySoundClip("SFX_Enforce", SoundType.SFX);

                        malyeogUpgrade++;
                        ++Player.Instance.stat.magicPower.timeQuick;

                        malyeogUpgradeText.text = malyeogUpgrade + "/4";
                        foundation.dimensionalPrice.text = (1700 + malyeogUpgrade * 250).ToString();

                        GameManager.Instance.crystal -= (1700 + malyeogUpgrade * 250);
                    }
                    else
                        SoundManager.instance.PlaySoundClip("SFX_Error", SoundType.SFX);
                    break;

                case 7:
                    if (GameManager.Instance.crystal >= (3300 + malyeogUpgrade * 3000) && foundation.dimensionalPrice.text != "Max")
                    {
                        SoundManager.instance.PlaySoundClip("SFX_Enforce", SoundType.SFX);

                        malyeogUpgrade++;
                        ++Player.Instance.stat.magicPower.thaumcraft;

                        malyeogUpgradeText.text = malyeogUpgrade + "/2";
                        foundation.dimensionalPrice.text = (3300 + malyeogUpgrade * 3000).ToString();

                        GameManager.Instance.crystal -= (3300 + malyeogUpgrade * 3000);
                    }
                    else
                        SoundManager.instance.PlaySoundClip("SFX_Error", SoundType.SFX);
                    break;
            }
        }
    }


}
