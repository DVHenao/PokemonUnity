using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButtonUI : MonoBehaviour
{
    public enum ButtonType
    {
        ATTACK,
        HEAL,
    }


    [SerializeField]
    private TMPro.TextMeshProUGUI skillNameText;
    [SerializeField]
    private TMPro.TextMeshProUGUI costText;

    public Sprite blue;
    public Sprite red;

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

    public void SetButtonType(ButtonType type)
    {
        switch(type)
        {
            case ButtonType.ATTACK:
                GetComponent<Image>().sprite = red;
                break;
            case ButtonType.HEAL:
                GetComponent<Image>().sprite = blue;
                break;
        }
    }
}
