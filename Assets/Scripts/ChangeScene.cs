using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public Texture fadeImage;
    public float waitTime = 3f;

    private float fadeSpeed = .125f;
    private int drawDepth = -1000;    
    private float alpha = 0.0f;
    private int fadeDir = -1;
    private bool isFadingOut = false;

    public void ChangeToMain()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        isFadingOut = true;
        yield return new WaitForSeconds(waitTime);
        Application.LoadLevel("main");
    }
    
    void OnGUI()
    {
        if (isFadingOut)
        {
            alpha -= fadeDir * fadeSpeed * Time.deltaTime;
            alpha = Mathf.Clamp01(alpha);
            Color thisAlpha = GUI.color;
            thisAlpha.a = alpha;
            GUI.color = thisAlpha;
            GUI.depth = drawDepth;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeImage);
        }
    }
}
