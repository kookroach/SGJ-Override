using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannibalKing : King
{
    private Boolean hasEaten = false;


    public override bool OnAttack(GameObject other)
    {
        if (other.CompareTag(this.gameObject.tag) && hasEaten)
            return false;


        Component component;
        if (other.CompareTag(this.gameObject.tag) && !hasEaten && !other.TryGetComponent(typeof(Pawn), out component))
        {
            if (other.TryGetComponent(typeof(IRule), out component))
            {
                Destroy(this.GetComponent<King>());
                String classString = component.name;
                switch (other.GetComponent<IRule>())
                {
                    case Rook:
                        Rook rookKing = gameObject.AddComponent(typeof(Rook)) as Rook;
                        Destroy(other);
                        return true;

                    case Knight:
                        Knight knightKing = gameObject.AddComponent(typeof(Knight)) as Knight;
                        Destroy(other);
                        return true;


                    case Bishop:
                        Bishop bishopKing = gameObject.AddComponent(typeof(Bishop)) as Bishop;
                        Destroy(other);
                        return true;


                    case Queen:
                        Queen queenKing = gameObject.AddComponent(typeof(Queen)) as Queen;
                        Destroy(other);
                        return true;


                    default:
                        return false;
                }
            }
        }

        if (other.GetComponent<IRule>().OnDestroy() && !other.CompareTag(this.gameObject.tag))
        {
            Destroy(other);
            return true;
        }

        return false;
    }
}