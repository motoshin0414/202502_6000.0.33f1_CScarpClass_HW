using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System;


namespace motoshin
{
    public class Main : MonoBehaviour
    {
        [SerializeField,Header("初始與遊戲開始後的物件")]
        public GameObject InterboxAndResultArea;    //對話框及結果區
        public GameObject LogoAndStartButtom;       //標題及開始紐
        public TextMeshProUGUI TurnWord;            //顯示還有幾回合的文字
        [SerializeField, Header("文字生成")]
        public GameObject TextObject;               //預設第一個生成幾A幾B結果物件(用於校準結果位置)
        public GameObject TextPrefab;               //產生出幾A幾B的預置物
        public TextMeshProUGUI WinTrun;             //顯示你花了幾回合完成
        public Transform ParentTransform;           //獲取父物件的位置
        public InputField ResultInputField;
        [SerializeField, Header("結束物件")]
        public GameObject ClearWin;                 //顯示你贏了的物件
        public GameObject FailWin;                  //顯示你輸了的物件
        public GameObject ErrorWin;                 //輸入有誤的物件
        //[SerializeField, Header("程式運用變數")]
        int count = 0;
        int Time = 40;                           //可使用回合數
        int Trun = 0;                           //現在回合
        int A,B = 0;                            //幾A幾B
        int High = 30;
        private List<int> QuestionList = new List<int>();     //電腦生成的問題
        private List<int> AnswerList = new List<int>();       //玩家回答的答案

        public void GameStart()
        {
            InterboxAndResultArea.SetActive(true);
            LogoAndStartButtom.SetActive(false);
            TurnWord.text = Time.ToString();

            while (count < 4)
            {
                int Num = UnityEngine.Random.Range(0, 10);  // 生成 0 到 9 的亂數
                Debug.Log(Num);                           //輸出亂數

                if (!QuestionList.Contains(Num)) // Contains:確保不重複
                {
                    QuestionList.Add(Num);
                    count++;
                }
            }
            //輸出測試內容
            foreach (int i in QuestionList)
            {
                Debug.Log($"<color=#F61>{i}</color>");
            }
        }

        public void AnsEnter(Text EnterNum)
        {
            /*Debug.Log(EnterNum.text);*/
            int AnsNum = int.Parse(EnterNum.text);
            AnswerList.Clear();
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
            foreach (int i in AnswerList)
            {
                Debug.Log($"<color=#95f>{i}</color>");
            }
            AnswerList.Reverse();       //反轉陣列內容
            //int[] Ans = AnswerList.ToArray();
            //到這裡結束
            if (Trun < Time && AnswerList.Count == 4)
            {
                Trun++;
                A = 0;
                B = 0;
                TurnWord.text = (Time - Trun).ToString();
                for (int i = 3; i >= 0; i--)
                {
                    for (int n = 3; n >= 0; n--)
                    {
                        Debug.Log("Ans[" + i + "]:" + AnswerList[i] + ", Q[" + n + "]:" + QuestionList[n]);
                        if (AnswerList[i] == QuestionList[n])
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
                Vector3 NewResultPosition = TextObject.transform.position + new Vector3(270 * ((Trun - 1) / 10), -45 * ((Trun - 1) % 10), 0);//transform.position = new Vector3( x, y, z );

                // 生成物件
                // 生成新的文字物件 (TextPrefab)  
                // 位置：NewResultPosition  
                // 旋轉：Quaternion.identity (無旋轉)  
                // 父物件：ParentTransform (將其設置為指定的父物件，方便管理)
                GameObject NewResultText = Instantiate(TextPrefab, NewResultPosition, Quaternion.identity, ParentTransform);
                TextMeshProUGUI NewResultComponent = NewResultText.GetComponent<TextMeshProUGUI>();
                NewResultComponent.text = $" {Trun}. {EnterNum.text} : {A}A{B}B"; // 設定test屬性
                if (A == 4)
                {
                    WinTrun.text = Trun.ToString();
                    ClearWin.SetActive(true);
                }
                else if ((Time - Trun) == 0)
                {
                    FailWin.SetActive(true);
                }
            }
            else
            {
                ErrorWin.SetActive(true);
            }
            OnSubmitEvent();                //將輸入框內的內容清除


        }

        void OnSubmitEvent()
        {
            ResultInputField.text = "";
        }
    }

}
