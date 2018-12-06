using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InitialComboScript : MonoBehaviour
{
    Managers.GameManager gameManager;

    [Header("Initial 1")]
    [SerializeField]
    private GameObject initial1;
    private Text initial1_text;
    private int initial1CharNum;
    [Header("Initial 2")]
    [SerializeField]
    private GameObject initial2;
    private Text initial2_text;
    private int initial2CharNum;
    [Header("Initial 3")]
    [SerializeField]
    private GameObject initial3;
    private Text initial3_text;
    private int initial3CharNum;

    //Character list
    private char[] charList;
    private string charString = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    void Start()
    {
        gameManager = Managers.GameManager.Instance;
        //Set intials to AAA
        GetCurrentInitials();
        //Load text fields from gameobjects
        initial1_text = initial1.GetComponent<Text>();
        initial2_text = initial2.GetComponent<Text>();
        initial3_text = initial3.GetComponent<Text>();
        //Load char list
        charList = charString.ToCharArray();
        //Update display
        UpdateCharDisplay();
    }

    private void UpdateCharDisplay()
    {
        initial1_text.text = charList[initial1CharNum].ToString();
        initial2_text.text = charList[initial2CharNum].ToString();
        initial3_text.text = charList[initial3CharNum].ToString();
    }

    private void GetCurrentInitials()
    {
        initial1CharNum = gameManager.currentInitials[0];
        initial2CharNum = gameManager.currentInitials[1];
        initial3CharNum = gameManager.currentInitials[2];
    }

    public void AdjustInitial1(bool up)
    {
        if (up)
        {
            initial1CharNum++;
            //If last char, set to front
            if (initial1CharNum >= charList.Length)
                initial1CharNum = 0;
        }
        else
        {
            initial1CharNum--;
            //If first char, set to back
            if (initial1CharNum < 0)
                initial1CharNum = (charList.Length - 1);
        }
        UpdateCharDisplay();
    }

    public void AdjustInitial2(bool up)
    {
        if (up)
        {
            initial2CharNum++;
            //If last char, set to front
            if (initial2CharNum >= charList.Length)
                initial2CharNum = 0;
        }
        else
        {
            initial2CharNum--;
            //If first char, set to back
            if (initial2CharNum < 0)
                initial2CharNum = (charList.Length - 1);
        }
        UpdateCharDisplay();
    }

    public void AdjustInitial3(bool up)
    {
        if (up)
        {
            initial3CharNum++;
            //If last char, set to front
            if (initial3CharNum >= charList.Length)
                initial3CharNum = 0;
        }
        else
        {
            initial3CharNum--;
            //If first char, set to back
            if (initial3CharNum < 0)
                initial3CharNum = (charList.Length - 1);
        }
        UpdateCharDisplay();
    }

    public void SetCurrentInitials()
    {
        gameManager.SetCurrentInitials(initial1CharNum, initial2CharNum, initial3CharNum);
    }
}
