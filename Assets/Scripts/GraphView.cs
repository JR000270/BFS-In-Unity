using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphView : MonoBehaviour
{
    public GameObject nodeViewPrefab;
    public Color openColor = Color.white;
    public Color blockedColor = Color.black;
    public NodeView[,] nodeViews;

    public void Init(Graph graph)
    {
        if(graph == null)
        {
            Debug.LogWarning("Error: no graph to initiallize");
            return;
        }
        nodeViews = new NodeView[graph.m_width , graph.m_height];
        foreach(Node n in graph.nodes)
        {
            GameObject instance = Instantiate(nodeViewPrefab, Vector3.zero, Quaternion.identity);
            NodeView nodeView = instance.GetComponent<NodeView>();
            if(nodeView != null)
            {
                nodeView.Init(n);
                nodeViews[n.xIndex, n.yIndex] = nodeView;
                if (n.nodeType == NodeType.Blocked)
                {
                    nodeView.ColorNode(blockedColor);
                }
                else
                {
                    nodeView.ColorNode(openColor);
                }

            }
        }
    }

    public void ColorNodes(List<Node> nodes, Color color)
    {
        foreach(Node n in nodes)
        {
            if(n != null)
            {
                NodeView nodeView = nodeViews[n.xIndex, n.yIndex];
                if(nodeView!= null)
                {
                    nodeView.ColorNode(color);
                }
            }
        }
    }
}
