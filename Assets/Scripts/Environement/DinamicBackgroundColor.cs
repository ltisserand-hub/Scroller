using System.Collections.Generic;
using UnityEngine;

public class DinamicBackgroundColor : MonoBehaviour
{
    [SerializeField] float transitionTime = 5f;
    [Header("Colors")]
    [SerializeField] List<Color> color;
    private Color currentColor;
    private float transitionCurrentTime = 0f;
    void Update()
    {
        transitionCurrentTime = Mathf.PingPong(Time.unscaledTime,transitionTime);
        currentColor = Color.Lerp(color[0], color[1], transitionCurrentTime/transitionTime);
        Camera.main.backgroundColor = currentColor;
      
    }
}
