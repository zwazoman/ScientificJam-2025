using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Ce script n'est pas un singleton car on peut avoir une pool par prefab dans la sc�ne,avec chacune des tailles diff�rentes.
/// Dans un plus gros projet, on pourrait coder un script poolmanager qui serait un singleton avec dedans toutes les pools et des enums pour faire spawn n'importe quelle prefab facilement etc, mais KISS.
/// 
/// Messages optionnels envoy�s aux objets de la pool:
/// - OnInstantiatedByPool
/// - OnPulledFromPool
/// - OnPutBackIntoPool
/// </summary>
public class Pool : MonoBehaviour
{
    [SerializeField] GameObject _prefab;
    [SerializeField] uint _initialPoolSize = 50;

    /*[SerializeField] */
    private List<PooledObject> _instances; //toutes les instances d'objets, actives comme inactives.
    /*[SerializeField] */
    private List<int> _freeIndices = new List<int>(); //contient tous les indices des instances inactives dans le tableau _instances

    private void Awake()
    {
        //cr�ation du tableau d'instances
        _instances = new List<PooledObject>();
        PopulatePool(_initialPoolSize);
    }

    /// <summary>
    /// peuple la pool en instanciant des GameObjects ayant tous un component PooledObject
    /// </summary>
    private void PopulatePool(uint poolSize)
    {
        

        //regarde si il y'aura besoin d'ajouter le component PooledObject aux instances de la prefab,ou si il y est d�j�.
        bool PrefabAlreadyHasPooledObjectComponent = _prefab.GetComponent<PooledObject>();

        //cr�e toutes les instances de la prefab et les met dans la pool.
        for (int i = 0; i < poolSize; i++)
        {
            GameObject instancedObject = GameObject.Instantiate(_prefab);
            instancedObject.name += i.ToString();

            PooledObject po = PrefabAlreadyHasPooledObjectComponent ? instancedObject.GetComponent<PooledObject>() : instancedObject.AddComponent<PooledObject>();
            _instances.Add(po);
            po.Index = _instances.IndexOf(po);
            po.Pool = this;
            po.IsInPool = true;

            //message optionnel
            instancedObject.BroadcastMessage("OnInstantiatedByPool", SendMessageOptions.DontRequireReceiver);

            PutObjectBackInPool(po);
        }
    }


    /// <summary>
    /// permet de faire "spawn" un gameObject inactif depuis la pool. � appeler � la place de GAMEOBJECT.INSTANTIATE()
    /// </summary>
    /// <param name="Parent"></param>
    /// <returns></returns>
    public GameObject PullObjectFromPool(Transform Parent = null,bool shouldBroadcast = true)
    {
        if (_freeIndices.Count <= 0)
        {
            PopulatePool(5);
        }

        //pioche le premier indice libre dans la pool.
        int id = _freeIndices[0];
        _freeIndices.Remove(id);

        //activation de l'objet s�lectionn�
        PooledObject o = _instances[id];
        o.IsInPool = false;
        o.gameObject.SetActive(true);
        o.enablePreviouslyDisabledComponents();
        o.transform.parent = Parent;

        //message optionnel
        if(shouldBroadcast) o.gameObject.BroadcastMessage("OnPulledFromPool", SendMessageOptions.DontRequireReceiver);
        return o.gameObject;
    }

    /// <summary>
    /// permet de faire "spawn" un gameObject inactif depuis la pool � une certaine position. 
    /// � appeler � la place de GAMEOBJECT.INSTANTIATE()
    /// </summary>
    public GameObject PullObjectFromPool(Vector3 Position, Transform Parent = null, bool shouldBroadcast = true)
    {
        GameObject o = PullObjectFromPool(Parent,false);
        o.transform.position = Position;

        if (shouldBroadcast) o.gameObject.BroadcastMessage("OnPulledFromPool", SendMessageOptions.DontRequireReceiver);
        return o;
    }

    /// <summary>
    /// permet de faire "spawn" un gameObject inactif depuis la pool � une certaine position et rotation. 
    /// � appeler � la place de GAMEOBJECT.INSTANTIATE()
    /// </summary>
    public GameObject PullObjectFromPool(Vector3 Position, Quaternion rotation, Transform Parent = null, bool shouldBroadcast = true)
    {
        GameObject o = PullObjectFromPool(Position, Parent,false);
        o.transform.rotation = rotation;

        if (shouldBroadcast) o.gameObject.BroadcastMessage("OnPulledFromPool", SendMessageOptions.DontRequireReceiver);
        return o;
    }



    /// <summary>
    /// desactive un objet et le remet dans la pool.
    /// le component PooledObject contient �galement une m�thode GoBackIntoPool() pour faire "despawn" l'objet facilement.
    /// </summary>
    /// <param name="ObjectToPool"></param>
    public void PutObjectBackInPool(PooledObject ObjectToPool)
    {
        if (!_freeIndices.Contains(ObjectToPool.Index))
        {
            _freeIndices.Add(ObjectToPool.Index);
            ObjectToPool.gameObject.BroadcastMessage("OnPutBackIntoPool", SendMessageOptions.DontRequireReceiver);

        }

        ObjectToPool.transform.parent = transform;
        ObjectToPool.gameObject.SetActive(false);
        ObjectToPool.IsInPool = true;



    }


}
