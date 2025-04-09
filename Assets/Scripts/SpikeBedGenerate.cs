using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBedGenerate : MonoBehaviour
{
    public int numOfSpikes;
    public GameObject spikePrefab;

    private int i;
    void Start()
    {
        for(i = 0; i < numOfSpikes; i++)
        {
            Instantiate(spikePrefab, new Vector2(transform.position.x + i, transform.position.y), transform.rotation);
        }
    }
}
