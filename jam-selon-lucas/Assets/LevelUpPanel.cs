using DG.Tweening;
using UnityEngine;

public class LevelUpPanel : MonoBehaviour
{

    private void Start()
    {
        PlayerMain.instance.playerXP.OnLvlUp += () =>
        {
            Sequence s = DOTween.Sequence();
            s.Append(GetComponent<RectTransform>().DOAnchorPosX(220, .3f));
            s.AppendInterval(1.2f);
            s.Append(GetComponent<RectTransform>().DOAnchorPosX(-270, .3f));
            s.PlayForward();
        };
    }
}
