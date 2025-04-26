using System.Collections;
using UnityEngine;
using System;
using UnityEngine.Rendering;

//-----------------------------  classe abstraite  -----------------------------

/// <summary>
/// les enfants de cette classe permettent d'animer facilement les valeurs de post processing (bloom,exposure...) in game.
/// ils n�c�ssitent la pr�sence d'une instance de PostProcessController dans la sc�ne
/// ses parametres sont s�rialis�s et il suffit d'appeler setUp() dans start ou Awake et OnDestroy() dans OnDestroy() puis Play() pour l'utiliser.
/// </summary>
/// <typeparam name="ComponentType"></typeparam>
[Serializable]
public abstract class PostProcessEffectAnimation<ComponentType> where ComponentType : VolumeComponent
{
    public float duration;

    protected ComponentType _component;


    public void SetUp(VolumeProfile mVolumeProfile)
    {
        for (int i = 0; i < mVolumeProfile.components.Count; i++)
        {

            if (mVolumeProfile.components[i].GetType() == typeof(ComponentType))
            {
                _component = (ComponentType)mVolumeProfile.components[i];
            }
        }

        if (!PostProcessController.instance.effectsCoroutines.ContainsKey(this.GetType()))
        {
            PostProcessController.instance.effectsCoroutines.Add(this.GetType(), null);
        }

        OnSetUp();

    }

    /// <summary>
    /// peut etre utilis� pour enregistrer la valeur de base de l'effet qu'on va modifier
    /// </summary>
    protected abstract void OnSetUp();

    /// <summary>
    /// peut etre utilis� pour remettre la valeur � 0 � la fin du jeu.
    /// </summary>
    public abstract void OnDestroy();

    /// <summary>
    /// joue l'animation
    /// </summary>
    /// <param name="useUnscaledTime"></param>
    public void play(bool useUnscaledTime = false)
    {
        stop();
        PostProcessController.instance.effectsCoroutines[this.GetType()] = PostProcessController.instance.StartCoroutine(_Play(useUnscaledTime));
    }

    /// <summary>
    /// arrete l'animation (sans reset la valeur)
    /// </summary>
    /// <param name="mb"></param>
    public void stop()
    {
        if (PostProcessController.instance.effectsCoroutines[this.GetType()] != null)
        {
            PostProcessController.instance.StopCoroutine(PostProcessController.instance.effectsCoroutines[this.GetType()]);
        }
        PostProcessController.instance.effectsCoroutines[this.GetType()] = null;
    }
    private IEnumerator _Play(bool useUnscaledTime = false)
    {
        //print("--anim starting--");
        float t = (useUnscaledTime ? Time.unscaledTime : Time.time);
        float endTime = t + duration;
        OnBeforePlay();
        while (t < endTime)
        {
            t = (useUnscaledTime ? Time.unscaledTime : Time.time);
            //print("putain;");
            float alpha = Mathf.InverseLerp(endTime - duration, endTime, t);//1f-(endTime-Time.time)/duration;
                                                                            //print("Alpha:" + alpha);
            ApplyEffect(_component, alpha);
            //parameter.value = (parameter.value*alpha);

            yield return null;
        }
        ApplyEffect(_component, 1);
        // print("--anim done--");
    }

    /// <summary>
    /// peut etre utilis� pour r�cup�rer la valeur voulue juste avant le d�but de l'animation
    /// </summary>
    protected virtual void OnBeforePlay()
    {

    }

    /// <summary>
    /// cette fonction est appel�e chaque frame pendant la dur�e de l'animation.
    /// la valeur Alpha va de 0 � 1 et peut etre utilis�e pour appliquer l'effet de post process voulu sur le component.
    /// </summary>
    /// <param name="component"></param>
    /// <param name="alpha"></param>
    protected abstract void ApplyEffect(ComponentType component, float alpha);
}

