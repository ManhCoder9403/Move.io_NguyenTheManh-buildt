using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Cache
{
    private static Dictionary<Collider, Characters> characters = new Dictionary<Collider, Characters>();

    public static Characters GetCharacter(this Collider collider)
    {
        if (!characters.ContainsKey(collider))
        {
            characters.Add(collider, collider.GetComponent<Characters>());
        }

        return characters[collider];
    }
}
