using UnityEngine;

public class Mouse : MonoBehaviour
{
    Camera cam;

    #region Singleton
    private static Mouse instance;

    public static Mouse Instance
    {
        get
        {
            /*if (instance == null)
            {
                GameObject go = new GameObject("Mouse");
                instance = go.AddComponent<Mouse>();
            }*/
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
        cam = Camera.main;
        Debug.Log(cam);
    }
    #endregion

    private void Update()
    {
        Vector3 pose = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));
        pose.z = 0;
        transform.position = pose;
    }
}
