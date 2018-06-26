using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Astar
{
	private List<Node> _openSet;
	private List<Node> _closedSet;
    public Node CurrentNode;

	public Astar()
	{
		_openSet = new List<Node> ();
		_closedSet = new List<Node>();
	}

	public Node[] SearchPath(Node start, Node end)
	{
      /*  var allNoditos = GameObject.FindObjectsOfType<Node>();
        foreach (var item in allNoditos)
        {
            item.isStart = false;
            item.isEnd = false;
            item.isCamino = false;
            item.index = Mathf.Infinity;
        }
        start.isStart = true;
        end.isEnd = true;*/

        _closedSet.Clear();//1
        _openSet.Clear();//1
        _openSet.Add(start);//2
        CurrentNode = start;//3
        
        while (_openSet != null && _openSet.Count > 0)//4
        {
            CurrentNode = BuscarFMasBajo();//5
            if (CurrentNode = end)//6
            {
                return ThetaStar(ReconstruirCamino(start, CurrentNode));//7
                //ReconstruirCamino(start, CurrentNode);
            }
            foreach (var v in CurrentNode.vecinos)//8
            {
                if(v.isBlocked)//9
                {
                    _closedSet.Add(v);
                    continue;//10
                }
                var NDist = (Vector3.Distance(CurrentNode.transform.position, v.transform.position)) + CurrentNode.G;
                //Si la lista abierta TIENE A V
                if (_openSet.Contains(v))
                {
                    if (v.G <= NDist)
                    {
                        continue;
                    }
                }
                //Si la lista cerrada tiene a V
                else if (_closedSet.Contains(v))
                {
                    if (v.G <= NDist)
                        continue;
                    _openSet.Add(v);
                    _closedSet.Remove(v);
                }
                //Si la lista cerrada no tiene a V
                else
                {
                    _openSet.Add(v);
                    v.SetH(end); //tenian esto, nose para que es
                }
                v.G = NDist;
                v.padre = CurrentNode;
                v.F = v.G + v.H; // me exploto la cabeza
            }
            _closedSet.Add(CurrentNode);
            _openSet.Remove(CurrentNode);
            if (CurrentNode == end)
            {
                return ThetaStar(ReconstruirCamino(start, CurrentNode));
            }
            else if (_openSet.Count == 0) ;
               
        }
        /*
        1-limpiar la lista abierta y la lista cerrada
        2-agregar start a la lista abierta
        3-siendo C el nodo actual
        4-mientras la lista abierta no este vacia
            5-siendo currentNode -> BuscarFMasBajo()
            6-si C es el nodo final
                7-reconstruimos el camino -> ReconstruirCamino(start, C) y aplicamos theta* sobre el mismo
            8-por cada vecino V
                9-si V esta en lista cerrada o no es transitable
                    10-pasamos al siguiente nodo
                11-si V no esta en lista abierta
                    12-agregamos V a la lista abierta
                    13-seteamos C como padre de V
                    14-Actualizamos F, G y H.
                 15-Si V esta en la lista abierta
                    16-chequeamos el G de V con el potencial G si padre de V es igual a C
            		17-si G potencial es menor al G actual
                        18-actualizamos el nuevo padre 
                        19-recalculamos F y G.
        20-si currentNode es el nodo final
            21-reconstruimos el camino -> ReconstruirCamino(start, C) y aplicamos theta* sobre el mismo
        22-sino, si la lista abierta esta vacía
            23-no encontramos camino 
                24-tirar excepcion o error;
        */
        return null;
	}

	private Node[] ThetaStar(Node[] AstarPath)
    {
        foreach (var Nos in AstarPath)
        {
            Nos.vecinosEnVista = new List<Node>();
            foreach (var Nes in AstarPath)
            {
                Ray ray = new Ray(Nos.transform.position, (Nes.transform.position - Nos.transform.position).normalized);
                RaycastHit Hit;
                if(Physics.Raycast(ray, out Hit, 500))
                    if(Hit.collider.gameObject == Nes.gameObject)
                    {
                        Nos.vecinosEnVista.Add(Nes);
                    }
            }
        }
        foreach (var B in AstarPath)
        {
            B.ReComputePath(); // esto tambien lo usa y nose que es (esta en la clase node)
        }
        List<Node> recorrer = new List<Node>();
        Node p = AstarPath[0];
        while (!p.isEnd)
        {
            recorrer.Add(p);
            p = p.padre;
        }
        recorrer.Add(p);
        /*
		para el theta*:
		crear en el nodo un indice o un valor segun su cercania al final
		crear una lista de vecinos en vista
		tirar rayos de cada nodo hacia los demas y
		completar las listas con los vecinos que esten visibles
		recalcular el camino
		*/
        //return AstarPath;
        // return reccorer.toarray (): TIENEN ELLAS
        return recorrer.ToArray();
    }
	
	private Node BuscarFMasBajo()
	{
    /*    int count = 0;
        foreach (var N in _openSet)
        {
            count++;
        }*/
		//Recorremos la lista de nodos abiertos.
		//Retornamos valor F mas bajo.
		return null;
	}

	private Node[] ReconstruirCamino(Node start, Node end)
	{
        var c = end;
        List<Node> aux = new List<Node>();
        while (c != start)
        {
            aux.Add(c);
            c = c.padre;
        }
        List<Node> path = new List<Node>();
        for (int i = aux.Count - 1; i >= 0; i--)
        {
            path.Add(aux[i]);
            aux[i].index = i;
        }
        foreach (var item in path)
        {
            item.isCamino = true;
        }

        //creamos C como nodo actual.
        //mientras C sea diferente a start
        //agregamos C a la lista del camino
        //seteamos C como el padre de C
        //dar vuelta la lista del camino
        //return null;
        return path.ToArray();
	}
}
