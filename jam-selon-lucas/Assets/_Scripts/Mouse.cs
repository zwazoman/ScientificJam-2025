using UnityEngine;

public class Mouse : MonoBehaviour
{
    #region Singleton
    private static Mouse instance;

    public static Mouse Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("Mouse");
                instance = go.AddComponent<Mouse>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }
    #endregion

    private void Update()
    {
        transform.position = Input.mousePosition;
    }
}
