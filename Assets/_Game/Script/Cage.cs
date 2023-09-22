using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : Solid
{
    [SerializeField] Character character;
    public override void OnDespawn()
    {
        base.OnDespawn();
        FreeCharacter();
    }

    void FreeCharacter()
    {
        character.OnFree();
    }    
}
