using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{

    public void OnAnimationEnd()
    {
        Destroy(gameObject);
    }
}
