using DG.Tweening;
using UnityEngine;

public class LevelUpPanel : MonoBehaviour
{

    private void Start()
    {
        PlayerMain.instance.playerXP.OnLvlUp += () =>
        {
            Sequence s = DOTween.Sequence();
            s.Append(GetComponent<RectTransform>().DOAnchorPosX(220, .5f));
            s.AppendInterval(.5f);
            s.Append(GetComponent<RectTransform>().DOAnchorPosX(-270, .5f));
            s.PlayForward();
        };
    }
}
