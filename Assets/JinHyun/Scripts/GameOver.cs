using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _tier;
    [SerializeField] private TextMeshProUGUI _time;

    

    void Start()
    {
        _tier.text = "Tier\n"+GetCapacityTier(ScoreManager.Instance.CurrentScore);
        _time.text = ((int)ScoreManager.Instance.CurrentTime).ToString() + "\n SEC";
    }

    public string GetCapacityTier(long score)
    {
        if(score / (1000*1000*1000) > 0)
        {
            return "PB";
        }
        else if(score / (1000*1000) > 0)
        {
            return "TB";
        }
        else if(score / 1000 > 0)
        {
            return "GB";
        }
        return "MB";
    }
}
