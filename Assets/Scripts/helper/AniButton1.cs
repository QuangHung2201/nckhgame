using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniButton1 : MonoBehaviour
{
    // Start is called before the first frame update
 
    void Start()
    {
        aniUpDown(); 
    }

    public void aniUpDown()
    {
        Sequence seqUpDown = DOTween.Sequence();
        seqUpDown.Append(transform.DOScale(1.2f, 0.25f))
        .Append(transform.DOScale(1f, 0.25f))
        .AppendInterval(3f)
        .SetLoops(-1);
    }    
}
