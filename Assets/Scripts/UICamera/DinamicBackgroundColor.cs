using System.Collections.Generic;
using UnityEngine;

public class DinamicBackgroundColor : MonoBehaviour
{
    [SerializeField] float transitionTime = 5f;
    public Gradient colors;
    private float transitionCurrentTime = 0f;
    void Update()
    {
        transitionCurrentTime = Mathf.PingPong(Time.unscaledTime,transitionTime);
        Camera.main.backgroundColor = colors.Evaluate(transitionCurrentTime / transitionTime);
      
    }
}
