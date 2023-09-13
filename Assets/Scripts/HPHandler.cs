using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPHandler : MonoBehaviour
{
    public Image Team2Target1Healthbar;
    public Image Team2Target2Healthbar;
    public Image Team2Target3Healthbar;
    public Image Team1Target1Healthbar;
    public Image Team1Target2Healthbar;
    public Image Team1Target3Healthbar;
    
    public TextMeshProUGUI Team1HP;
    public TextMeshProUGUI Team2HP;
    
    void Update()
    {
        Team1HP.SetText(((int)((Team1Target1Healthbar.fillAmount + Team1Target2Healthbar.fillAmount + Team1Target3Healthbar.fillAmount)/3*100)).ToString());
        Team2HP.SetText(((int)((Team2Target1Healthbar.fillAmount + Team2Target2Healthbar.fillAmount + Team2Target3Healthbar.fillAmount)/3*100)).ToString());
    }
}
