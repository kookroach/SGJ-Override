using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece : MonoBehaviour
{
    public virtual PieceMovement PieceMovement { get; set; }
    public IEnumerator WaitForDeath(GameObject other)
    {
        yield return new WaitForSeconds(3);
        Destroy(other);
    }
}
