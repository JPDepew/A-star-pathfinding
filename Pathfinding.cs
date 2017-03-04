using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour {

    Grid grid;

    void Awake()
    {
        grid = GetComponent<Grid>();
    }

    void FindPathf(Vector3 startPos, Vector3 targetPos)
    {
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();

        openSet.Add(startNode);

        while(openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for (int i = 1; i< openSet.Count; i++)
            {
                if(openSet[i].fCost < currentNode.fCost)
                {
                    currentNode = openSet[i];
                }
            }
        }
    }
}
