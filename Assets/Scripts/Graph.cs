using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public Node[,] nodes; //Array of Nodes
    public List<Node> walls = new List<Node>();

    int[,] m_MapData;
    public int m_width;
    public int m_height;

    public static readonly Vector2[] allDirections =
    {
        new Vector2(0f,1f),
        new Vector2(1f, 1f),
        new Vector2(1f, 0f),
        new Vector2(0f, 0f),
        new Vector2(1f, -1f),
        new Vector2(0f, -1f),
        new Vector2(-1f, 0f),
        new Vector2(-1f, 1f),
        new Vector2(-1f, -1f)
    };
    public void Init(int[,] mapData)
    {
        m_MapData = mapData;
        m_width = mapData.GetLength(0);
        m_height = mapData.GetLength(1);
        nodes = new Node[m_width, m_height];
        for(int y = 0; y < m_height; y++)
        {
            for(int x = 0; x < m_width; x++)
            {
                NodeType nodeType = (NodeType) mapData[x, y];
                Node newNode = new Node(x, y, nodeType);
                nodes[x, y] = newNode;
                newNode.position = new Vector3(x, 0, y);
                Debug.Log("Node ("  + newNode.position.x + ", " + newNode.position.z + ") ");             
                if(nodeType == NodeType.Blocked)
                {
                    walls.Add(newNode);
                }
            }
        }

        for (int y = 0; y < m_height; y++)
        {
            for (int x = 0; x < m_width; x++)
            {
                if (nodes[x, y].nodeType != NodeType.Blocked)
                {
                    nodes[x, y].neighbors = getNeighbors(x, y);
                }
            }
        }
        Debug.Log("Successfully Called");
    }

    public bool IsWithinBounds(int x, int y)
    {
        return (x>= 0 && x<m_width && y>= 0 && y < m_height);
    }

    public List<Node> getNeighbors(int x, int y, Node[,] NodeArray, Vector2[] directions)
    {
        List<Node> neighborNodes = new List<Node>();
        Debug.Log("Current Node ( " + NodeArray[x,y].position.x + ", " + NodeArray[x, y].position.z + ")");
        //vector2[] directions = allDirections;
        foreach(Vector2 dir in allDirections)
        {
            int newX = x + (int)dir.x;
            int newY = y + (int)dir.y;
            if(IsWithinBounds(newX, newY) && NodeArray[newX, newY]!=null && NodeArray[newX, newY].nodeType != NodeType.Blocked)
            {
                neighborNodes.Add(NodeArray[newX, newY]);
            }
        }
        Debug.Log("Neighbor Count " + neighborNodes.Count );
        return neighborNodes;
    }

    public List<Node> getNeighbors(int x, int y)
    {
        return getNeighbors(x, y, nodes, allDirections);
    }

}

