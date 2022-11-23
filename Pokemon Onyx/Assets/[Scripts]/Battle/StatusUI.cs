using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StatusUI : MonoBehaviour
{

    public TMPro.TextMeshProUGUI NameText { get; }

    [SerializeField]
    private Slider hpSlider;
    [SerializeField]
    private Slider mpSlider;
    [SerializeField]
    private TMPro.TextMeshProUGUI hpText;
    [SerializeField]
    private TMPro.TextMeshProUGUI mpText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetName(string name)
    {
        NameText.text = name;
    }

    public void SetHpSlider(float hp, float maxHP)
    {
        hpSlider.value = hp/maxHP;
    }

    public void SetMpSlider(float mp, float maxMP)
    {
        hpSlider.value = mp / maxMP;
    }

    public void SetHPText(float hp, float maxHP)
    {
        hpText.text = hp.ToString() + " / " + maxHP.ToString();
    }

    public void SetMPText(float mp, float maxMP)
    {
        hpText.text = mp.ToString() + " / " + maxMP.ToString();
    }

}
