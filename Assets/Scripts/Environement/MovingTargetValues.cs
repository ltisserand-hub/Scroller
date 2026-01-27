using UnityEngine;
public class MovingTargetValues : MonoBehaviour
{
    [Header("Targets")]
    [SerializeField] Transform targetA;
    [SerializeField] Transform targetB;
    [Header("Settings")]
    [SerializeField] float speed;
    [SerializeField] float waitTime;
    [SerializeField] bool easeInOut = true;
    private Animation _animation;
    AnimationClip _clip;

    private void Start()
    {
        _clip = new  AnimationClip();
        _animation = GetComponent<Animation>();
        _animation.clip = _clip;
        _clip.legacy = true;
        
        AnimationCurve curveX = new  AnimationCurve();
        AnimationCurve curveY = new  AnimationCurve();
        if (easeInOut)
        {
            curveX = AnimationCurve.EaseInOut(waitTime, targetA.localPosition.x, speed+waitTime, targetB.localPosition.x);
            curveY = AnimationCurve.EaseInOut(waitTime, targetA.localPosition.y, speed+waitTime, targetB.localPosition.y);
        }
        else
        {
            curveX = AnimationCurve.Linear(waitTime, targetA.localPosition.x, speed+waitTime, targetB.localPosition.x);
            curveY = AnimationCurve.Linear(waitTime, targetA.localPosition.y, speed+waitTime, targetB.localPosition.y);
        }
        
        _clip.SetCurve("", typeof(Transform), "localPosition.x", curveX);
        _clip.SetCurve("", typeof(Transform), "localPosition.y", curveY);
        
        _clip.wrapMode = WrapMode.PingPong;
        _animation.AddClip(_clip, "PlatformAnimation");
        _animation.Play("PlatformAnimation");
    }
}
