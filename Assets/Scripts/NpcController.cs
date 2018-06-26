using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NpcController : MonoBehaviour 
{
	private Astar _pathfinding;

    void Awake()
	{
		_pathfinding = new Astar ();
	}

    int currentIndex = 0;
    Node[] AstarPath;

    void Update()
    {
        if (AstarPath == null || currentIndex == AstarPath.Length)
            return;
        float dist = Vector3.Distance(AstarPath[currentIndex].transform.position, transform.position);
        if (dist >= 1)
        {
            transform.LookAt(AstarPath[currentIndex].transform.position);
            transform.position += transform.forward * 2 * Time.deltaTime;
        }
        else
            currentIndex++;
    }

    public void MoveTo(Vector3 endPos)
	{
        currentIndex = 0;
        AstarPath = null;
        AstarPath = _pathfinding.SearchPath (FindNearNode (transform.position),
		                                   FindNearNode (endPos));

	}

	private Node FindNearNode(Vector3 pos)
	{
        var nod = Physics.OverlapSphere(pos, 2);
        var nodos = new List<Node>();
        foreach (var item in nod)
            if (item.GetComponent<Node>())
                nodos.Add(item.GetComponent<Node>());

        float dist = Mathf.Infinity;
        Node closest = null;
        foreach (var item in nodos)
        {
            var ds = Vector3.Distance(pos, item.transform.position);
            if(ds < dist)
            {
                dist = ds;
                closest = item;
            }
        }
		return closest;
	}
}
