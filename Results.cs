using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Results : MonoBehaviour
{
    /// <summary>
    /// This script send information on server about this fight
    /// 1. Send opponent's place.
    /// 2. Send player's place
    /// 3. Turn on opponent's cooldown
    /// 4. Turn on player's cooldown
    /// 5. Increase winner's level
    /// 6. Give to winner 10% of opponent's money + reward.
    /// 
    /// </summary>

    int opponent_result;

    void Start()
    {
        
    }

    private IEnumerator Get_Level()//remake
    {
        WWWForm form = new WWWForm();
        form.AddField("id", PlayerPrefs.GetInt("opponent_id").ToString());


        WWW www = new WWW("http://doublenikmak.ru/Test_State/System/Get_Level.php", form);

        yield return www;
        if (www.error != null)
        {
            Debug.Log("Ïðîèçîøëà îøèáêà: " + www.error);
            yield break;
        }

        else
        {
            PlayerPrefs.SetInt("level", Convert.ToInt32(www.text));
            StartCoroutine(Get_Damage());
        }
    }

}
