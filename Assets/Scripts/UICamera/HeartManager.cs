using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class HeartManager : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] [Range(0, 40)] private int maxHealth; //1 visual heart is considered as 2 HP
    [SerializeField] [Range(0, 40)] int health;
    [Header("Render Settings")]
    [SerializeField] [Range(1, 10)] int heartsPerColumn; //Counts the visual hearts
    [SerializeField] private Vector3 offSet;
    [Header("Sprite")]
    [SerializeField] private List<Sprite> hearts;
    //6 images required in this order:
    //0 = Full Heart
    //1 = Half Full Heart
    //2 = Empty Heart
    //3 = Half Empty Heart
    //4 = Half Full Heart with empty
    [SerializeField] public GameObject heartPrefab;
    private Image _image;
    private List<GameObject> _hearts = new();
    private Vector3 healthMemory;
    
    void Update()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
            Debug.Log("Imposible Health Value in: " + gameObject.name);
        }
        if (new Vector3(health, maxHealth) != healthMemory)
        {
            while (_hearts.Count < (float)maxHealth / 2 )
            {
                _hearts.Add(CreateHeart(_hearts.Count));
            }
            
            RenderHearts();
            healthMemory = (new Vector3(health, maxHealth));
        }
    }

    GameObject CreateHeart( int _position)
    {
        GameObject heart = Instantiate<GameObject>(heartPrefab);
        heart.transform.SetParent(gameObject.transform,false);
        heart.GetComponent<RectTransform>().localPosition = new Vector3(offSet.x * (_position % heartsPerColumn + 1),offSet.y*(_position/ heartsPerColumn));
        heart.name = "Heart " + _position.ToString();
        return heart;
    }

    void RenderHearts()
    {
        for (int i = 0; i < _hearts.Count; i ++)
        {
            if (i < maxHealth / 2)
            {
                _hearts[i].SetActive(true);
                if (i < health / 2)
                {
                    _hearts[i].GetComponent<Image>().sprite = hearts[0];
                }
                else if (i < (float)health / 2)
                {
                    _hearts[i].GetComponent<Image>().sprite = hearts[3];
                }
                else
                {
                    _hearts[i].GetComponent<Image>().sprite = hearts[2];
                }
            }
            else if (i < (float)maxHealth / 2)
            {
                _hearts[i].SetActive(true);
                if (i < (float)health / 2)
                {
                    _hearts[i].GetComponent<Image>().sprite = hearts[1];
                }
                else
                {
                    _hearts[i].GetComponent<Image>().sprite = hearts[4];
                }
            }
            else
            {
                _hearts[i].SetActive(false);
            }
        }
    } 
}
