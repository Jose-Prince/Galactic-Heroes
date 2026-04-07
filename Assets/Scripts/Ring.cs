using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    [SerializeField] int ringID;
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

        RaceManager.Instance.SaveProgress();
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