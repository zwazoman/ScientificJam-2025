using UnityEngine;

public class hyperlink : MonoBehaviour
{
    public string link;
    public void Open()
    {
        Application.OpenURL(link);
    }
}
