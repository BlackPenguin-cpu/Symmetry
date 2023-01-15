using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCollision : MonoBehaviour
{
    public int distanceNum;

    void Start()
    {

    }

    void Update()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Player") || collision.GetComponent<ITypePlayer>() != null ) && !SkillWindow.instance.isPurchase)
        {
            SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);
            SkillWindow.instance.isCollisionCheck = true;

            SkillWindow.instance.SkillNum = distanceNum;
            Skill_List.instance.Skill_Num(distanceNum);
            SkillWindow.instance.OpenWindow(distanceNum);
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.CompareTag("Player") || collision.GetComponent<ITypePlayer>() != null))
        {
            SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);
            SkillWindow.instance.isCollisionCheck = false;

            SkillWindow.instance.CloseWindow(distanceNum);
        }
    }

}
