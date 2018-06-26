using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour 
{
	public Node padre;
	public List<Node> vecinos;
    public List<Node> vecinosEnVista;
    public float index;
	public float G;
	public float H;

    public float F;

    public bool isStart;
    public bool isEnd;
    public bool isCamino;

    public bool isBlocked;

	void OnDrawGizmos()
	{
        Gizmos.color = Color.green;
        if (isCamino)
            Gizmos.DrawSphere(transform.position, 1);

        Gizmos.color = Color.blue;
        if (isStart)
            Gizmos.DrawSphere(transform.position, 1);

        Gizmos.color = Color.blue;
        if (isEnd)
            Gizmos.DrawSphere(transform.position, 1);

        Gizmos.color = Color.red;
        if (isBlocked)
            Gizmos.DrawSphere(transform.position, 1);

        /*Gizmos.color = Color.white;
        foreach (var item in vecinos)
            if(item)
                Gizmos.DrawLine(transform.position, item.transform.position);
                */

        Gizmos.color = Color.white;
        foreach (var item in vecinosEnVista)
            if(item)
                Gizmos.DrawLine(transform.position, item.transform.position);
	}

    public void SetH(Node end)
    {
        var x = end.transform.position.x - transform.position.x;
        var z = end.transform.position.z - transform.position.z;

        x = x < 0 ? -x : x;
        z = z < 0 ? -z : z;

        H = x + z;
    }
    public void ReComputePath()
    {
        if (isEnd)
        {
            padre = null;
            return;
        }
        foreach (var vecino in vecinosEnVista)
            if (vecino.index < padre.index)
                padre = vecino;
    }

}
