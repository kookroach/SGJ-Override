using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptSelector : MonoBehaviour
{
    public void pickFirst()
    {
        var b = GameManager.Instance.cardSelect == LayoutData.CardSelect.first;
    }
    public void pickSecond()
    {
        var b = GameManager.Instance.cardSelect == LayoutData.CardSelect.second;

    }
    public void pickThird()
    {
        var b = GameManager.Instance.cardSelect == LayoutData.CardSelect.third;

    }
}
