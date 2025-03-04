using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeView : MonoBehaviour
{
    public GameObject tile;
    [Range(0,0.5f)]//range of values for bordersize
    public float bordersize = 0.15f;
    public void Init(Node node)
    {
        if (tile != null)
        {
            gameObject.name = "Node (" + node.position.x + ", " + node.position.z + ")";
            tile.transform.position = node.position;
            tile.transform.localScale = new Vector3(1f-bordersize, 1f, 1f-bordersize);//scaling boundaries beteen tiles on x and z axis
        }
    }

    void ColorNode(Color color, GameObject gameObject)
    {
        if (gameObject != null)
        {
            Renderer gameObjectRenderer = gameObject.GetComponent<Renderer>();
            gameObjectRenderer.material.color = color;
        }
        
    }

    public void ColorNode(Color color)
    {
        ColorNode(color, tile);
    }

}
