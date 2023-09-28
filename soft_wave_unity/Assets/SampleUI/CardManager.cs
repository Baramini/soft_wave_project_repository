using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public int[] deck = new int[CardInfo.size]; // �÷��̾ ������ ��
    public int[] hands = new int[5];            // �÷��̾ �տ� ��� �ִ� ��

    private static CardManager _instance;       // �̱��� ���� ������

    public static CardManager Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        _instance = this;

        deck = new int[3] { 10, 10, 10 };       // �׽�Ʈ �������� ������ ���� �����ϵ��� ��
    }

    void Update()
    {   
        // �׽�Ʈ ����
        // 1-5 Ű�� ������ �տ� ī�带 ��ο�
        // 6-0 Ű�� ������ �տ� �� ī�带 ��
        if (Input.GetKeyDown(KeyCode.Alpha1)) DrawCard(0, (int)CardInfo.CARD.ATTACK);
        if (Input.GetKeyDown(KeyCode.Alpha2)) DrawCard(1, (int)CardInfo.CARD.DEFENSE);
        if (Input.GetKeyDown(KeyCode.Alpha3)) DrawCard(2, (int)CardInfo.CARD.ROLL);
        if (Input.GetKeyDown(KeyCode.Alpha4)) DrawCard(3, (int)CardInfo.CARD.ATTACK);
        if (Input.GetKeyDown(KeyCode.Alpha5)) DrawCard(4, (int)CardInfo.CARD.DEFENSE);

        if (Input.GetKeyDown(KeyCode.Alpha6)) UseCard(0);
        if (Input.GetKeyDown(KeyCode.Alpha7)) UseCard(1);
        if (Input.GetKeyDown(KeyCode.Alpha8)) UseCard(2);
        if (Input.GetKeyDown(KeyCode.Alpha9)) UseCard(3);
        if (Input.GetKeyDown(KeyCode.Alpha0)) UseCard(4);
    }

    // ī�� ��� ����
    void UseCard(int handIndex)
    {
        CardUIManager.Instance.Discard(handIndex);  // UI�� ī�� ��� �Լ��� ȣ��
        deck[hands[handIndex]]++;                   // ���� �տ� �ִ� ī�带 ���� ��ȯ
    }

    // ī�� �̱� ����
    void DrawCard(int handIndex, int cardIndex)
    {
        if (!(deck[cardIndex] > 0))                 // �������� ���� ī�带 �������� �� ���
        {
            Debug.Log("�������� ���� ī�带 �������� ��.");
            return;
        }

        deck[cardIndex]--;                                      // ������ ī�带 �ϳ� ������
        CardUIManager.Instance.DrawCard(handIndex, cardIndex);  // UI�� ī�� �̱� �Լ��� ȣ��
    }

    // ���� ī���� ���� ����� ������ �ε����� ��ȯ�ϴ� �Լ�
    // ���� ī���� �ε����� ���� �� ����� �� ����
    public int GetRandomIndex()
    {
        int total = 0;
        for(int i = 0; i < deck.Length; i++)
        {
            total += deck[i];
        }

        int index = 0;
        int rand = Random.Range(1, total + 1);

        while (deck[index] < rand)
        {
            rand -= deck[index];
            index++;
        }

        return index;
    }
}
