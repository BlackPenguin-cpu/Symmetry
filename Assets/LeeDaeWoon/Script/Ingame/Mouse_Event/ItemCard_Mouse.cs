using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class ItemCard_Mouse : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public enum EDirection
    {
        Left,
        Among,
        Right
    }

    public EDirection eDirection;
    float timer = 0;

    [Header("확인")]
    int LeftClick_Check = 1;
    bool LeftCloseWindow_Check = true;

    int AmongClick_Check = 1;
    bool AmongCloseWindow_Check = true;

    int RightClick_Check = 1;
    bool RightCloseWindow_Check = true;

    [Header("빛")]
    public Image Left_Light;
    public Image Among_Light;
    public Image Right_Light;

    [Header("아이템 창")]
    public GameObject Left_Window;
    public GameObject Among_Window;
    public GameObject Right_Window;

    [Header("아이템 봉 / 배경")]
    //왼쪽
    public GameObject Left_Pole_01;
    public GameObject Left_Pole_02;
    public RectTransform Left_Rect;

    // 가운데
    public GameObject Among_Pole_01;
    public GameObject Among_Pole_02;
    public RectTransform Among_Rect;

    // 오른쪽
    public GameObject Right_Pole_01;
    public GameObject Right_Pole_02;
    public RectTransform Right_Rect;

    void Start()
    {
        Card_Manager.instance.isLeftPick = true;
        Card_Manager.instance.isAmongPick = true;
        Card_Manager.instance.isRightPick = true;
    }

    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        switch (eDirection)
        {
            case EDirection.Left:
                if (Card_Manager.instance.isAmongPick == true && Card_Manager.instance.isRightPick == true && Card_Manager.instance.isItemCardOpenCheck == false)
                {
                    SoundManager.instance.PlaySoundClip("SFX_Button_Over", SoundType.SFX);
                    Left_Light.DOFade(1f, 0.5f);
                }
                break;

            case EDirection.Among:
                if (Card_Manager.instance.isLeftPick == true && Card_Manager.instance.isRightPick == true && Card_Manager.instance.isItemCardOpenCheck == false)
                {
                    SoundManager.instance.PlaySoundClip("SFX_Button_Over", SoundType.SFX);
                    Among_Light.DOFade(1f, 0.5f);
                }
                break;

            case EDirection.Right:
                if (Card_Manager.instance.isLeftPick == true && Card_Manager.instance.isAmongPick == true && Card_Manager.instance.isItemCardOpenCheck == false)
                {
                    SoundManager.instance.PlaySoundClip("SFX_Button_Over", SoundType.SFX);
                    Right_Light.DOFade(1f, 0.5f);
                }
                break;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eDirection == EDirection.Left)
            Left_Light.DOFade(0f, 0.5f);

        if (eDirection == EDirection.Among)
            Among_Light.DOFade(0f, 0.5f);

        if (eDirection == EDirection.Right)
            Right_Light.DOFade(0f, 0.5f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 왼쪽 카드를 선택 했을 때
        if (eDirection == EDirection.Left)
        {

            Card_Manager.instance.fade.DOFade(0f, 0.5f);
            UI_Manager.instance.isCursorFade = false;

            if (Card_Manager.instance.isLeftPick == true && Card_Manager.instance.isItemCardOpenCheck == false)
            {
                Card_Manager.instance.isLeftPick = false;
                Card_Manager.instance.isRightPick = false;
                Card_Manager.instance.isAmongPick = false;

                // 방어구 및 장신구를 선택했을 때
                if (Card_Manager.instance.isDaLeft == false)
                {
                    for (int i = 0; i < Card_Manager.instance.daBuffer.Count; i++)
                    {
                        if (Card_Manager.instance.daBuffer[i].Itme_Name == Card_Manager.instance.itemDALeftCheck[0].Itme_Name)
                        {
                            Stop_Manager.Inst.ItemDA_Have.Add(Card_Manager.instance.daBuffer[i]);
                            Card_Manager.instance.daBuffer.RemoveAt(i);
                        }
                    }

                    switch (Card_Manager.instance.itemDALeftCheck[0].Itme_Name)
                    {
                        case "바람의 귀걸이":
                            Player.Instance.stat.PlayerDATypeList.WindEarRing = true;
                            Debug.Log(Player.Instance.stat.PlayerDATypeList.WindEarRing);
                            break;

                        case "가시견갑":
                            Player.Instance.stat.PlayerDATypeList.NeedleArmour = true;
                            Debug.Log(Player.Instance.stat.PlayerDATypeList.NeedleArmour);
                            break;

                        case "칼날망토":
                            Player.Instance.stat.PlayerDATypeList.KnifeCape = true;
                            Debug.Log(Player.Instance.stat.PlayerDATypeList.KnifeCape);
                            break;

                        case "저주받은 단검":
                            Player.Instance.stat.PlayerDATypeList.CurseKnife = true;
                            Debug.Log(Player.Instance.stat.PlayerDATypeList.CurseKnife);
                            break;

                        case "피의 장갑":
                            Player.Instance.stat.PlayerDATypeList.BloodGauntlet = true;
                            Debug.Log(Player.Instance.stat.PlayerDATypeList.BloodGauntlet);
                            break;

                        case "수정구":
                            Player.Instance.stat.PlayerDATypeList.CrystalOrb = true;
                            Debug.Log(Player.Instance.stat.PlayerDATypeList.CrystalOrb);
                            break;

                        case "절대반지":
                            Player.Instance.stat.PlayerDATypeList.TheOneRing = true;
                            Debug.Log(Player.Instance.stat.PlayerDATypeList.TheOneRing);
                            break;

                    }

                }

                // 마정석을 선택했을 때
                if (Card_Manager.instance.isDaLeft == true)
                {
                    switch (Card_Manager.instance.itemDALeftCheck[0].Itme_Name)
                    {
                        case "힘의 마정석":
                            Player.Instance.stat.Crystals[(int)CrystalsType.POWER]++;
                            break;

                        case "신속의 마정석":
                            Player.Instance.stat.Crystals[(int)CrystalsType.SPEED]++;
                            break;

                        case "연속의 마정석":
                            Player.Instance.stat.Crystals[(int)CrystalsType.ATTACKSPEED]++;
                            break;

                        case "체력의 마정석":
                            Player.Instance.stat.Crystals[(int)CrystalsType.HEALTH]++;
                            break;

                        case "시간의 마정석":
                            Player.Instance.stat.Crystals[(int)CrystalsType.TIME]++;
                            break;

                        case "방어의 마정석":
                            Player.Instance.stat.Crystals[(int)CrystalsType.DEFFENCE]++;
                            break;
                    }
                }


                if (Card_Manager.instance.isItemLeft == false)
                    Stop_Manager.Inst.ItemDA_Have.Add(Card_Manager.instance.itemDALeftCheck[0]);

                if (Card_Manager.instance.isItemBool == true)
                    Card_Manager.instance.isItemBool = false;

                else if (Card_Manager.instance.isItemBool == false)
                    Card_Manager.instance.itemCheck += LeftClick_Check;


                if (Card_Manager.instance.timeItemCount <= 2)
                {
                    Card_Manager.instance.timeItemLimit.Add(Card_Manager.instance.itemDALeftCheck[0]);
                    for (int i = 0; i < Card_Manager.instance.timeItemLimit.Count; i++)
                    {
                        if (Card_Manager.instance.timeItemLimit[i].Itme_Name.Contains(Card_Manager.instance.itemBuffer[4].Itme_Name))
                        {
                            Card_Manager.instance.timeItemCount++;
                            Card_Manager.instance.timeItemLimit.Clear();
                        }
                    }
                }

                Left_Light.DOFade(1f, 0.1f);
                Left_Window.transform.DOLocalMoveY(1100, 0.5f).SetEase(Ease.InQuad);
                StartCoroutine(Close_Dot());
            }
        }

        // 가운데 카드를 선택 했을 때
        if (eDirection == EDirection.Among)
        {
            Card_Manager.instance.fade.DOFade(0f, 0.5f);
            UI_Manager.instance.isCursorFade = false;

            if (Card_Manager.instance.isAmongPick == true && Card_Manager.instance.isItemCardOpenCheck == false)
            {
                Card_Manager.instance.isAmongPick = false;
                Card_Manager.instance.isLeftPick = false;
                Card_Manager.instance.isRightPick = false;

                // 방어구 및 장신구를 선택했을 때
                if (Card_Manager.instance.isDaAmong == false)
                {
                    for (int i = 0; i < Card_Manager.instance.daBuffer.Count; i++)
                    {
                        if (Card_Manager.instance.daBuffer[i].Itme_Name == Card_Manager.instance.itemDAAmongCheck[0].Itme_Name)
                        {
                            Stop_Manager.Inst.ItemDA_Have.Add(Card_Manager.instance.daBuffer[i]);
                            Card_Manager.instance.daBuffer.RemoveAt(i);
                        }
                    }

                    switch (Card_Manager.instance.itemDAAmongCheck[0].Itme_Name)
                    {
                        case "바람의 귀걸이":
                            Player.Instance.stat.PlayerDATypeList.WindEarRing = true;
                            Debug.Log(Player.Instance.stat.PlayerDATypeList.WindEarRing);
                            break;

                        case "가시견갑":
                            Player.Instance.stat.PlayerDATypeList.NeedleArmour = true;
                            Debug.Log(Player.Instance.stat.PlayerDATypeList.NeedleArmour);
                            break;

                        case "칼날망토":
                            Player.Instance.stat.PlayerDATypeList.KnifeCape = true;
                            Debug.Log(Player.Instance.stat.PlayerDATypeList.KnifeCape);
                            break;

                        case "저주받은 단검":
                            Player.Instance.stat.PlayerDATypeList.CurseKnife = true;
                            Debug.Log(Player.Instance.stat.PlayerDATypeList.CurseKnife);
                            break;

                        case "피의 장갑":
                            Player.Instance.stat.PlayerDATypeList.BloodGauntlet = true;
                            Debug.Log(Player.Instance.stat.PlayerDATypeList.BloodGauntlet);
                            break;

                        case "수정구":
                            Player.Instance.stat.PlayerDATypeList.CrystalOrb = true;
                            Debug.Log(Player.Instance.stat.PlayerDATypeList.CrystalOrb);
                            break;

                        case "절대반지":
                            Player.Instance.stat.PlayerDATypeList.TheOneRing = true;
                            Debug.Log(Player.Instance.stat.PlayerDATypeList.TheOneRing);
                            break;

                    }
                }

                // 마정석을 선택했을 때
                if (Card_Manager.instance.isDaAmong == true)
                {
                    switch (Card_Manager.instance.itemDAAmongCheck[0].Itme_Name)
                    {
                        case "힘의 마정석":
                            Player.Instance.stat.Crystals[(int)CrystalsType.POWER]++;
                            break;

                        case "신속의 마정석":
                            Player.Instance.stat.Crystals[(int)CrystalsType.SPEED]++;
                            break;

                        case "연속의 마정석":
                            Player.Instance.stat.Crystals[(int)CrystalsType.ATTACKSPEED]++;
                            break;

                        case "체력의 마정석":
                            Player.Instance.stat.Crystals[(int)CrystalsType.HEALTH]++;
                            break;

                        case "시간의 마정석":
                            Player.Instance.stat.Crystals[(int)CrystalsType.TIME]++;
                            break;

                        case "방어의 마정석":
                            Player.Instance.stat.Crystals[(int)CrystalsType.DEFFENCE]++;
                            break;
                    }
                }


                if (Card_Manager.instance.isItemAmong == false)
                    Stop_Manager.Inst.ItemDA_Have.Add(Card_Manager.instance.itemDAAmongCheck[0]);

                if (Card_Manager.instance.isItemBool == true)
                    Card_Manager.instance.isItemBool = false;

                else if (Card_Manager.instance.isItemBool == false)
                    Card_Manager.instance.itemCheck += AmongClick_Check;

                if (Card_Manager.instance.timeItemCount <= 2)
                {
                    Card_Manager.instance.timeItemLimit.Add(Card_Manager.instance.itemDAAmongCheck[0]);
                    for (int i = 0; i < Card_Manager.instance.timeItemLimit.Count; i++)
                    {
                        if (Card_Manager.instance.timeItemLimit[i].Itme_Name.Contains(Card_Manager.instance.itemBuffer[4].Itme_Name))
                        {
                            Card_Manager.instance.timeItemCount++;
                            Card_Manager.instance.timeItemLimit.Clear();
                        }
                    }
                }

                Among_Light.DOFade(1f, 0.1f);
                Among_Window.transform.DOLocalMoveY(1150, 0.5f).SetEase(Ease.InQuad);
                StartCoroutine(Close_Dot());
            }

        }

        // 오른쪽 카드를 선택 했을 때
        if (eDirection == EDirection.Right)
        {
            Card_Manager.instance.fade.DOFade(0f, 0.5f);
            UI_Manager.instance.isCursorFade = false;

            if (Card_Manager.instance.isRightPick == true && Card_Manager.instance.isItemCardOpenCheck == false)
            {
                Card_Manager.instance.isRightPick = false;
                Card_Manager.instance.isLeftPick = false;
                Card_Manager.instance.isAmongPick = false;

                // 방어구 및 장신구를 선택했을 때
                if (Card_Manager.instance.isDaRight == false)
                {
                    for (int i = 0; i < Card_Manager.instance.daBuffer.Count; i++)
                    {
                        if (Card_Manager.instance.daBuffer[i].Itme_Name == Card_Manager.instance.itemDARightCheck[0].Itme_Name)
                        {
                            Stop_Manager.Inst.ItemDA_Have.Add(Card_Manager.instance.daBuffer[i]);
                            Card_Manager.instance.daBuffer.RemoveAt(i);
                        }
                    }

                    switch (Card_Manager.instance.itemDARightCheck[0].Itme_Name)
                    {
                        case "바람의 귀걸이":
                            Player.Instance.stat.PlayerDATypeList.WindEarRing = true;
                            Debug.Log(Player.Instance.stat.PlayerDATypeList.WindEarRing);
                            break;

                        case "가시견갑":
                            Player.Instance.stat.PlayerDATypeList.NeedleArmour = true;
                            Debug.Log(Player.Instance.stat.PlayerDATypeList.NeedleArmour);
                            break;

                        case "칼날망토":
                            Player.Instance.stat.PlayerDATypeList.KnifeCape = true;
                            Debug.Log(Player.Instance.stat.PlayerDATypeList.KnifeCape);
                            break;

                        case "저주받은 단검":
                            Player.Instance.stat.PlayerDATypeList.CurseKnife = true;
                            Debug.Log(Player.Instance.stat.PlayerDATypeList.CurseKnife);
                            break;

                        case "피의 장갑":
                            Player.Instance.stat.PlayerDATypeList.BloodGauntlet = true;
                            Debug.Log(Player.Instance.stat.PlayerDATypeList.BloodGauntlet);
                            break;

                        case "수정구":
                            Player.Instance.stat.PlayerDATypeList.CrystalOrb = true;
                            Debug.Log(Player.Instance.stat.PlayerDATypeList.CrystalOrb);
                            break;

                        case "절대반지":
                            Player.Instance.stat.PlayerDATypeList.TheOneRing = true;
                            Debug.Log(Player.Instance.stat.PlayerDATypeList.TheOneRing);
                            break;

                    }
                }

                // 마정석을 선택했을 때
                if (Card_Manager.instance.isDaRight == true)
                {
                    switch (Card_Manager.instance.itemDARightCheck[0].Itme_Name)
                    {
                        case "힘의 마정석":
                            Player.Instance.stat.Crystals[(int)CrystalsType.POWER]++;
                            break;

                        case "신속의 마정석":
                            Player.Instance.stat.Crystals[(int)CrystalsType.SPEED]++;
                            break;

                        case "연속의 마정석":
                            Player.Instance.stat.Crystals[(int)CrystalsType.ATTACKSPEED]++;
                            break;

                        case "체력의 마정석":
                            Player.Instance.stat.Crystals[(int)CrystalsType.HEALTH]++;
                            break;

                        case "시간의 마정석":
                            Player.Instance.stat.Crystals[(int)CrystalsType.TIME]++;
                            break;

                        case "방어의 마정석":
                            Player.Instance.stat.Crystals[(int)CrystalsType.DEFFENCE]++;
                            break;
                    }
                }

                if (Card_Manager.instance.isItemRight == false)
                    Stop_Manager.Inst.ItemDA_Have.Add(Card_Manager.instance.itemDARightCheck[0]);

                if (Card_Manager.instance.isItemBool == true)
                    Card_Manager.instance.isItemBool = false;

                else if (Card_Manager.instance.isItemBool == false)
                    Card_Manager.instance.itemCheck += RightClick_Check;

                if (Card_Manager.instance.timeItemCount <= 2)
                {
                    Card_Manager.instance.timeItemLimit.Add(Card_Manager.instance.itemDARightCheck[0]);
                    for (int i = 0; i < Card_Manager.instance.timeItemLimit.Count; i++)
                    {
                        if (Card_Manager.instance.timeItemLimit[i].Itme_Name.Contains(Card_Manager.instance.itemBuffer[4].Itme_Name))
                        {
                            Card_Manager.instance.timeItemCount++;
                            Card_Manager.instance.timeItemLimit.Clear();
                        }
                    }
                }

                Right_Light.DOFade(1f, 0.1f);
                Right_Window.transform.DOLocalMoveY(1150, 0.5f).SetEase(Ease.InQuad);
                StartCoroutine(Close_Dot());
            }
        }
    }

    public IEnumerator Close_Dot()
    {
        SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX);

        timer = 0;
        if (eDirection == EDirection.Left)
        {
            Among_Pole_01.transform.DOLocalMoveY(-53f, 0.5f);
            Among_Pole_02.transform.DOLocalMoveY(-125f, 0.5f);

            Right_Pole_01.transform.DOLocalMoveY(-32f, 0.5f);
            Right_Pole_02.transform.DOLocalMoveY(-108f, 0.5f);

            while (timer < 1)
            {
                Among_Rect.sizeDelta = new Vector2(522.6044f, Mathf.Lerp(824.77f, -20, timer));
                Right_Rect.sizeDelta = new Vector2(522.6044f, Mathf.Lerp(824.77f, -20, timer));
                timer += Time.deltaTime * 3f;
                yield return null;
            }

            LeftCloseWindow_Check = false;
            if (LeftCloseWindow_Check == false)
            {
                yield return new WaitForSeconds(0.2f);
                DOTween.PauseAll();
                Destroy(GameObject.Find("Item_Window(Clone)"));
            }
            UI_Manager.instance.timerCheck = true;

            WaveManager.Instance.StartCoroutine(WaveManager.Instance.WaveProcessing(WaveManager.Instance.m_WaveNum - 1));

            if (WaveManager.Instance.m_WaveNum == 3 || WaveManager.Instance.m_WaveNum == 5)
                Potal.Inst.Potal_M();

            Skill_Manager.instance.Instantiate_SkillCheck = false;
        }

        if (eDirection == EDirection.Among)
        {
            Left_Pole_01.transform.DOLocalMoveY(50f, 0.5f);
            Left_Pole_02.transform.DOLocalMoveY(-26f, 0.5f);

            Right_Pole_01.transform.DOLocalMoveY(-32f, 0.5f);
            Right_Pole_02.transform.DOLocalMoveY(-108f, 0.5f);

            while (timer < 1)
            {
                Left_Rect.sizeDelta = new Vector2(522.6044f, Mathf.Lerp(824.77f, -20, timer));
                Right_Rect.sizeDelta = new Vector2(522.6044f, Mathf.Lerp(824.77f, -20, timer));
                timer += Time.deltaTime * 3f;
                yield return null;
            }

            AmongCloseWindow_Check = false;
            if (AmongCloseWindow_Check == false)
            {
                yield return new WaitForSeconds(0.2f);
                DOTween.PauseAll();
                Destroy(GameObject.Find("Item_Window(Clone)"));
            }
            UI_Manager.instance.timerCheck = true;

            WaveManager.Instance.StartCoroutine(WaveManager.Instance.WaveProcessing(WaveManager.Instance.m_WaveNum - 1));

            if (WaveManager.Instance.m_WaveNum == 3 || WaveManager.Instance.m_WaveNum == 5)
                Potal.Inst.Potal_M();

            Skill_Manager.instance.Instantiate_SkillCheck = false;
        }

        if (eDirection == EDirection.Right)
        {
            Left_Pole_01.transform.DOLocalMoveY(50f, 0.5f);
            Left_Pole_02.transform.DOLocalMoveY(-26f, 0.5f);

            Among_Pole_01.transform.DOLocalMoveY(-53f, 0.5f);
            Among_Pole_02.transform.DOLocalMoveY(-125f, 0.5f);

            while (timer < 1)
            {
                Left_Rect.sizeDelta = new Vector2(522.6044f, Mathf.Lerp(824.77f, -20, timer));
                Among_Rect.sizeDelta = new Vector2(522.6044f, Mathf.Lerp(824.77f, -20, timer));
                timer += Time.deltaTime * 3f;
                yield return null;
            }

            RightCloseWindow_Check = false;
            if (RightCloseWindow_Check == false)
            {
                yield return new WaitForSeconds(0.2f);
                DOTween.PauseAll();
                Destroy(GameObject.Find("Item_Window(Clone)"));
            }
            UI_Manager.instance.timerCheck = true;

            //TODO: 응애
            WaveManager.Instance.StartCoroutine(WaveManager.Instance.WaveProcessing(WaveManager.Instance.m_WaveNum - 1));

            if (WaveManager.Instance.m_WaveNum == 3 || WaveManager.Instance.m_WaveNum == 5)
                Potal.Inst.Potal_M();

            Skill_Manager.instance.Instantiate_SkillCheck = false;
        }
    }
}
