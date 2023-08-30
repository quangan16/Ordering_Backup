using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cache : MonoBehaviour
{
    // Start is called before the first frame update
    public static Dictionary<Collider2D,Solid> colliders = new Dictionary<Collider2D, Solid>();
    public static Solid GetSolid(Collider2D collider2D)
    {
        if(!colliders.ContainsKey(collider2D))
        {
            colliders.Add(collider2D,collider2D.GetComponent<Solid>());
        }
        return colliders[collider2D];


    }
}
