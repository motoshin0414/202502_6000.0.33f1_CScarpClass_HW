using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace motoshin
{
    public class Main : MonoBehaviour
    {
        public GameObject InterboxAndResultArea;    //對話框及結果區
        public GameObject LogoAndStartButtom;       //標題及開始紐
        public TextMeshProUGUI TurnWord;            //顯示還有幾回合的文字

        int count;
        int Time;                         //可使用回合數
        int Trun;                         //現在回合
        int A,B;                          //幾A幾B
        private List<int> QuestionList;
        private List<int> AnswerList;

        public void GameStart()
        {
            InterboxAndResultArea.SetActive(true);
            LogoAndStartButtom.SetActive(false);

            while (count < 4)
            {
                int Num = UnityEngine.Random.Range(0, 10);  // 生成 0 到 9 的亂數
                //Debug.Log(Num);                           //輸出亂數

                if (!QuestionList.Contains(Num)) // Contains:確保不重複
                {
                    QuestionList.Add(Num);
                    count++;
                }
            }
            //輸出測試內容
            /*foreach (int i in QuestionList)
            {
                Debug.Log($"<color=#F61>{i}</color>");
            }*/
        }

        public void AnsEnter(Text EnterNum)
        {
            /*Debug.Log(EnterNum.text);*/
            int AnsNum = int.Parse(EnterNum.text);
            //以下是將數字存進清單內的程式碼
            for (int i = 0; i < 4; i++)
            {
                int dataNum = AnsNum % 10; // 取得最後一位數字
                AnsNum /= 10; // 去掉最後一位數字

                // 確保數字不重複
                if (!AnswerList.Contains(dataNum))
                {
                    AnswerList.Add(dataNum); // 添加到 List 中
                }
            }
            AnswerList.Reverse();
            int[] Ans = AnswerList.ToArray();
            //到這裡結束
            if (Trun < Time && Ans.Length == 4)
            {
                Trun++;
                A = 0;
                B = 0;
                TurnWord.text = (Time - Trun).ToString();
                for (int i = 3; i >= 0; i--)
                {
                    /*Ans[i] = AnsNum % 10;
                    AnsNum = AnsNum / 10;
                    //Debug.Log(Ans[i]);*/
                    for (int n = 3; n >= 0; n--)
                    {
                        Debug.Log("Ans[" + i + "]:" + Ans[i] + ", Q[" + n + "]:" + QuestionList[n]);
                        if (Ans[i] == QuestionList[n])
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
                /*Vector3 NewResultPosition = TextObject.transform.position + new Vector3(360 * ((AnsTime - 1) / 10), -110 * ((AnsTime - 1) % 10), 0);//transform.position = new Vector3( x, y, z );
                                                                                                                                                                     // 生成物件
                GameObject NewResultText = Instantiate(Prefab, NewResultPosition, Quaternion.identity, ParentTransform);
                TextMeshProUGUI NewResultComponent = NewResultText.GetComponent<TextMeshProUGUI>();
                NewResultComponent.text = $" {AnsTime}. {EnterNum.text} : {A}A{B}B"; // 設定test屬性
                if (A == 4)
                {
                    WinTrun.text = AnsTime.ToString();
                    ClearWin.SetActive(true);
                }
                else if ((Trun - AnsTime) == 0)
                {
                    FailWin.SetActive(true);
                }*/
            }
            else
            {
                //MissObject.SetActive(true);
            }


        }

    }

}
