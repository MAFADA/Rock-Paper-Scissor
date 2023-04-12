using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [SerializeField] Character selectedCharacter;
    [SerializeField] List<Character> characterList;
    [SerializeField] Transform atkReference;
    public Character SelectedCharacter { get => selectedCharacter; }
    public List<Character> CharacterList { get => characterList; }

    public void Prepare()
    {
        selectedCharacter = null;
    }

    public void SelectCharacter(Character character)
    {
        selectedCharacter = character;
    }

    public void SetPlay(bool value)
    {
        foreach (var character in characterList)
        {
            character.Button.interactable = value;
        }
    }

    public void Attack()
    {
        selectedCharacter.transform.DOMove(atkReference.position, 0.5f);

    }

    public bool IsAttacking()
    {
        if (selectedCharacter == null)
        {
            return false;
        }
        return DOTween.IsTweening(selectedCharacter.transform);
    }

    public void TakeDamage(int damageValue)
    {
        selectedCharacter.ChangeHP(-damageValue);
        var spriteRend = selectedCharacter.GetComponent<SpriteRenderer>();
        spriteRend.DOColor(Color.red, 0.1f).SetLoops(6, LoopType.Yoyo);
    }

    public bool IsDamaging()
    {
        if (selectedCharacter == null)
        {
            return false;
        }

        var spriteRend = selectedCharacter.GetComponent<SpriteRenderer>();
        return DOTween.IsTweening(spriteRend);
    }

    public void Remove(Character character)
    {
        if (characterList.Contains(character) == false)
        {
            return;
        }

        if (selectedCharacter == character)
        {
            selectedCharacter = null;
        }

        character.Button.interactable = false;
        character.gameObject.SetActive(value: false);
        characterList.Remove(character);
    }

    public void Return()
    {
        selectedCharacter.transform.DOMove(selectedCharacter.InitialPosition, 0.5f);
    }

    public bool IsReturning()
    {
        if (selectedCharacter == null)
        {
            return false;
        }

        return DOTween.IsTweening(selectedCharacter.transform);
    }
}
