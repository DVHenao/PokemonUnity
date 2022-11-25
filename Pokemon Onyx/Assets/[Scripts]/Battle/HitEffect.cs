using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    // Start is called before the first frame update




    public void OnAnimationEnd()
    {
        Destroy(gameObject);
    }
}
