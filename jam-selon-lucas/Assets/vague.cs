using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vague : MonoBehaviour
{

    void Start()
    {
        transform.rotation=Quaternion.identity;
       //d�tache tous les objets attach�s et se suicide. Cet objet est juste utilis� pour pouvoir faire spawn plusieurs objets d'un coup
        while(transform.childCount > 0)
        {
            transform.GetChild(0).parent = null ;
        }
        Destroy(gameObject);
    }


}
