using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillButtonUI : MonoBehaviour
{

    [SerializeField]
    private TMPro.TextMeshProUGUI skillNameText;
    [SerializeField]
    private TMPro.TextMeshProUGUI costText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetSkillNameText(string text)
    {
        skillNameText.text = text;
    }

    public void SetCostText(string text)
    {
        costText.text = text;
    }
}
