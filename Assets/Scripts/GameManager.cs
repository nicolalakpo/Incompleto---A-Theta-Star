using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
	public NpcController npc;
    public GameObject node;

	void Start()
	{
        GenerateNodes(2, 20, 20);
	}

	void Update()
	{
		if (Input.GetMouseButtonDown (0)) 
		{
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000))
                npc.MoveTo(hit.point);

        }
	}

	private void GenerateNodes(float distBetweenNodes, int rowQty, int colQty)
	{
        float xBase = 10;
        float zBase = 10;
        int num = 0;
        for (int i = 0; i < colQty; i++)
        {
            for (int j = 0; j < rowQty; j++)
            {
                var nod = Instantiate(node);
                nod.transform.position = new Vector3(xBase + j * distBetweenNodes, 1, zBase);
                nod.name = num + "";
                num++;
            }
            zBase += distBetweenNodes;
        }

        var nodos = FindObjectsOfType<Node>();
        foreach (var nodo in nodos)
            foreach (var vecino in Physics.OverlapSphere(nodo.transform.position, 3))
                if (vecino.GetComponent<Node>() != nodo && vecino.GetComponent<Node>())
                    nodo.vecinos.Add(vecino.GetComponent<Node>());
    }

}
