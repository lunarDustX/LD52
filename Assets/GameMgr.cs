using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMgr : MonoBehaviour
{
    UIMgr uiMgr;
    void Start()
    {
       uiMgr = FindObjectOfType<UIMgr>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void Lose()
    {
        Debug.Log("Lose");
        uiMgr.LoseUI();
    }

    public void Win()
    {
        uiMgr.WinUI();
    }
}
