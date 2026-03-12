using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SpriteAslatHelper : MonoBehaviour // sử dụng trong 1 scene
{
    public static SpriteAslatHelper Instance;
    public SpriteAtlas atlasSticker;
    public SpriteAtlas atlasSpriteMap;

    private void Awake()
    {
        Instance = this;
    }
    private void OnDisable()
    {
        Instance = null;    
    }
}
