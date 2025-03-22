using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
using Unity.VisualScripting;
using UnityEngine.UI;
using TMPro;
//using Newtonsoft.Json.Linq;

public class DifficultyButton : MonoBehaviour
{
    //這部分是基礎的設定用
    public static int Trun= 20;
    int AnsTime = 0;
    static int[] Q = new int[4];// 用來存放四個不重複亂數的陣列
    int A;
    int B;
    //這部分是建立Prefab用
    public GameObject[] Result;
    public GameObject Prefab; // 要生成的 prefab
    public GameObject TextObject;
    public GameObject MissObject;
    public TextMeshProUGUI TurnWord;
    public GameObject ClearWin;
    public GameObject FailWin;
    public TextMeshProUGUI WinTrun;
    public Transform ParentTransform; // 父物件 

    // Start is called before the first frame update
    /// <summary>
    /// 點擊難度後設定回合數
    /// </summary>
    /// <param name="SenceNum"></param>
    public void Buttompush( int SenceNum)
    {

        /*SceneManager.LoadScene(SenceNum);//轉換場景以及設定難度的回合數
        if (SenceNum == 2) //難度Easy
        { 
            Trun = 15;
        }else if (SenceNum == 3)//難度Normal
        {
            Trun = 10;
        }
        else if (SenceNum == 4)//難度Hard
        {
            Trun = 6;
        }
        else if (SenceNum == 5)//難度Endless
        {
            Trun = 20;
        }*/

        // 建立隨機數生成器

        // 用來存放四個不重複亂數的陣列
        int count = 0;
        Array.Clear(Q, 0, Q.Length);
        Array.Fill(Q, -1);

        // 使用迴圈生成不重複的亂數
        while (count < 4)
        {
            int Num = UnityEngine.Random.Range(0, 10); // 生成 0 到 9 的亂數
            Debug.Log(Num);

            if (!Q.Contains(Num)) // 確保不重複
            {
                Q[count] = Num;
                count++;
            }
        }

        Debug.Log("產生的亂數為: "  + string.Join(", ", Q));
    }

    public void AnsEnter(Text EnterNum)
    {
        /*Debug.Log(EnterNum.text);*/
        int AnsNum = int.Parse(EnterNum.text);
        //以下是將數字存進清單內的程式碼
        List<int> AnsList = new List<int>(); // 使用 List 替代陣列

        for (int i = 0; i < 4; i++)
        {
            int dataNum = AnsNum % 10; // 取得最後一位數字
            AnsNum /= 10; // 去掉最後一位數字

            // 確保數字不重複
            if (!AnsList.Contains(dataNum))
            {
                AnsList.Add(dataNum); // 添加到 List 中
            }
        }
        AnsList.Reverse();
        int[] Ans = AnsList.ToArray();
        //到這裡結束
        if (AnsTime < Trun  && Ans.Length == 4)
        {
            AnsTime++;
            A = 0;
            B = 0;
            TurnWord.text = (Trun - AnsTime).ToString();
            for (int i = 3; i >= 0; i--)
            {
                /*Ans[i] = AnsNum % 10;
                AnsNum = AnsNum / 10;
                //Debug.Log(Ans[i]);*/
                for (int n = 3; n >= 0; n--)
                {
                    Debug.Log("Ans[" + i + "]:" + Ans[i] + ", Q[" + n + "]:" + Q[n]);
                    if (Ans[i] == Q[n])
                    {
                        if (i == n)
                        {
                            A++;
                            break;
                        }
                        else
                        {
                            B++;
                            break;
                        }
                    }
                }
            }
            Vector3 NewResultPosition = TextObject.transform.position/*startPosition*/ + new Vector3(360*((AnsTime-1)/10), -110 * ((AnsTime-1)%10), 0);//transform.position = new Vector3( x, y, z );
            // 生成物件
            GameObject NewResultText = Instantiate(Prefab, NewResultPosition, Quaternion.identity, ParentTransform);
            TextMeshProUGUI NewResultComponent = NewResultText.GetComponent<TextMeshProUGUI>();
            NewResultComponent.text = $" {AnsTime}. {EnterNum.text} : {A}A{B}B"; // 設定test屬性
            if(A == 4)
            {
                WinTrun.text = AnsTime.ToString();
                ClearWin.SetActive(true);
            }
            else if((Trun - AnsTime) == 0) 
            {
                FailWin.SetActive(true);
            }
        }
        else
        {
            MissObject.SetActive(true) ;
        }
        

    }
}
