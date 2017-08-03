using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExhibitPlayer : MonoBehaviour {

    public float sheetColumns = 4f; 
    public float sheetRows = 2f; 
    public float framesPerSecond = 1.0f;
    public Material stoppedMaterial;
    public bool activated = false;

    public GameObject top;
    public GameObject left;
    public GameObject right;
    public GameObject table;


    private Vector2 tileSize;
    private Material animatedMat;
    private Renderer rend;

    private float origScaleX;
    private float origScaleY;
    private Vector3 oldPosition;
    
    void Start()
    {
        // Movie stuff
        rend = GetComponent<Renderer>();
        tileSize = new Vector2(1.0f / sheetColumns, 1.0f / sheetRows);
        animatedMat = rend.material;
        rend.material = stoppedMaterial;

        // Resize stuff
        oldPosition = new Vector3(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y, GetComponent<Transform>().position.z);
        origScaleX = GetComponent<Transform>().localScale.x;
        origScaleY = GetComponent<Transform>().localScale.y;
    }

    void Update()
    {
        if (activated)
        {
            AnimateTexture();
        }
    }

    public void ToggleActivated()
    {
        activated = !activated;
        if (activated)
        {
            Embiggen();
        }
        else {
            Smallerize();
        }
    }

    void Embiggen()
    {
        top.SetActive(false);
        right.SetActive(false);
        left.SetActive(false);
        table.SetActive(false);

        rend.material = animatedMat;
        Transform transform = GetComponent<Transform>();
        transform.localScale = new Vector3(2f, 1.2f, GetComponent<Transform>().localScale.z);
    }

    void Smallerize()
    {
        top.SetActive(true);
        right.SetActive(true);
        left.SetActive(true);
        table.SetActive(true);

        rend.material = stoppedMaterial;
        GetComponent<Transform>().position = oldPosition;
        transform.localScale = new Vector3(origScaleX, origScaleY, GetComponent<Transform>().localScale.z);
    }

    void AnimateTexture()
    {
        // Calculate index
        float index = Time.time * framesPerSecond;

        // repeat when exhausting all frames
        //index = index % (sheetColumns * sheetRows);
        
        // split into horizontal and vertical index
        int horizontalIndex = (int)(index % sheetColumns);
        int verticalIndex = (int)(index / sheetColumns);

        // build offset
        // v coordinate is the bottom of the image in opengl so we need to invert.
        Vector2 offset = new Vector2(horizontalIndex * tileSize.x, 1.0f - tileSize.y - verticalIndex * tileSize.y);

        rend.material.SetTextureOffset("_MainTex", offset);
        rend.material.SetTextureScale("_MainTex", tileSize);
    }
}
