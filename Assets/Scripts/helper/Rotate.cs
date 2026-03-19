using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        rotate();
    }

    public void rotate()
    {
        transform.DORotate(
            new Vector3(0,0,360)
            ,2f
            ,RotateMode.WorldAxisAdd
            ).SetLoops(-1);
    }    
}
