using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vague : MonoBehaviour
{

    void Start()
    {
        transform.rotation=Quaternion.identity;
       //détache tous les objets attachés et se suicide. Cet objet est juste utilisé pour pouvoir faire spawn plusieurs objets d'un coup
        while(transform.childCount > 0)
        {
            transform.GetChild(0).parent = null ;
        }
        Destroy(gameObject);
    }


}
