using UnityEngine;
public class MovingTargetValues : MonoBehaviour
{
    [SerializeField] Transform targetA;
    [SerializeField] Transform targetB;
    [SerializeField] float speed;
    [SerializeField] float waitTime;
    float _currentTime;
    private Animation _animation;
    AnimationClip _clip;
    

    private void Start()
    {
        _clip = new  AnimationClip();
        _animation = GetComponent<Animation>();
        _animation.clip = _clip;
        _clip.legacy = true;
        
        AnimationCurve curveX = new  AnimationCurve();
        
        curveX.AddKey(_currentTime,targetA.position.x); //move to target A
        _currentTime += speed; //move time
        curveX.AddKey(_currentTime,targetA.position.x); //target A
        _currentTime += waitTime; //wait time
        curveX.AddKey(_currentTime,targetB.position.x); //move to target B
        _currentTime += speed; //move time
        curveX.AddKey(_currentTime,targetB.position.x); //target B

        _currentTime = 0f;
        
        AnimationCurve curveY = new  AnimationCurve();
        curveY.AddKey(_currentTime,targetA.position.y); //move to target A
        _currentTime += speed; //move time
        curveY.AddKey(_currentTime,targetA.position.y); //target A
        _currentTime += waitTime; //wait time
        curveY.AddKey(_currentTime,targetB.position.y); //move to target B
        _currentTime += speed; //move time
        curveY.AddKey(_currentTime,targetB.position.y); //target B
        
        _clip.SetCurve("", typeof(Transform), "localPosition.x", curveX);
        _clip.SetCurve("", typeof(Transform), "localPosition.y", curveY);
        
        
        _clip.wrapMode = WrapMode.PingPong;
        _animation.AddClip(_clip, "PlatformAnimation");
        _animation.Play("PlatformAnimation");
    }
}
