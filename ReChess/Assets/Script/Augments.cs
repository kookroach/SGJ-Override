using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Augments : MonoBehaviour
{
    [SerializeField] private Pawn pawn1;
    [SerializeField] private Pawn pawn2;
    [SerializeField] private Pawn pawn3;

    [SerializeField] private GameObject button1;
    [SerializeField] private GameObject button2;
    [SerializeField] private GameObject button3;


    public void SelectClass(Int32 index)
    {
        switch (index)
        {
            case 1:
                switchPawn();
                break;
                
            default:
                return;
        }
    }

    private void switchPawn()
    {
        button1.SetActive(true);
        button2.SetActive(true);
        button3.SetActive(true);
        
        button1.GetComponentInChildren<TextMeshProUGUI>().text = pawn1.ToString();
        button2.GetComponentInChildren<TextMeshProUGUI>().text = pawn2.ToString();
        button3.GetComponentInChildren<TextMeshProUGUI>().text = pawn3.ToString();



    }

    private void Start()
    {
        button1.SetActive(false);
        button2.SetActive(false);
        button3.SetActive(false);

        
    }

   
}
