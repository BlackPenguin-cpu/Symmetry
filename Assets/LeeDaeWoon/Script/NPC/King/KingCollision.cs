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

    bool rangeReach;
    public bool Dialogue_End;

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

        if (king.isMagicCreation == true)
            Foundation.instance.MagicCircle_Rotation();
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
        if ((king.dialogueText.text == king.Dialogue[king.sequenceText] && Dialogue_End == false))
            king.fBtn.gameObject.SetActive(true);
    }

    public void NextDialogue()
    {
        switch (area)
        {
            case Area.Area01:
                {
                    if (rangeReach == true)
                    {
                        // 전문이 타이핑이 됬을 경우 && 대사가 7번 이하 나왔을 경우 && 대사가 아직 안 끝났을 경우
                        if (king.dialogueText.text == king.Dialogue[king.sequenceText] && king.sequenceText <= 7 && !Dialogue_End)
                        {
                            if (king.sequenceText < 7)
                            {
                                king.sequenceText++;
                                king.dialogueText.text = "";
                                king.dialogueText.DOKill();

                                king.dialogueText.DOText(king.Dialogue[king.sequenceText], 3, richTextEnabled = true, scrambleMode = ScrambleMode.None, scrambleChars_Tool = null).SetEase(Ease.Linear);
                            }

                            else if (king.sequenceText == 7)
                            {
                                rangeReach = false;
                                Dialogue_End = true;
                                king.isDialogueExit = true;
                                king.area01Box.enabled = false;
                                king.kingNPC.DOFade(0, 1).SetEase(Ease.Linear).OnComplete(() =>
                                {
                                    king.kingNPC.transform.DOLocalMoveX(15, 1).SetEase(Ease.Linear).OnComplete(() =>
                                    {
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
                break;

            case Area.Area02:
                if (rangeReach == true)
                {
                    // 전문이 타이핑이 됬을 경우 && 대사가 7번 이하 나왔을 경우 && 대사가 아직 안 끝났을 경우
                    if (king.dialogueText.text == king.Dialogue[king.sequenceText] && king.sequenceText <= 12 && Dialogue_End == false)
                    {

                        if (king.sequenceText < 12)
                        {
                            king.sequenceText++;
                            king.dialogueText.text = "";
                            king.dialogueText.DOKill();
                            king.dialogueText.DOText(king.Dialogue[king.sequenceText], 3, richTextEnabled = true, scrambleMode = ScrambleMode.None, scrambleChars_Tool = null).SetEase(Ease.Linear);
                        }

                        else if (king.sequenceText == 12)
                        {
                            rangeReach = false;
                            Dialogue_End = true;
                            king.isDialogueExit = true;
                            king.area02Box.enabled = false;
                            king.kingNPC.DOFade(0, 1).SetEase(Ease.Linear).OnComplete(() =>
                            {
                                king.kingNPC.transform.DOLocalMoveX(23, 1).SetEase(Ease.Linear).OnComplete(() =>
                                {
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
                break;

            case Area.Area03:
                if (rangeReach == true)
                {
                    // 전문이 타이핑이 됬을 경우 && 대사가 7번 이하 나왔을 경우 && 대사가 아직 안 끝났을 경우
                    if (king.dialogueText.text == king.Dialogue[king.sequenceText] && king.sequenceText <= 18 && Dialogue_End == false)
                    {
                        if (king.sequenceText < 18)
                        {
                            king.sequenceText++;
                            king.dialogueText.text = "";
                            king.dialogueText.DOKill();

                            king.dialogueText.DOText(king.Dialogue[king.sequenceText], 3, richTextEnabled = true, scrambleMode = ScrambleMode.None, scrambleChars_Tool = null).SetEase(Ease.Linear);
                        }

                        else if (king.sequenceText == 18)
                        {
                            rangeReach = false;
                            Dialogue_End = true;
                            king.isDialogueExit = true;
                            king.area03Box.enabled = false;
                            king.kingNPC.DOFade(0, 1).SetEase(Ease.Linear).OnComplete(() =>
                            {
                                king.kingNPC.transform.DOLocalMoveX(32, 1).SetEase(Ease.Linear).OnComplete(() =>
                                {
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
                break;

            case Area.Area04:
                if (rangeReach == true)
                {

                    // 전문이 타이핑이 됬을 경우 && 대사가 7번 이하 나왔을 경우 && 대사가 아직 안 끝났을 경우
                    if (king.dialogueText.text == king.Dialogue[king.sequenceText] && king.sequenceText <= 23 && Dialogue_End == false)
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
                            rangeReach = false;
                            Dialogue_End = true;
                            king.isDialogueExit = true;
                            king.area04Box.enabled = false;
                            king.kingNPC.DOFade(0, 1).SetEase(Ease.Linear).OnComplete(() =>
                            {
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


    public IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.GetComponent<ITypePlayer>() != null && UIManager.instance.isKingCheck == false)
        {
            king.Zoom_Expansion(); // 카메라 확대 시킨다.

            switch (area)
            {
                case Area.Area01:
                    rangeReach = true;
                    king.cameraObj.GetComponent<CameraManager>().enabled = false;
                    king.cameraObj.transform.DOLocalMoveX(0, waitTime).SetEase(Ease.Linear);

                    yield return new WaitForSeconds(waitTime);

                    king.dialogueText.DOText(king.Dialogue[king.sequenceText], 3, richTextEnabled = true, scrambleMode = ScrambleMode.None, scrambleChars_Tool = null).SetEase(Ease.Linear);
                    break;

                case Area.Area02:
                    rangeReach = true;
                    king.cameraObj.GetComponent<CameraManager>().enabled = false;
                    king.cameraObj.transform.DOLocalMoveX(9, waitTime).SetEase(Ease.Linear);

                    yield return new WaitForSeconds(waitTime);

                    king.dialogueText.DOText(king.Dialogue[king.sequenceText], 3, richTextEnabled = true, scrambleMode = ScrambleMode.None, scrambleChars_Tool = null).SetEase(Ease.Linear);
                    break;

                case Area.Area03:
                    rangeReach = true;
                    king.cameraObj.GetComponent<CameraManager>().enabled = false;
                    king.cameraObj.transform.DOLocalMoveX(17.82f, waitTime).SetEase(Ease.Linear);

                    yield return new WaitForSeconds(waitTime);

                    king.dialogueText.DOText(king.Dialogue[king.sequenceText], 3f, richTextEnabled = true, scrambleMode = ScrambleMode.None, scrambleChars_Tool = null).SetEase(Ease.Linear);
                    break;

                case Area.Area04:
                    rangeReach = true;
                    king.cameraObj.GetComponent<CameraManager>().enabled = false;
                    king.cameraObj.transform.DOLocalMoveX(26.76f, waitTime).SetEase(Ease.Linear);

                    yield return new WaitForSeconds(waitTime);

                    king.dialogueText.DOText(king.Dialogue[king.sequenceText], 3f, richTextEnabled = true, scrambleMode = ScrambleMode.None, scrambleChars_Tool = null).SetEase(Ease.Linear);
                    break;
            }
        }
    }
}
