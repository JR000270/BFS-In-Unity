using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour
{
    public int height = 5;
    public int width = 5;
    public TextAsset Text;

    List<string> GetTextFromFile(TextAsset tAsset)
    {
        List<string> lines = new List<string>();
        if(tAsset != null)
        {
            string textData = tAsset.text;
            string[] delimeters = {"\r\n", "\n"};
            lines.AddRange(textData.Split(delimeters, System.StringSplitOptions.None));
        }
        return lines;
    }

    public List<string> GetTextFromFile()
    {
        return GetTextFromFile(Text);
    }

    void setDimensions(List<string> textLines)
    {
        height = textLines.Count;
        foreach (string line in textLines)
        {
            width = line.Length;
        }
    }

    public int[,] makeMap()
    {
        List<string> lines = new List<string>();
        lines = GetTextFromFile();
        setDimensions(lines);

        int[,] map = new int[width, height];
        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                map[x, y] = (int) char.GetNumericValue(lines[y][x]);
            }
        }
        

        return map;
    }

}
