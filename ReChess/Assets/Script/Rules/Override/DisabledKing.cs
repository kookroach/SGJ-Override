using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class DisabledKing : King
{
    private void Awake()
    {
        Destroy(this);
    }
}
