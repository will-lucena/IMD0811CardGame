using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private PlayerScript playerTarget;
    [SerializeField] private Transform zoneTarget;

	// Use this for initialization
	void Start ()
    {
        playerTarget.updateHandState += updateZone;
    }
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    private void updateZone()
    {
        while (zoneTarget.childCount > 0)
        {
            Destroy(zoneTarget.GetChild(zoneTarget.childCount-1));
        }

        foreach (GameObject obj in playerTarget.hand)
        {
            if (obj.transform.parent != zoneTarget)
            {
                Instantiate(obj, zoneTarget);
            }
        }
    }
}
