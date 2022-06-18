using System;

using UnityEngine;

[CreateAssetMenu(fileName = "FX_DATA", menuName = "FX/SFX_Library", order = 1)]
public class SFX_Library : ScriptableObject
{
    public SFX_Lib[] library;

    [Serializable]
    public class SFX_Lib
    {
        public FxManager.SFX_TYPE sfx;
        public AudioClip clip;
        public float time;
        public bool loop;
        public bool spatial;
    }

    public bool GetFX(FxManager.SFX_TYPE fX, out SFX_Lib obj)
    {
        obj = null;
        foreach (var lib in library)
        {
            if (lib.sfx == fX)
            {
                obj = lib;
                return true;
            }
        }
        return false;
    }

}
