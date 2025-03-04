using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PathFinder : MonoBehaviour
{
    Node m_startNode;
    Node m_goalNode;
    Graph m_graph;
    GraphView m_graphView;
    Queue<Node> m_frontierNodes;
    List<Node> m_explored;
    List<Node> m_pathNodes;
    public Color startColor = Color.green;
    public Color goalColor = Color.red;
    public Color frontierColor = Color.magenta;
    public Color pathColor = Color.cyan;
    public Color exploredColor = Color.grey;
    public bool isComplete;
    public int iterations;

    public void Init(Graph graph, GraphView graphView, Node start, Node goal)
    {
        if (start == null || goal == null || graph == null || graphView == null)
        {
            Debug.LogWarning("Path finder init error: missing a parameter");
            return;
        }
        if (start.nodeType == NodeType.Blocked || goal.nodeType == NodeType.Blocked)
        {
            Debug.LogWarning("Path finder Init error: start of goal node cannot be a blocked node");
            return;
        }
        m_graph = graph;
        m_graphView = graphView;
        m_startNode = start;
        m_goalNode = goal;
        m_frontierNodes = new Queue<Node>();
        m_frontierNodes.Enqueue(start);
        m_explored = new List<Node>();
        m_pathNodes = new List<Node>();

        for (int y = 0; y < m_graph.m_height; y++)
        {
            for (int x = 0; x < m_graph.m_width; x++)
            {
                m_graph.nodes[x, y].Reset();
            }
        }
        showColors();
        
        isComplete = false;
        iterations = 0;

    }

    private void showColors(GraphView graphView, Node start, Node goal)
    {
        if (graphView == null || start == null || goal == null ) {
            return;
        }
        NodeView startNodeView = m_graphView.nodeViews[start.xIndex, start.yIndex];
        NodeView goalNodeView = m_graphView.nodeViews[goal.xIndex, goal.yIndex];

        if (m_frontierNodes != null)
        {
            graphView.ColorNodes(m_frontierNodes.ToList(), frontierColor);
        }
        if (m_explored != null)
        {
            graphView.ColorNodes(m_explored, exploredColor);
        }
        if (m_pathNodes != null)
        {
            graphView.ColorNodes(m_pathNodes, pathColor);
        }
        if (startNodeView != null)
        {
            startNodeView.ColorNode(startColor);
        }
        if (goalNodeView != null)
        {
            goalNodeView.ColorNode(goalColor);
        }
    }

    public void showColors()
    {
        showColors(m_graphView, m_startNode, m_goalNode);
    }
    public IEnumerator SearchRoutine(float timeStep = 0.1f)
    {
        yield return null;
        while (!isComplete)
        {
            if(m_frontierNodes.Count > 0)
            {
                Node currentNode = m_frontierNodes.Dequeue();
                iterations++;
                if (!m_explored.Contains(currentNode))
                {
                    m_explored.Add(currentNode);
                }
                ExpandFrontier(currentNode);
                if (m_frontierNodes.Contains(m_goalNode))
                {
                    m_pathNodes = GetPathNodes(m_goalNode);
                    isComplete = true;
                }
                yield return new WaitForSeconds(timeStep);
            }
            else
            {
                isComplete = true;
            }
            showColors();
        }
        //showColors();
    }

    private void ExpandFrontier(Node node)
    {
        //go through all neighbors of current node
        for(int i = 0; i < node.neighbors.Count; i++)
        {
            if (!m_explored.Contains(node.neighbors[i]) &&
                !m_frontierNodes.Contains(node.neighbors[i])) 
            {
                node.neighbors[i].previous = node;
                m_frontierNodes.Enqueue(node.neighbors[i]);
            }
        }
            
    }

    List<Node> GetPathNodes(Node goalNode)
    {
        List<Node> path = new List<Node>();
        if (goalNode == null)
        {
            return path;
        }
        path.Add(goalNode);
        Node currentNode = goalNode.previous;
        while(currentNode != null)
        {
            path.Insert(0, currentNode);
            currentNode = currentNode.previous;
        }
        return path;
    }
}
