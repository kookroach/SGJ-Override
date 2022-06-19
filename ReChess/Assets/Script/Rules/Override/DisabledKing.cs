using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class DisabledKing : King
{
    private void Awake()
    {
        Destroy(this);
    }
}
