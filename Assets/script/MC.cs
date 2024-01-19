using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MC : MonoBehaviour
{
    NavMeshAgent _mcAgent;
    // Start is called before the first frame update
    void Awake()
    {
        _mcAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            SetDestination();
        }

        
    }

    void SetDestination ()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            _mcAgent.destination = hit.point;
        }
    }
}
