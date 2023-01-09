using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMgr : MonoBehaviour
{
    [SerializeField] GameObject winBoard;
    [SerializeField] GameObject loseBoard;

    public void WinUI()
    {
        winBoard.SetActive(true);
    }

    public void LoseUI()
    {
        Debug.Log("lose UI");
        loseBoard.SetActive(true);
    }
}
