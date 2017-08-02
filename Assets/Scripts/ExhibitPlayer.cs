using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExhibitPlayer : MonoBehaviour {

    public float sheetColumns = 24f; 
    public float sheetRows = 1f; 
    public float framesPerSecond = 10.0f;
    public Material stoppedMaterial;

    private Material animatedMat;
    private Renderer rend;
    private bool activated = false;

    void Animate()
    {
        // Calculate index
        float index = Time.time * framesPerSecond;

        // repeat when exhausting all frames
        //index = index % (sheetColumns * sheetRows);

        // Size of every tile
        Vector2 size = new Vector2(1.0f / sheetColumns, 1.0f / sheetRows);

        // split into horizontal and vertical index
        int horizontalIndex = (int)(index % sheetColumns);
        int verticalIndex = (int)(index / sheetColumns);

        // build offset
        // v coordinate is the bottom of the image in opengl so we need to invert.
        Vector2 offset = new Vector2(horizontalIndex * size.x, 1.0f - size.y - verticalIndex * size.y);

        rend.material.SetTextureOffset("_MainTex", offset);
        rend.material.SetTextureScale("_MainTex", size);
    }

    void Start()
    {
        rend = GetComponent<Renderer>();
        animatedMat = rend.material;
        rend.material = stoppedMaterial;
    }

    void Update()
    {
        if (activated)
        {
            rend.material = animatedMat;
            Animate();
        }
    }
    
}
