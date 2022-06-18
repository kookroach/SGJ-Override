using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SFX_Library;
using static VFX_Library;

public class FxManager : MonoBehaviour{


    private static FxManager _instance;
    public static FxManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject manager = GameObject.Find("GameManager");
                _instance = manager.GetComponent<FxManager>();

            }
            return _instance;
        }
    }


    public VFX_Library VFX_Library;
    public SFX_Library SFX_Library;

    public bool CreateVFX(GameObject @object, VFX_TYPE fx, bool playOnAwake = true)
    {
        VFX_Lib prefab;
       if (VFX_Library.GetFX(fx, out prefab))
            return false;

        GameObject vfxOBJ = new GameObject("VFX Container");
        vfxOBJ.transform.parent = @object.transform;

        VFX vfx = vfxOBJ.AddComponent<VFX>();
        if (playOnAwake)
        {
            vfx.playVFX();
        }
        return true;
    }

    public bool CreateSFX(GameObject @object, SFX_TYPE fx, bool playOnAwake = true)
    {
        SFX_Lib prefab;
        if (SFX_Library.GetFX(fx, out prefab))
            return false;

        GameObject sfxOBJ = new GameObject("Sound Container");
        SFX sfx = sfxOBJ.AddComponent<SFX>();
        if (playOnAwake)
        {
            sfx.PlaySound(prefab.clip, prefab.loop, prefab.spatial);
        }
        return true;
    }


    public enum VFX_TYPE
    {
        Explosion,
        Blood,
    }
    public enum SFX_TYPE
    {
        Clash,
        Move,
    }
}
