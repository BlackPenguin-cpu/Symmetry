using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

[System.Serializable]


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
    public Image leftLight;
    public Image amongLight;
    public Image rightLight;

    const float lightTimer = 0.5f;

    [Header("창")]
    [SerializeField] GameObject leftWindow;
    [SerializeField] GameObject amongWindow;
    [SerializeField] GameObject rightWindow;
    [SerializeField] RectTransform leftRect;
    [SerializeField] RectTransform amongRect;
    [SerializeField] RectTransform rightRect;

    const int windowWidth = 545;
    const int windowHeight = 890;

    [Header("봉")]
    [SerializeField] GameObject leftBarUp;
    [SerializeField] GameObject leftBarDown;
    [SerializeField] GameObject amongBarUp;
    [SerializeField] GameObject amongBarDown;
    [SerializeField] GameObject rightBarUp;
    [SerializeField] GameObject rightBarDown;

    const int barOpen = 450;
    const int barClose = 40;
    const float barSpeed = 0.38f;

    void Start()
    {
        BarOpen();
        StartCoroutine(itemWindow());

        CardManager.instance.isLeftPick = true;
        CardManager.instance.isAmongPick = true;
        CardManager.instance.isRightPick = true;
    }

    void Update()
    {

    }

    void BarOpen()
    {
        leftBarUp.transform.DOLocalMoveY(barOpen, barSpeed).SetEase(Ease.Linear);
        leftBarDown.transform.DOLocalMoveY(-barOpen, barSpeed).SetEase(Ease.Linear);

        amongBarUp.transform.DOLocalMoveY(barOpen, barSpeed).SetEase(Ease.Linear);
        amongBarDown.transform.DOLocalMoveY(-barOpen, barSpeed).SetEase(Ease.Linear);

        rightBarUp.transform.DOLocalMoveY(barOpen, barSpeed).SetEase(Ease.Linear);
        rightBarDown.transform.DOLocalMoveY(-barOpen, barSpeed).SetEase(Ease.Linear);
    }

    void BarClose()
    {
        leftBarUp.transform.DOLocalMoveY(barClose, barSpeed).SetEase(Ease.Linear);
        leftBarDown.transform.DOLocalMoveY(-barClose, barSpeed).SetEase(Ease.Linear);

        amongBarUp.transform.DOLocalMoveY(barClose, barSpeed).SetEase(Ease.Linear);
        amongBarDown.transform.DOLocalMoveY(-barClose, barSpeed).SetEase(Ease.Linear);

        rightBarUp.transform.DOLocalMoveY(barClose, barSpeed).SetEase(Ease.Linear);
        rightBarDown.transform.DOLocalMoveY(-barClose, barSpeed).SetEase(Ease.Linear);
    }

    IEnumerator itemWindow()
    {
        CardManager.instance.fade.DOFade(0.5f, 0.5f);
        UI_Manager.instance.isCursorFade = true;
        CardManager.instance.isItemCardOpenCheck = true;

        while (timer < 1)
        {
            leftRect.sizeDelta = new Vector2(windowWidth, Mathf.Lerp(0, windowHeight, timer));
            amongRect.sizeDelta = new Vector2(windowWidth, Mathf.Lerp(0, windowHeight, timer));
            rightRect.sizeDelta = new Vector2(windowWidth, Mathf.Lerp(0, windowHeight, timer));

            timer += Time.deltaTime * 3f;
            yield return null;
        }
        CardManager.instance.isItemCardOpenCheck = false;
    }

    IEnumerator itemWindowClose()
    {
        while (timer < 1)
        {
            leftRect.sizeDelta = new Vector2(windowWidth, Mathf.Lerp(windowHeight, 0, timer));
            amongRect.sizeDelta = new Vector2(windowWidth, Mathf.Lerp(windowHeight, 0, timer));
            rightRect.sizeDelta = new Vector2(windowWidth, Mathf.Lerp(windowHeight, 0, timer));

            timer += Time.deltaTime * 3f;
            yield return null;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.instance.PlaySoundClip("SFX_Button_Over", SoundType.SFX);

        switch (eDirection)
        {
            case EDirection.Left:
                leftLight.DOFade(1f, lightTimer);
                break;

            case EDirection.Among:
                amongLight.DOFade(1f, lightTimer);
                break;

            case EDirection.Right:
                rightLight.DOFade(1f, lightTimer);
                break;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        switch (eDirection)
        {
            case EDirection.Left:
                leftLight.DOFade(0, lightTimer);
                break;
            case EDirection.Among:
                amongLight.DOFade(0, lightTimer);
                break;
            case EDirection.Right:
                rightLight.DOFade(0, lightTimer);
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch(eDirection)
        {
            case EDirection.Left:

                break;

            case EDirection.Among:
                break;

            case EDirection.Right:
                break;
        }

        //// 왼쪽 카드를 선택 했을 때
        //if (eDirection == EDirection.Left)
        //{
        //    CardManager.instance.fade.DOFade(0f, 0.5f);
        //    UI_Manager.instance.isCursorFade = false;

        //    if (CardManager.instance.isLeftPick == true && CardManager.instance.isItemCardOpenCheck == false)
        //    {
        //        CardManager.instance.isLeftPick = false;
        //        CardManager.instance.isRightPick = false;
        //        CardManager.instance.isAmongPick = false;

        //        // 방어구 및 장신구를 선택했을 때
        //        if (CardManager.instance.isDaLeft == false)
        //        {
        //            for (int i = 0; i < CardManager.instance.daBuffer.Count; i++)
        //            {
        //                if (CardManager.instance.daBuffer[i].Itme_Name == CardManager.instance.itemDALeftCheck[0].Itme_Name)
        //                {
        //                    StopManager.instnace.ItemDA_Have.Add(CardManager.instance.daBuffer[i]);
        //                    CardManager.instance.daBuffer.RemoveAt(i);
        //                }
        //            }

        //            switch (CardManager.instance.itemDALeftCheck[0].Itme_Name)
        //            {
        //                case "바람의 귀걸이":
        //                    Player.Instance.stat.PlayerDATypeList.WindEarRing = true;
        //                    Debug.Log(Player.Instance.stat.PlayerDATypeList.WindEarRing);
        //                    break;

        //                case "가시견갑":
        //                    Player.Instance.stat.PlayerDATypeList.NeedleArmour = true;
        //                    Debug.Log(Player.Instance.stat.PlayerDATypeList.NeedleArmour);
        //                    break;

        //                case "칼날망토":
        //                    Player.Instance.stat.PlayerDATypeList.KnifeCape = true;
        //                    Debug.Log(Player.Instance.stat.PlayerDATypeList.KnifeCape);
        //                    break;

        //                case "저주받은 단검":
        //                    Player.Instance.stat.PlayerDATypeList.CurseKnife = true;
        //                    Debug.Log(Player.Instance.stat.PlayerDATypeList.CurseKnife);
        //                    break;

        //                case "피의 장갑":
        //                    Player.Instance.stat.PlayerDATypeList.BloodGauntlet = true;
        //                    Debug.Log(Player.Instance.stat.PlayerDATypeList.BloodGauntlet);
        //                    break;

        //                case "수정구":
        //                    Player.Instance.stat.PlayerDATypeList.CrystalOrb = true;
        //                    Debug.Log(Player.Instance.stat.PlayerDATypeList.CrystalOrb);
        //                    break;

        //                case "절대반지":
        //                    Player.Instance.stat.PlayerDATypeList.TheOneRing = true;
        //                    Debug.Log(Player.Instance.stat.PlayerDATypeList.TheOneRing);
        //                    break;

        //            }

        //        }

        //        // 마정석을 선택했을 때
        //        if (CardManager.instance.isDaLeft == true)
        //        {
        //            switch (CardManager.instance.itemDALeftCheck[0].Itme_Name)
        //            {
        //                case "힘의 마정석":
        //                    Player.Instance.stat.Crystals[(int)CrystalsType.POWER]++;
        //                    break;

        //                case "신속의 마정석":
        //                    Player.Instance.stat.Crystals[(int)CrystalsType.SPEED]++;
        //                    break;

        //                case "연속의 마정석":
        //                    Player.Instance.stat.Crystals[(int)CrystalsType.ATTACKSPEED]++;
        //                    break;

        //                case "체력의 마정석":
        //                    Player.Instance.stat.Crystals[(int)CrystalsType.HEALTH]++;
        //                    break;

        //                case "시간의 마정석":
        //                    Player.Instance.stat.Crystals[(int)CrystalsType.TIME]++;
        //                    break;

        //                case "방어의 마정석":
        //                    Player.Instance.stat.Crystals[(int)CrystalsType.DEFFENCE]++;
        //                    break;
        //            }
        //        }


        //        if (CardManager.instance.isItemLeft == false)
        //            StopManager.instnace.ItemDA_Have.Add(CardManager.instance.itemDALeftCheck[0]);

        //        if (CardManager.instance.isItemBool == true)
        //            CardManager.instance.isItemBool = false;

        //        else if (CardManager.instance.isItemBool == false)
        //            CardManager.instance.itemCheck += LeftClick_Check;


        //        if (CardManager.instance.timeItemCount <= 2)
        //        {
        //            CardManager.instance.timeItemLimit.Add(CardManager.instance.itemDALeftCheck[0]);
        //            for (int i = 0; i < CardManager.instance.timeItemLimit.Count; i++)
        //            {
        //                if (CardManager.instance.timeItemLimit[i].Itme_Name.Contains(CardManager.instance.itemBuffer[4].Itme_Name))
        //                {
        //                    CardManager.instance.timeItemCount++;
        //                    CardManager.instance.timeItemLimit.Clear();
        //                }
        //            }
        //        }

        //        leftLight.DOFade(1f, 0.1f);
        //        leftWindow.transform.DOLocalMoveY(1100, 0.5f).SetEase(Ease.InQuad);
        //        StartCoroutine(Close_Dot());
        //    }
        //}

        //// 가운데 카드를 선택 했을 때
        //if (eDirection == EDirection.Among)
        //{
        //    CardManager.instance.fade.DOFade(0f, 0.5f);
        //    UI_Manager.instance.isCursorFade = false;

        //    if (CardManager.instance.isAmongPick == true && CardManager.instance.isItemCardOpenCheck == false)
        //    {
        //        CardManager.instance.isAmongPick = false;
        //        CardManager.instance.isLeftPick = false;
        //        CardManager.instance.isRightPick = false;

        //        // 방어구 및 장신구를 선택했을 때
        //        if (CardManager.instance.isDaAmong == false)
        //        {
        //            for (int i = 0; i < CardManager.instance.daBuffer.Count; i++)
        //            {
        //                if (CardManager.instance.daBuffer[i].Itme_Name == CardManager.instance.itemDAAmongCheck[0].Itme_Name)
        //                {
        //                    StopManager.instnace.ItemDA_Have.Add(CardManager.instance.daBuffer[i]);
        //                    CardManager.instance.daBuffer.RemoveAt(i);
        //                }
        //            }

        //            switch (CardManager.instance.itemDAAmongCheck[0].Itme_Name)
        //            {
        //                case "바람의 귀걸이":
        //                    Player.Instance.stat.PlayerDATypeList.WindEarRing = true;
        //                    Debug.Log(Player.Instance.stat.PlayerDATypeList.WindEarRing);
        //                    break;

        //                case "가시견갑":
        //                    Player.Instance.stat.PlayerDATypeList.NeedleArmour = true;
        //                    Debug.Log(Player.Instance.stat.PlayerDATypeList.NeedleArmour);
        //                    break;

        //                case "칼날망토":
        //                    Player.Instance.stat.PlayerDATypeList.KnifeCape = true;
        //                    Debug.Log(Player.Instance.stat.PlayerDATypeList.KnifeCape);
        //                    break;

        //                case "저주받은 단검":
        //                    Player.Instance.stat.PlayerDATypeList.CurseKnife = true;
        //                    Debug.Log(Player.Instance.stat.PlayerDATypeList.CurseKnife);
        //                    break;

        //                case "피의 장갑":
        //                    Player.Instance.stat.PlayerDATypeList.BloodGauntlet = true;
        //                    Debug.Log(Player.Instance.stat.PlayerDATypeList.BloodGauntlet);
        //                    break;

        //                case "수정구":
        //                    Player.Instance.stat.PlayerDATypeList.CrystalOrb = true;
        //                    Debug.Log(Player.Instance.stat.PlayerDATypeList.CrystalOrb);
        //                    break;

        //                case "절대반지":
        //                    Player.Instance.stat.PlayerDATypeList.TheOneRing = true;
        //                    Debug.Log(Player.Instance.stat.PlayerDATypeList.TheOneRing);
        //                    break;

        //            }
        //        }

        //        // 마정석을 선택했을 때
        //        if (CardManager.instance.isDaAmong == true)
        //        {
        //            switch (CardManager.instance.itemDAAmongCheck[0].Itme_Name)
        //            {
        //                case "힘의 마정석":
        //                    Player.Instance.stat.Crystals[(int)CrystalsType.POWER]++;
        //                    break;

        //                case "신속의 마정석":
        //                    Player.Instance.stat.Crystals[(int)CrystalsType.SPEED]++;
        //                    break;

        //                case "연속의 마정석":
        //                    Player.Instance.stat.Crystals[(int)CrystalsType.ATTACKSPEED]++;
        //                    break;

        //                case "체력의 마정석":
        //                    Player.Instance.stat.Crystals[(int)CrystalsType.HEALTH]++;
        //                    break;

        //                case "시간의 마정석":
        //                    Player.Instance.stat.Crystals[(int)CrystalsType.TIME]++;
        //                    break;

        //                case "방어의 마정석":
        //                    Player.Instance.stat.Crystals[(int)CrystalsType.DEFFENCE]++;
        //                    break;
        //            }
        //        }


        //        if (CardManager.instance.isItemAmong == false)
        //            StopManager.instnace.ItemDA_Have.Add(CardManager.instance.itemDAAmongCheck[0]);

        //        if (CardManager.instance.isItemBool == true)
        //            CardManager.instance.isItemBool = false;

        //        else if (CardManager.instance.isItemBool == false)
        //            CardManager.instance.itemCheck += AmongClick_Check;

        //        if (CardManager.instance.timeItemCount <= 2)
        //        {
        //            CardManager.instance.timeItemLimit.Add(CardManager.instance.itemDAAmongCheck[0]);
        //            for (int i = 0; i < CardManager.instance.timeItemLimit.Count; i++)
        //            {
        //                if (CardManager.instance.timeItemLimit[i].Itme_Name.Contains(CardManager.instance.itemBuffer[4].Itme_Name))
        //                {
        //                    CardManager.instance.timeItemCount++;
        //                    CardManager.instance.timeItemLimit.Clear();
        //                }
        //            }
        //        }

        //        amongLight.DOFade(1f, 0.1f);
        //        amongWindow.transform.DOLocalMoveY(1150, 0.5f).SetEase(Ease.InQuad);
        //        StartCoroutine(Close_Dot());
        //    }

        //}

        //// 오른쪽 카드를 선택 했을 때
        //if (eDirection == EDirection.Right)
        //{
        //    CardManager.instance.fade.DOFade(0f, 0.5f);
        //    UI_Manager.instance.isCursorFade = false;

        //    if (CardManager.instance.isRightPick == true && CardManager.instance.isItemCardOpenCheck == false)
        //    {
        //        CardManager.instance.isRightPick = false;
        //        CardManager.instance.isLeftPick = false;
        //        CardManager.instance.isAmongPick = false;

        //        // 방어구 및 장신구를 선택했을 때
        //        if (CardManager.instance.isDaRight == false)
        //        {
        //            for (int i = 0; i < CardManager.instance.daBuffer.Count; i++)
        //            {
        //                if (CardManager.instance.daBuffer[i].Itme_Name == CardManager.instance.itemDARightCheck[0].Itme_Name)
        //                {
        //                    StopManager.instnace.ItemDA_Have.Add(CardManager.instance.daBuffer[i]);
        //                    CardManager.instance.daBuffer.RemoveAt(i);
        //                }
        //            }

        //            switch (CardManager.instance.itemDARightCheck[0].Itme_Name)
        //            {
        //                case "바람의 귀걸이":
        //                    Player.Instance.stat.PlayerDATypeList.WindEarRing = true;
        //                    Debug.Log(Player.Instance.stat.PlayerDATypeList.WindEarRing);
        //                    break;

        //                case "가시견갑":
        //                    Player.Instance.stat.PlayerDATypeList.NeedleArmour = true;
        //                    Debug.Log(Player.Instance.stat.PlayerDATypeList.NeedleArmour);
        //                    break;

        //                case "칼날망토":
        //                    Player.Instance.stat.PlayerDATypeList.KnifeCape = true;
        //                    Debug.Log(Player.Instance.stat.PlayerDATypeList.KnifeCape);
        //                    break;

        //                case "저주받은 단검":
        //                    Player.Instance.stat.PlayerDATypeList.CurseKnife = true;
        //                    Debug.Log(Player.Instance.stat.PlayerDATypeList.CurseKnife);
        //                    break;

        //                case "피의 장갑":
        //                    Player.Instance.stat.PlayerDATypeList.BloodGauntlet = true;
        //                    Debug.Log(Player.Instance.stat.PlayerDATypeList.BloodGauntlet);
        //                    break;

        //                case "수정구":
        //                    Player.Instance.stat.PlayerDATypeList.CrystalOrb = true;
        //                    Debug.Log(Player.Instance.stat.PlayerDATypeList.CrystalOrb);
        //                    break;

        //                case "절대반지":
        //                    Player.Instance.stat.PlayerDATypeList.TheOneRing = true;
        //                    Debug.Log(Player.Instance.stat.PlayerDATypeList.TheOneRing);
        //                    break;

        //            }
        //        }

        //        // 마정석을 선택했을 때
        //        if (CardManager.instance.isDaRight == true)
        //        {
        //            switch (CardManager.instance.itemDARightCheck[0].Itme_Name)
        //            {
        //                case "힘의 마정석":
        //                    Player.Instance.stat.Crystals[(int)CrystalsType.POWER]++;
        //                    break;

        //                case "신속의 마정석":
        //                    Player.Instance.stat.Crystals[(int)CrystalsType.SPEED]++;
        //                    break;

        //                case "연속의 마정석":
        //                    Player.Instance.stat.Crystals[(int)CrystalsType.ATTACKSPEED]++;
        //                    break;

        //                case "체력의 마정석":
        //                    Player.Instance.stat.Crystals[(int)CrystalsType.HEALTH]++;
        //                    break;

        //                case "시간의 마정석":
        //                    Player.Instance.stat.Crystals[(int)CrystalsType.TIME]++;
        //                    break;

        //                case "방어의 마정석":
        //                    Player.Instance.stat.Crystals[(int)CrystalsType.DEFFENCE]++;
        //                    break;
        //            }
        //        }

        //        if (CardManager.instance.isItemRight == false)
        //            StopManager.instnace.ItemDA_Have.Add(CardManager.instance.itemDARightCheck[0]);

        //        if (CardManager.instance.isItemBool == true)
        //            CardManager.instance.isItemBool = false;

        //        else if (CardManager.instance.isItemBool == false)
        //            CardManager.instance.itemCheck += RightClick_Check;

        //        if (CardManager.instance.timeItemCount <= 2)
        //        {
        //            CardManager.instance.timeItemLimit.Add(CardManager.instance.itemDARightCheck[0]);
        //            for (int i = 0; i < CardManager.instance.timeItemLimit.Count; i++)
        //            {
        //                if (CardManager.instance.timeItemLimit[i].Itme_Name.Contains(CardManager.instance.itemBuffer[4].Itme_Name))
        //                {
        //                    CardManager.instance.timeItemCount++;
        //                    CardManager.instance.timeItemLimit.Clear();
        //                }
        //            }
        //        }

        //        rightLight.DOFade(1f, 0.1f);
        //        rightWindow.transform.DOLocalMoveY(1150, 0.5f).SetEase(Ease.InQuad);
        //        StartCoroutine(Close_Dot());
        //    }
        //}
    }

    public IEnumerator Close_Dot()
    {
        SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX);

        timer = 0;
        if (eDirection == EDirection.Left)
        {
            amongBarUp.transform.DOLocalMoveY(-53f, 0.5f);
            amongBarDown.transform.DOLocalMoveY(-125f, 0.5f);

            rightBarUp.transform.DOLocalMoveY(-32f, 0.5f);
            rightBarDown.transform.DOLocalMoveY(-108f, 0.5f);

            while (timer < 1)
            {
                amongRect.sizeDelta = new Vector2(522.6044f, Mathf.Lerp(824.77f, -20, timer));
                rightRect.sizeDelta = new Vector2(522.6044f, Mathf.Lerp(824.77f, -20, timer));
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

            WaveManager.instnace.StartCoroutine(WaveManager.instnace.WaveProcessing(WaveManager.instnace.m_WaveNum - 1));

            if (WaveManager.instnace.m_WaveNum == 3 || WaveManager.instnace.m_WaveNum == 5)
                Potal.Inst.Potal_M();

            Skill_Manager.instance.Instantiate_SkillCheck = false;
        }

        if (eDirection == EDirection.Among)
        {
            leftBarUp.transform.DOLocalMoveY(50f, 0.5f);
            leftBarDown.transform.DOLocalMoveY(-26f, 0.5f);

            rightBarUp.transform.DOLocalMoveY(-32f, 0.5f);
            rightBarDown.transform.DOLocalMoveY(-108f, 0.5f);

            while (timer < 1)
            {
                leftRect.sizeDelta = new Vector2(522.6044f, Mathf.Lerp(824.77f, -20, timer));
                rightRect.sizeDelta = new Vector2(522.6044f, Mathf.Lerp(824.77f, -20, timer));
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

            WaveManager.instnace.StartCoroutine(WaveManager.instnace.WaveProcessing(WaveManager.instnace.m_WaveNum - 1));

            if (WaveManager.instnace.m_WaveNum == 3 || WaveManager.instnace.m_WaveNum == 5)
                Potal.Inst.Potal_M();

            Skill_Manager.instance.Instantiate_SkillCheck = false;
        }

        if (eDirection == EDirection.Right)
        {
            leftBarUp.transform.DOLocalMoveY(50f, 0.5f);
            leftBarDown.transform.DOLocalMoveY(-26f, 0.5f);

            amongBarUp.transform.DOLocalMoveY(-53f, 0.5f);
            amongBarDown.transform.DOLocalMoveY(-125f, 0.5f);

            while (timer < 1)
            {
                leftRect.sizeDelta = new Vector2(522.6044f, Mathf.Lerp(824.77f, -20, timer));
                amongRect.sizeDelta = new Vector2(522.6044f, Mathf.Lerp(824.77f, -20, timer));
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
            WaveManager.instnace.StartCoroutine(WaveManager.instnace.WaveProcessing(WaveManager.instnace.m_WaveNum - 1));

            if (WaveManager.instnace.m_WaveNum == 3 || WaveManager.instnace.m_WaveNum == 5)
                Potal.Inst.Potal_M();

            Skill_Manager.instance.Instantiate_SkillCheck = false;
        }
    }
}
