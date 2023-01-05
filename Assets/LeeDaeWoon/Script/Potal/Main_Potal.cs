using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Main_Potal : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ITypePlayer>() != null)
        {
            DOTween.KillAll();
            SceneManager.LoadScene("test");
        }
    }
}
