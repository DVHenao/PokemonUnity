using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StatusUI : MonoBehaviour
{
    [SerializeField]
    public TMPro.TextMeshProUGUI NameText;

    [SerializeField]
    private Slider hpSlider;
    [SerializeField]
    private Slider mpSlider;
    [SerializeField]
    private TMPro.TextMeshProUGUI hpText;
    [SerializeField]
    private TMPro.TextMeshProUGUI mpText;

    public void SetName(string name)
    {
        NameText.text = name;
    }

    public void SetHpSlider(float hp, float maxHP)
    {
        hpSlider.value = hp/maxHP;
        SetHPText(hp, maxHP);
    }

    public void SetMpSlider(float mp, float maxMP)
    {
        mpSlider.value = mp / maxMP;
        SetMPText(mp, maxMP);
    }

    public void SetHPText(float hp, float maxHP)
    {
        hpText.text = hp.ToString() + " / " + maxHP.ToString();
    }

    public void SetMPText(float mp, float maxMP)
    {
        mpText.text = mp.ToString() + " / " + maxMP.ToString();
    }

}
