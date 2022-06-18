using System;

using UnityEngine;

[CreateAssetMenu(fileName = "FX_DATA", menuName = "FX/VFX_Library", order = 2)]
public class VFX_Library : ScriptableObject
{
    public VFX_Lib[] library;

    [Serializable]
    public class VFX_Lib
    {
        public FxManager.VFX_TYPE vfx;
        public GameObject prefab;
        public float time;
    }


    public bool GetFX(FxManager.VFX_TYPE fX, out VFX_Lib obj)
    {
        obj = null; 
        foreach(var lib in library)
        {
            if(lib.vfx == fX)
            {
                obj = lib;
                return true; 
            }
        }
        return false;
    }

}
