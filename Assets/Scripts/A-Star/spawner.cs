using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class spawner : MonoBehaviour
{
    public GameObject target;
    public List<Unit> agents;

    public GameObject seekerPrefab;

    void Awake()
    {
        Unit[] _agents = FindObjectsOfType<Unit>();
        agents = new List<Unit>();

        for(int i = 0; i < _agents.Length; i++)
        {
            agents.Add(_agents[i]);
        }
    }

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject.name == "base")
                {
                    GameObject newSeeker = Instantiate(seekerPrefab, hit.point, Quaternion.identity) as GameObject;
                    Unit NSU = newSeeker.GetComponent<Unit>();
                    NSU.enabled = true;
                    agents.Add(NSU);
                }
            }
        }

        if (Input.GetMouseButtonDown(2))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.name == "base")
                {
                    target.transform.position = new Vector3(hit.point.x, 0, hit.point.z); ;
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.name == "base")
                {
                    target.transform.position = new Vector3(hit.point.x, 0, hit.point.z); ;
                    //resetTargets();
                }
            }
        }

    }

    void resetTargets()
    {
        foreach(Unit seeker in agents)
        {
            seeker.Start();
            //print("x");
        }
    }
}