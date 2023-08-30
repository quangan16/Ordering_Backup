using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static MultiRigidbodySync;


public class MultiRigidbodySync : MonoBehaviour
{
    [System.Serializable]
    public class ObjectPair
    {
        public ColorC objectA;
        public ColorC objectB;
    }
    public List<ColorC> colorCs => FindObjectsOfType<ColorC>().ToList();

    public static List<ObjectPair> objectPairs = new List<ObjectPair>();

    private static float[] rotationDifferences;

    void Start()
    {
        for(int i = 0;i< colorCs.Count-1;i++)
        {
            ObjectPair objectPair = new ObjectPair();
            objectPair.objectA = colorCs[i];
            objectPair.objectB = colorCs[i + 1];
            objectPairs.Add(objectPair);
        }
        ObjectPair objectPair2 = new ObjectPair();
        objectPair2.objectA = colorCs[colorCs.Count - 1];
        objectPair2.objectB = colorCs[0];
        objectPairs.Add(objectPair2);

        rotationDifferences = new float[objectPairs.Count];

        for (int i = 0; i < objectPairs.Count; i++)
        {
            rotationDifferences[i] = objectPairs[i].objectB.rb.rotation - objectPairs[i].objectA.rb.rotation;
        }
    }


    public static void Synchronize()
    {
        for (int i = 0; i < objectPairs.Count; i++)
        {
            ObjectPair pair = objectPairs[i];
            if (pair.objectA.rb.bodyType == RigidbodyType2D.Dynamic && pair.objectB.rb.bodyType == RigidbodyType2D.Dynamic)
            {
                // Synchronize rotation from A to B
                float targetRotationB = pair.objectA.rb.rotation + rotationDifferences[i];
                pair.objectB.rb.MoveRotation(targetRotationB);

                // Synchronize rotation from B to A
                float targetRotationA = pair.objectB.rb.rotation - rotationDifferences[i];
                pair.objectA.rb.MoveRotation(targetRotationA);
            }
        }
    }
}

