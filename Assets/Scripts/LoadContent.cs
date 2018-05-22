using UnityEngine;
using UnityEngine.UI;

public class LoadContent : MonoBehaviour
{
    [SerializeField] private GameObject contentPrefab;
    public CardData[] cards;

    void Start()
    {
        cards = Resources.LoadAll<CardData>("Cards/");
        float newSize = 0;
        foreach (CardData c in cards)
        {
            GameObject obj = Instantiate(contentPrefab, transform);
            HeroData data = (HeroData) c;

            obj.GetComponent<Content>().setValues(data.image, data.name, data.atk, data.def, data.health, data);

            newSize += obj.GetComponent<LayoutElement>().preferredHeight;
        }
        resizeContent(newSize);
    }

    private Sprite loadSprite(string name)
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>(name);
        return spriteRenderer.sprite;
    }

    private void resizeContent(float offset)
    {
        RectTransform rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y + offset);
        rt.offsetMin = new Vector2(rt.offsetMin.x, -rt.sizeDelta.y);
        rt.offsetMax = new Vector2(rt.offsetMax.x, 0);
    }
}