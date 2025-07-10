using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class AchievementUIMove : MonoBehaviour
{
    Vector3 thisPos;
    [SerializeField] Transform EndPos;
    private Image image;

    [SerializeField] TextMeshProUGUI achievementname;
    [SerializeField] Image achievementimage;
    private void Start()
    {
        image= GetComponent<Image>();
        thisPos = image.rectTransform.position;

        AchievementsBase achievements = 
            Resources.Load<AchievementsAll>("Achievements/AchievementsAll").achievements[AchievementStatus.achievementNumber];

        achievementname.text = achievements.AchievementsName;

        achievementimage.sprite=achievements.AchievementsImage;

    }
    public void OnDisable()
    {
        AchievementStatus.achievementNumber = -1;
    }
    float time = 0;
    bool flag = false;  
    public void FixedUpdate()
    {
        time += Time.deltaTime;


        image.rectTransform.position=Vector3.Lerp(thisPos, EndPos.position, time);


        if (time < 2) return;
        if(flag) SceneManager.UnloadSceneAsync("AchievementScene");


        Vector3 pos = thisPos;

        thisPos = EndPos.position;
        EndPos.position = pos;
        time = 0;

        flag=true;



    }

}
