using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StainsController : MonoBehaviour
{
    public int MaxStainsCount = 10000;
    private void Start()
    {
        StartCoroutine(CheckLimit());
    }
    public void AddStain(GameObject stain)
    {
        stain.transform.SetParent(transform, true);
    }

    IEnumerator CheckLimit()
    {
        while (true)
        {
            while (transform.childCount > MaxStainsCount)
            {
                Destroy(transform.GetChild(0).gameObject);
                yield return null;
            }
            yield return null;
        }
    }
}
