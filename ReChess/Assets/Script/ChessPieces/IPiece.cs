using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPiece
{   
    public abstract IRule rule { get; }
    public abstract GameObject model { get; }
}
