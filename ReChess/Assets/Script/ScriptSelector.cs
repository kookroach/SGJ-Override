using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScriptSelector : MonoBehaviour
{
    public void pickFirst()
    {
        GameManager.Instance.cardSelect = LayoutData.CardSelect.first;
        GameManager.Instance.PlayerMoves = GameManager.Instance.layoutData.playerMoves.Where(x => x.card == LayoutData.CardSelect.first).ToList();

    }
    public void pickSecond()
    {
        GameManager.Instance.cardSelect = LayoutData.CardSelect.second;
        GameManager.Instance.PlayerMoves = GameManager.Instance.layoutData.playerMoves.Where(x => x.card == LayoutData.CardSelect.second).ToList();

    }
    public void pickThird()
    {
        GameManager.Instance.cardSelect = LayoutData.CardSelect.third;
        GameManager.Instance.PlayerMoves = GameManager.Instance.layoutData.playerMoves.Where(x => x.card == LayoutData.CardSelect.third).ToList();

    }
}
