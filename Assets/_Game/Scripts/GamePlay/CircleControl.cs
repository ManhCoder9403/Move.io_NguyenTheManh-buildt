using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleControl : MonoBehaviour
{
    [SerializeField] private Characters _character;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ((int)LayerType.Character))
        {
            _character.FoundTarget(other.GetCharacter());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == ((int)LayerType.Character))
        {
            _character.LostTarget(other.GetCharacter());
        }
    }
}
