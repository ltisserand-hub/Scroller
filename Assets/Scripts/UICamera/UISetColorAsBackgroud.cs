using UnityEngine;
using Image = UnityEngine.UI.Image;

public class UISetColorAsBackgroud : MonoBehaviour
{
    [SerializeField][Range(0.0f, 1.0f)] private float _tintPourcentage;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _target;
    void Update()
    {
        _target.GetComponent<Image>().color = Color.Lerp(_camera.backgroundColor, Color.white, _tintPourcentage);
    }
}
