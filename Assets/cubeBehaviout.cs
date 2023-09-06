using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeBehaviout : MonoBehaviour
{
    public Transform target; 
    private Collider _collider;
    void Start()
    {
        _collider = GetComponent<Collider>();
        _collider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position;
    }
}
