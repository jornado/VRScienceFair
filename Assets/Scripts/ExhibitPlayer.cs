using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExhibitPlayer : MonoBehaviour {

    // Public Variables
    public float sheetColumns = 4f; 
    public float sheetRows = 2f; 
    public float framesPerSecond = 1.0f;
    public Material stoppedMaterial;
    public bool activated = false;

    public GameObject top;
    public GameObject left;
    public GameObject right;
    public GameObject table;

    // Private Variables
    private AudioSource audioClip;
    private ExhibitPlayer[] allExhibits;
  
    private Vector2 tileSize;
    private Material animatedMat;
    private Renderer rend;

    private float origScaleX;
    private float origScaleY;
    private Vector3 oldPosition;

    void Awake()
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

        // Audio stuff
        audioClip = GetComponent<AudioSource>();
        allExhibits = FindObjectsOfType(typeof(ExhibitPlayer)) as ExhibitPlayer[];
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
            Activate();
        }
        else {
            Deactivate();
        }
    }

    void ToggleSetDressing(bool tf)
    {
        top.SetActive(tf);
        right.SetActive(tf);
        left.SetActive(tf);
        table.SetActive(tf);
    }

    void TurnOffSetDressing()
    {
        ToggleSetDressing(false);
    }

    void TurnOnSetDressing()
    {
        ToggleSetDressing(true);
    }

    void Activate()
    {
        StopAllAudio();
        TurnOffSetDressing();
        Embiggen();
        if (audioClip && audioClip.isActiveAndEnabled && !audioClip.isPlaying)
        {
            audioClip.Play();
        }
        
    }

    void Deactivate()
    {
        TurnOnSetDressing();
        Smallerize();
        if (audioClip && audioClip.isActiveAndEnabled && audioClip.isPlaying)
        {
            audioClip.Stop();
        }
    }

    void StopAllAudio()
    {
        ExhibitPlayer thisScript = gameObject.GetComponent<ExhibitPlayer>();
        foreach (ExhibitPlayer exhibit in allExhibits)
        {
            if (exhibit != thisScript && exhibit.activated)
            {
                exhibit.ToggleActivated();
            }
        }
    }
    void Embiggen()
    {
        rend.material = animatedMat;
        Transform transform = GetComponent<Transform>();
        transform.localScale = new Vector3(2f, 1.2f, GetComponent<Transform>().localScale.z);
    }

    void Smallerize()
    {
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
