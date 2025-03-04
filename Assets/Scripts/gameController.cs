using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour
{
    public MapData mapData;
    public Graph graph;
    public PathFinder pathFinder;
    public int startX = 0;
    public int startY = 0;
    public int goalX = 15;
    public int goalY = 1;
    public float timeStep = 0.1f;

    void Start()
    {
        if(mapData!=null && graph != null)
        {
            int[,] mapInstance = mapData.makeMap();//2D array of 1's and 0's
            graph.Init(mapInstance);//convert map to array of nodes
            GraphView graphView = graph.gameObject.GetComponent<GraphView>();
            if (graphView != null)
            {
                graphView.Init(graph);
            }
            else
            {
                Debug.LogWarning("NO graph view is found");
            }
            if (graph.IsWithinBounds(startX, startY) && graph.IsWithinBounds(goalX, goalY) && pathFinder != null) 
            {
                Node startNode = graph.nodes[startX, startY];
                Node goalNode = graph.nodes[goalX, goalY];
                pathFinder.Init(graph, graphView, startNode, goalNode);
                StartCoroutine(pathFinder.SearchRoutine(timeStep));
            }
            else
            {
                Debug.Log("Out of range");
            }
        }   
    }
}
