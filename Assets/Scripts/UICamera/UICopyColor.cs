using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class UICopyColor : MonoBehaviour
{
    [SerializeField] private List<GameObject> origines;
    [SerializeField] private List<GameObject> targets;
    void FixedUpdate()
    {
        if (origines != null || targets != null)
        {
            for (int i = 0; i < origines.Count; i++)
            {
                if (targets.Count >= i)
                {
                    try
                    {
                        targets[i].GetComponent<Image>().color = origines[i].GetComponent<Image>().color;
                    }
                    catch
                    {
                        Debug.LogError("Target or Origine n°" + i.ToString() +"does not have an Image game component, in:" + this.name);
                    }
                }
                else
                {
                    Debug.LogWarning("Target is smaller than origin in:" + this.name);
                }
            }
        }
        else
        {
            Debug.LogWarning("A list is empty in: " + this.name);
        }
    }
}
