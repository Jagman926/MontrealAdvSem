using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InitialComboScript : MonoBehaviour
{
    Managers.GameManager gameManager;

    [SerializeField]
    private Button initial1;
    private TextMeshProUGUI initial1text;
    [SerializeField]
    private Button initial2;
    private TextMeshProUGUI initial2text;
    [SerializeField]
    private Button initial3;
    private TextMeshProUGUI initial3text;

    //Character list
    private char[] charList;

    //Button Selected
    private bool buttonSelected;
    private Button selectedButton;

    void Start()
    {
        gameManager = Managers.GameManager.Instance;
        //Get text component from each button
        initial1text = initial1.GetComponent<TextMeshProUGUI>();
        initial2text = initial2.GetComponent<TextMeshProUGUI>();
        initial3text = initial3.GetComponent<TextMeshProUGUI>();
        //Load last entered intials (if not auto uses 'AAA')
        LoadCurrentInitials();
        //Load char list
        charList = "ABCDEFGHIJKLMNOPQRSTUVWXYZ *?!#$&".ToCharArray();
    }

    void Update()
    {
        CurrentSelected();
    }

    private void LoadCurrentInitials()
    {
        initial1text.text = gameManager.currentInitials[0].ToString();
        initial2text.text = gameManager.currentInitials[1].ToString();
        initial3text.text = gameManager.currentInitials[2].ToString();
    }

    private void CurrentSelected()
    {
        if (initial1.GetComponent<SpriteRenderer>().sprite == initial1.spriteState.highlightedSprite)
        {
            Debug.Log("Test");
        }
    }

    public void UpdateSelectedButton(int buttonNum)
    {
        if (buttonNum == 1)
            selectedButton = initial1;
        if (buttonNum == 2)
            selectedButton = initial2;
        if (buttonNum == 3)
            selectedButton = initial3;
        buttonSelected = !buttonSelected;
    }

    private void AdjustButtonChar()
    {

    }
}
