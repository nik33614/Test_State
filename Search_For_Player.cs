using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Search_For_Players : MonoBehaviour
{
    /// <summary>
    /// This script does auto_searach for players with the same level, who were in the game ater cooldown.
    /// 1. Get Level of Player from server(id -> level)
    /// 2. Get Damage of Player from server(id -> damage)
    /// 3. Send information of level on server(id -> id of opponent)
    /// 4. Get Opponent's health(id -> health)
    /// 5. Show animation
    /// </summary>

    void Start()
    {
        StartCoroutine(Get_Level());
    }

    private IEnumerator Get_Level()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", PlayerPrefs.GetInt("id").ToString());


        WWW www = new WWW("http://doublenikmak.ru/Test_State/System/Get_Level.php", form);

        yield return www;
        if (www.error != null)
        {
            Debug.Log("Произошла ошибка: " + www.error);
            yield break;
        }

        else
        {
            PlayerPrefs.SetInt("level", Convert.ToInt32(www.text));
            StartCoroutine(Get_Damage());
        }
    }

    private IEnumerator Get_Damage()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", PlayerPrefs.GetInt("id").ToString());


        WWW www = new WWW("http://doublenikmak.ru/Test_State/System/Get_Damage.php", form);

        yield return www;
        if (www.error != null)
        {
            Debug.Log("Произошла ошибка: " + www.error);
            yield break;
        }

        else
        {
            PlayerPrefs.SetInt("damage", Convert.ToInt32(www.text));
            StartCoroutine(Send_Level());
        }
    }

    private IEnumerator Send_Level()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", PlayerPrefs.GetInt("id").ToString());


        WWW www = new WWW("http://doublenikmak.ru/Test_State/System/Find_Opponent.php", form);

        yield return www;
        if (www.error != null)
        {
            Debug.Log("Произошла ошибка: " + www.error);
            yield break;
        }

        else
        {
            PlayerPrefs.SetInt("id_opponent", Convert.ToInt32(www.text));
            StartCoroutine(Get_Health());
        }
    }

    private IEnumerator Get_Health()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", PlayerPrefs.GetInt("id_opponent").ToString());


        WWW www = new WWW("http://doublenikmak.ru/Test_State/System/Get_Health.php", form);

        yield return www;
        if (www.error != null)
        {
            Debug.Log("Произошла ошибка: " + www.error);
            yield break;
        }

        else
        {
            PlayerPrefs.SetInt("opponent_health", Convert.ToInt32(www.text));
            Animation();
        }
    }

    void Animation()
    {
        //show animation
        //change to the next level
    }
}
