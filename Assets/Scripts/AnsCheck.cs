using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
using Unity.VisualScripting;
using UnityEngine.UI;
using TMPro;

namespace motoshin
{
        
    public class AnsCheck : MonoBehaviour
    {
        private int time;
        private int Trun;
        private List<int> Question = new List<int> { };
        private int A;//根據玩家輸入的結果有所變動，是玩家輸入的數字對位置也對的代表
        private int B;//根據玩家輸入的結果有所變動，是玩家輸入的數字對位置不對的代表

        public TextMeshProUGUI WinTrun;
        public TextMeshProUGUI TurnWord;

        //這部分是建立Prefab用
        public GameObject[] Result;
        public GameObject Prefab; // 要生成的 prefab
        public GameObject TextObject;
        public GameObject MissObject;
        public GameObject ClearWin;
        public GameObject FailWin;
        public Transform ParentTransform; // 父物件

        private void Awake()
        {
            
            while (Question.Count < 5)
            {
                int Num = UnityEngine.Random.Range(0, 10); // 生成 0 到 9 的亂數
                Debug.Log($"亂數出的數字:{Num}");
                
                if (!Question.Contains(Num)) // 確保不重複
                {
                    Question.Add(Num);
                    foreach (int i in Question)
                    {
                    Debug.Log($"list內資料:{i}");
                    }
                }
            }
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
            
            if (time < Trun && Ans.Length == 4)
            {
                time++;
                A = 0;
                B = 0;
                TurnWord.text = (Trun - time).ToString();
                for (int i = 3; i >= 0; i--)
                {
                    for (int n = 3; n >= 0; n--)
                    {
                        
                        if (Ans[i] == Question[n])
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
                Vector3 NewResultPosition = TextObject.transform.position/*startPosition*/ + new Vector3(360 * ((time - 1) / 10), -110 * ((time - 1) % 10), 0);//transform.position = new Vector3( x, y, z );
                                                                                                                                                                     // 生成物件
                GameObject NewResultText = Instantiate(Prefab, NewResultPosition, Quaternion.identity, ParentTransform);
                TextMeshProUGUI NewResultComponent = NewResultText.GetComponent<TextMeshProUGUI>();
                NewResultComponent.text = $" {time}. {EnterNum.text} : {A}A{B}B"; // 設定test屬性
                if (A == 4)
                {
                    WinTrun.text = time.ToString();
                    ClearWin.SetActive(true);
                }
                else if ((Trun - time) == 0)
                {
                    FailWin.SetActive(true);
                }
            }
            else
            {
                MissObject.SetActive(true);
            }


        }
    }
}


