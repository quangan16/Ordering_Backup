using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level : MonoBehaviour
{
    public List<Solid> solidList ;
    public List<Hint> hintList  = new List<Hint>();
    public bool isWin => solidList.Count == 0;
    public float cameraDist = 10;
    private void Start()
    {
        solidList = GetComponentsInChildren<Solid>().ToList();
        Sort();
    }
    void Sort()
    {
        hintList.Sort(new HintSorter());
    }
    // Update is called once per frame
    public Vector3[] HintPosition()
    {
        Sort();

        return hintList[0].Hints();
    }
}
public class HintSorter : IComparer<Hint>
{
    public int Compare(Hint c1, Hint c2)
    {
        return c1.order.CompareTo(c2.order);
    }
}
[System.Serializable]
public class Hint
{
    public List<Solid> solidList = new List<Solid>();
    public int order;
    public Vector3[] Hints()
    {
        
        return solidList.Select(c=>c.transform.position).ToArray(); 

    }


}
