using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Zoom : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 initialScale;
    private Transform parent;
    private Transform myCanvas;
    public float zoomSpeed;
    public float maxZoom;
    private bool isZoom;
    private bool isCentered;

    private void Start()
    {
        initialScale = transform.localScale;
        parent = transform.parent;
        isZoom = false;

        foreach (Transform t in GetComponentsInParent<Transform>())
        {
            if (t.CompareTag("Canvas"))
            {
                myCanvas = t;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Vector3 delta = Vector3.one * eventData.position.x * zoomSpeed;
        Vector3 newScale = transform.localScale + delta;

        newScale = ClampNewScale(newScale);

        transform.localScale = newScale;
        transform.SetParent(myCanvas);
        transform.position = new Vector3(Screen.width / 2, Screen.height / 2, transform.localPosition.z);
        
        if (!isCentered)
        {
            isCentered = true;
        }
        else
        {
            isZoom = true;
        }
    }

    private Vector3 ClampNewScale(Vector3 scale)
    {
        scale = Vector3.Max(initialScale, scale);
        scale = Vector3.Min(initialScale * maxZoom, scale);

        return scale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isZoom)
        {
            transform.SetParent(parent);
            transform.localScale = initialScale;
            isZoom = false;
            isCentered = false;
        }
    }
}
