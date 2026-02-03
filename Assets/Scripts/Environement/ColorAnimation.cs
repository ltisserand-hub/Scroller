using UnityEngine;

public class ColorAnimation : MonoBehaviour
{
    [SerializeField] float transitionTime = 5f;
    [SerializeField] Gradient colors;
    void Update()
    {
        GetComponent<SpriteRenderer>().color = colors.Evaluate(Mathf.PingPong(Time.unscaledTime,transitionTime)/transitionTime);
    }
}
