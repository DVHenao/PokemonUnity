using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAnimationController : MonoBehaviour
{

    public Character Player;
    public Enemy Enemy;

    public Transform EffectTransform;

    // Start is called before the first frame update
    void Start()
    {
        Enemy = FindObjectOfType<Enemy>();
    }

    public void PlayEffectAnimation(Turn currentTurn, SkillType skillType)
    {
        Transform transformVal = null;
        GameObject selectedEffect = null;
        string soundname = "";
        switch (skillType)
        {
            case SkillType.ATTACK:
                transformVal = currentTurn == Turn.PLAYER ? Enemy.transform : Player.transform;
                break;
            case SkillType.HEAL:
                transformVal = currentTurn == Turn.PLAYER ? Player.transform : Enemy.transform;
                break;

        }
        selectedEffect = currentTurn == Turn.PLAYER ? Player.status.SelectedEffectPrefab : Enemy.status.SelectedEffectPrefab;
        soundname = currentTurn == Turn.PLAYER ? Player.status.SelectedSoundName : Enemy.status.SelectedSoundName;
        StartCoroutine(AnimationSequence(transformVal, selectedEffect, soundname));
    }

    IEnumerator AnimationSequence(Transform transform, GameObject effectPrefab, string soundName)
    {
        if (effectPrefab != null)
        {
            Vector2 position1 = new Vector2(transform.position.x - 0.3f, transform.position.y + 0.4f);
            Vector2 position2 = new Vector2(transform.position.x + 0.3f, transform.position.y + 0.4f);
            Vector2 position3 = new Vector2(transform.position.x, transform.position.y + 0.3f);

            var effectObj = Instantiate(effectPrefab, EffectTransform);
            effectObj.transform.position = position1;
            SoundManager.Instance.PlayFX(soundName, 0.5f);

            yield return new WaitForSeconds(0.1f);

            effectObj = Instantiate(effectPrefab, EffectTransform);
            effectObj.transform.position = position2;
            SoundManager.Instance.PlayFX(soundName, 0.5f);

            yield return new WaitForSeconds(0.1f);

            effectObj = Instantiate(effectPrefab, EffectTransform);
            effectObj.transform.position = position3;
            SoundManager.Instance.PlayFX(soundName, 0.5f);

        }
        yield return null;
    }
}
