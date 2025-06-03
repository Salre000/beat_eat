using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    //デバッグ用
    public TextMeshProUGUI[] m_TextMeshProUGUI;
    public TextMeshProUGUI _TextMeshProUGUI;


    private List<Hands> hand = new List<Hands>(12);
    private List<List<System.Action>> handEndAction = new List<List<System.Action>>(12);

    private bool commandMouse = false;
    public void AddEndAction(System.Action action, int ID)
    {
        handEndAction[ID].Add(action);

    }
    public List<Hands> GetHand() { return hand; }
    public Vector2 GetPosition(int id) { return hand[id].HandPosition; }

    private int touchPoint = 12;

    public struct Hands
    {
        public Vector2 HandPosition;
        public int ID;
        public bool flag;
    }

    public void Awake()
    {
        HandUtility.handManager = this;

        for (int i = 0; i < 12; i++)
        {
            Hands h = new Hands();
            h.ID = i;
            h.flag = true;
            h.HandPosition = Vector2.zero;
            hand.Add(h);

            List<System.Action> actions = new List<System.Action>();
            handEndAction.Add(actions);

        }

        CreateTapArea.textMeshProUGUI = _TextMeshProUGUI;
    }
    public int SetHand(Vector2 vector2)
    {
        for (int i = 0; i < hand.Count; i++)
        {
            if (hand[i].flag) continue;

            Hands hands = hand[i];
            hands.HandPosition = vector2;
            hands.flag = true;
            hand[i] = hands;

            return i;
        }
        return -1;
    }

    private void AddHand()
    {

        List<Vector2> touchPosition = new List<Vector2>();
        touchPoint = Input.touchCount;

        if (!commandMouse)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                touchPosition.Add(Input.GetTouch(i).position);
            }
        }
        else
        {
            touchPosition.Add(Input.mousePosition);
        }

        for (int i = 0; i < touchPosition.Count; i++)
        {
            for (int j = 0; j < hand.Count; j++)
            {
                if (!hand[j].flag) continue;


                if (Vector2.Distance(hand[j].HandPosition, touchPosition[i]) > 10) continue;
                touchPosition.RemoveAt(i);
                i--;
                break;
            }
        }

        _TextMeshProUGUI.text = touchPosition.Count.ToString()+touchPosition[0].y.ToString();

        for (int i = 0; i < touchPosition.Count; i++)
        {
            SetHand(touchPosition[i]);

        }


    }
    private void SbuHand()
    {
        List<Vector2> touchPosition = new List<Vector2>();
        touchPoint = Input.touchCount;

        if (!commandMouse)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                touchPosition.Add(Input.GetTouch(i).position);
            }
        }
        else
        {
            touchPosition.Add(Input.mousePosition);
        }

        int sbuID = -1;
        float miniRange = 0;

        for (int p = 0; p < hand.Count - touchPosition.Count; p++)
        {

            for (int i = 0; i < hand.Count; i++)
            {
                if (!hand[i].flag) continue;

                float renge = 1000;

                for (int j = 0; j < touchPosition.Count; j++)
                {
                    if (Vector2.Distance(touchPosition[j], hand[i].HandPosition) > renge) continue;

                    renge = Vector2.Distance(touchPosition[j], hand[i].HandPosition);


                }

                if (miniRange > renge) continue;
                miniRange = renge;
                sbuID = i;

            }

            Hands hands = hand[sbuID];
            hands.flag = false;
            hand[sbuID] = hands;
            for (int i = 0; i < handEndAction[sbuID].Count; i++)
            {
                handEndAction[sbuID][i]();

            }
            handEndAction[sbuID].Clear();
        }
    }

    private void PositionUpdata()
    {
        List<Vector2> touchPosition = new List<Vector2>();

        if (!commandMouse)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                touchPosition.Add(Input.GetTouch(i).position);
            }
        }
        else
        {
            touchPosition.Add(Input.mousePosition);
        }

        for (int i = 0; i < hand.Count; i++)
        {
            if (!hand[i].flag) continue;

            float renge = 1000;

            int chengeID = -1;

            for (int j = 0; j < touchPosition.Count; j++)
            {
                if (Vector2.Distance(touchPosition[j], hand[i].HandPosition) >= renge) continue;

                renge = Vector2.Distance(touchPosition[j], hand[i].HandPosition);

                chengeID = j;
            }

            if (chengeID < 0) continue;
            Hands hands = hand[i];
            hands.HandPosition = touchPosition[chengeID];
            hand[i] = hands;


        }


    }

    public void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.Alpha1) && !commandMouse)
        {
            commandMouse = !commandMouse;
        }
        if (!commandMouse) CheckHand();
        PositionUpdata();

        //for (int i = 0; i < Input.touchCount; i++)
        //{
        //    m_TextMeshProUGUI[i].text =Input.GetTouch(i).position.ToString();
        //}
        for (int i = 0; i < hand.Count; i++)
        {
            m_TextMeshProUGUI[i].text = hand[i].HandPosition.ToString() + hand[i].flag.ToString() + "ID" + i.ToString();
        }

        if (Input.GetMouseButton(0) && commandMouse)
        {
            touchPoint = 1;

            Hands hands = hand[0];
            hands.flag = true;
            hand[0] = hands;

        }
        else if (commandMouse)
        {
            touchPoint = 0;

            if (hand[0].flag)
            {
                for (int i = 0; i < handEndAction[i].Count; i++)
                {
                    handEndAction[0][i]();

                }
                handEndAction[0].Clear();

            }


            Hands hands = hand[0];
            hands.flag = false;
            hand[0] = hands;

        }
    }

    private void CheckHand()
    {
        if (touchPoint > Input.touchCount)
        {
            SbuHand();
        }

        if (touchPoint < Input.touchCount)
        {
            AddHand();
        }
    }




}
