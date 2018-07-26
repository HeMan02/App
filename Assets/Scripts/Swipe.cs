using UnityEngine;

public class Swipe : MonoBehaviour
{
    Transform grid;
    Vector3 posOld;

    void Start()
    {
        grid = transform.Find("Grid");
    }
	
    // Update is called once per frame
    void Update()
    {
        if (Input.touches.Length == 1)
        {
            Touch touch = Input.touches[0];

            if (posOld == Vector3.zero)
                posOld = new Vector3(touch.position.x, 0);
            
            grid.transform.position += new Vector3(touch.position.x, 0) - posOld;
            posOld = new Vector3(touch.position.x, 0);
        }
        else
        {
            if (posOld != Vector3.zero)
            {
                int w = Camera.main.pixelWidth;
                int pageSelected = Mathf.FloorToInt(grid.transform.position.x / w);
                pageSelected = Mathf.Clamp(pageSelected, -grid.childCount + 1, 0);
                grid.transform.position = new Vector3(pageSelected * w + (w / 2f), grid.transform.position.y, 0);
                posOld = Vector3.zero;
            }
        }
    }
}
