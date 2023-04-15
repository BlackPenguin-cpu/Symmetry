using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class KingCollision : MonoBehaviour
{
    public enum Area
    {
        Area01,
        Area02,
        Area03,
        Area04,
    }
    public Area area;

    [Header("텍스트 연출")]
    public string scrambleChars_Tool;
    public bool richTextEnabled;
    public ScrambleMode scrambleMode;

    bool isRangeReach = false;
    bool isDialogueEnd = false;

    King king;

    const float waitTime = 0.5f;

    void Start()
    {
        king = King.instance;
        king.fBtn.gameObject.SetActive(false);
    }

    void Update()
    {
        DialogueBtn_FadeInOut();
        NextDialogue_F();

        if (king.isMagicCreation)
            Foundation.instance.magicCircle.DOFade(1, 0);
    }

    public void NextDialogue_F()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            king.fBtn.gameObject.SetActive(false);
            NextDialogue();
        }
    }

    public void DialogueBtn_FadeInOut()
    {
        if ((king.dialogueText.text == king.Dialogue[king.sequenceText] && !isDialogueEnd))
            king.fBtn.gameObject.SetActive(true);
    }

    void Dialogue(int num, int kingPos)
    {
        if (isRangeReach)
        {
            // 전문이 타이핑이 됬을 경우 && 대사가 7번 이하 나왔을 경우 && 대사가 아직 안 끝났을 경우
            if (king.dialogueText.text == king.Dialogue[king.sequenceText] && king.sequenceText <= num && !isDialogueEnd)
            {
                if (king.sequenceText < num)
                {
                    king.sequenceText++;
                    king.dialogueText.text = "";
                    king.dialogueText.DOKill();

                    king.dialogueText.DOText(king.Dialogue[king.sequenceText], 3, richTextEnabled = true, scrambleMode = ScrambleMode.None, scrambleChars_Tool = null).SetEase(Ease.Linear);
                }

                else if (king.sequenceText == num)
                {
                    isRangeReach = false;
                    isDialogueEnd = true;
                    king.isDialogueExit = true;
                    king.area01Box.enabled = false;
                    king.kingNPC.DOFade(0, 1).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        king.kingNPC.transform.DOLocalMoveX(kingPos, 1).SetEase(Ease.Linear).OnComplete(() =>
                        {
                            SoundManager.instance.PlaySoundClip("BGM_Main", SoundType.BGM);

                            king.sequenceText++;
                            king.Zoom_Shrinking();
                            king.dialogueText.text = "";
                            king.kingNPC.DOFade(1, 0).SetEase(Ease.Linear);
                        });
                    });
                }
            }

            else // 전문이 다 출력되기전 F키를 눌렀을 시
            {
                // 치고 있던 타이핑은 멈추고, 바로 전문이 완성되도록 한다.
                king.dialogueText.DOKill();
                king.dialogueText.DOText(king.Dialogue[king.sequenceText], 0, richTextEnabled = true, scrambleMode = ScrambleMode.None, scrambleChars_Tool = null).SetEase(Ease.Linear);
            }
        }
    }

    public void NextDialogue()
    {
        switch (area)
        {
            case Area.Area01:
                Dialogue(7, 15);
                break;

            case Area.Area02:
                Dialogue(12, 23);
                break;

            case Area.Area03:
                Dialogue(18, 32);
                break;

            case Area.Area04:
                if (isRangeReach)
                {

                    // 전문이 타이핑이 됬을 경우 && 대사가 7번 이하 나왔을 경우 && 대사가 아직 안 끝났을 경우
                    if (king.dialogueText.text == king.Dialogue[king.sequenceText] && king.sequenceText <= 23 && !isDialogueEnd)
                    {
                        if (king.sequenceText < 23)
                        {
                            king.sequenceText++;
                            king.dialogueText.text = "";
                            king.dialogueText.DOPause();
                            king.dialogueText.DOText(king.Dialogue[king.sequenceText], 3, richTextEnabled = true, scrambleMode = ScrambleMode.None, scrambleChars_Tool = null).SetEase(Ease.Linear);
                        }

                        else if (king.sequenceText == 23)
                        {
                            isRangeReach = false;
                            isDialogueEnd = true;
                            king.isDialogueExit = true;
                            king.area04Box.enabled = false;
                            king.kingNPC.DOFade(0, 1).SetEase(Ease.Linear).OnComplete(() =>
                            {
                                SoundManager.instance.PlaySoundClip("BGM_Main", SoundType.BGM);

                                king.isMagicCreation = true;
                                king.Zoom_Shrinking();
                                UIManager.instance.isKingCheck = true;
                            });
                        }
                    }

                    else // 전문이 다 출력되기전 F키를 눌렀을 시
                    {
                        // 치고 있던 타이핑은 멈추고, 바로 전문이 완성되도록 한다.
                        king.dialogueText.DOPause();
                        king.dialogueText.DOText(king.Dialogue[king.sequenceText], 0, richTextEnabled = true, scrambleMode = ScrambleMode.None, scrambleChars_Tool = null).SetEase(Ease.Linear);
                    }
                }
                break;
        }
    }

    void KingCamera(float cameraPos)
    {
        SoundManager.instance.PlaySoundClip("BGM_Tutorial", SoundType.BGM);

        isRangeReach = true;
        king.cameraObj.GetComponent<CameraManager>().enabled = false;
        king.cameraObj.transform.DOLocalMoveX(cameraPos, waitTime).SetEase(Ease.Linear).OnComplete(() =>
        {
            king.dialogueText.DOText(king.Dialogue[king.sequenceText], 3, richTextEnabled = true, scrambleMode = ScrambleMode.None, scrambleChars_Tool = null).SetEase(Ease.Linear);
        });
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.GetComponent<ITypePlayer>() != null && UIManager.instance.isKingCheck == false)
        {
            king.Zoom_Expansion(); // 줌을 확대시킨다.

            switch (area)
            {
                case Area.Area01:
                    KingCamera(0);
                    break;

                case Area.Area02:
                    KingCamera(9);
                    break;

                case Area.Area03:
                    KingCamera(17.82f);
                    break;

                case Area.Area04:
                    KingCamera(26.76f);
                    break;
            }
        }
    }
}
