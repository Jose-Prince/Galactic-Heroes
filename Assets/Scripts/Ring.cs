using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    public bool passed = false;
    private SphereCollider sc;

    void Start()
    {
        sc = GetComponent<SphereCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        passed = true;
        GetComponentInChildren<MeshRenderer>().material.color = Color.green;
        sc.enabled = false;
        RaceManager.Instance.RingPassed(this);      
    }

    public void ResetRing()
    {
        passed = false;

        var renderer = GetComponentInChildren<MeshRenderer>();
        if (renderer)
        {
            renderer.material.color = Color.white;
        }
    }
}