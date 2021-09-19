using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : MonoBehaviour
{
    /// <summary>
    /// This script does result of fight, count damage and health and load all information on the server
    /// 1. Get from PlayerPrefs player's damage
    /// 2. Get from PlayerPrefs opponent's health
    /// 3. Get from PlayerPrefs opponent's id
    /// 4. Get from PlayerPrefs player's id
    /// 5. Find who is wining and who is losing
    /// 6. Send on server information about percentages
    /// 7. Show animation
    /// </summary>

    int damage_player;
    int health_opponent;

    int id_player;
    int id_opponent;

    int result;

    float percentages;

    void Start()
    {
        damage_player = PlayerPrefs.GetInt("damage");
        health_opponent = PlayerPrefs.GetInt("opponent_health");

        id_player = PlayerPrefs.GetInt("id");
        id_opponent = PlayerPrefs.GetInt("opponent_id");

        result = health_opponent - damage_player;

        if(result == 0)
        {
            PlayerPrefs.SetInt("result", 2);
        }
        if(result > 0)
        {
            PlayerPrefs.SetInt("result", 0);
        }
        if(result < 0)
        {
            PlayerPrefs.SetInt("result", 1);
        }

        StartCoroutine(Send_Result());

        Animation();
    }

    void Animation()
    {
        //show animation
        //change scene
        //percentages
    }

    private IEnumerator Send_Result()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", PlayerPrefs.GetInt("id").ToString());
        form.AddField("opponent_id", PlayerPrefs.GetInt("opponent_id").ToString());
        form.AddField("per", percentages.ToString());

        WWW www = new WWW("http://doublenikmak.ru/Test_State/System/Persentage.php", form);

        yield return www;
        if (www.error != null)
        {
            Debug.Log("Error: " + www.error);
            yield break;
        }
        else
        {
            yield break;
        }
    }
}
