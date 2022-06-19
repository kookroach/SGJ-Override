using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Moves : MonoBehaviour
{
    [SerializeField]private GameObject _textMeshPro;
    void Update()
    {
        transform.LookAt(Camera.main.transform);


        _textMeshPro.GetComponent<TextMeshProUGUI>().text =
            GetComponentInParent<WearDownBishop>().forwardMovement.ToString();
    }
}
