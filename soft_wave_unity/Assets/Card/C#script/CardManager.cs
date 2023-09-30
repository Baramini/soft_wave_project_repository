using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

/* 10/1 00:50 �������� - �۰��
 * �����迭�̾��� deck�� ��ųʸ��� ����
 * deck�� key�� ī���� ���� ID (CardDB.csv ����)
 * deck�� value�� �ش� ī���� ����
 * Update���� ������ ī�带 ��ο��ϵ��� ����
 * UseCard�޼ҵ��� ī�� ���� ���� �ڵ带 ����
 * DrawCard�޼ҵ��� ī�� ���� ���� �ڵ带 ����, ������� �ʾҴ� hands�迭�� ����ϵ��� ����
 * GetRandomIndex�� �ڵ� ���� ����
 */
public class CardManager : MonoBehaviour
{
    [SerializeField] public Dictionary<int, int> deck;   // �÷��̾ ������ ��
    public int[] hands = new int[5];            // �÷��̾ �տ� ��� �ִ� ��

    private static CardManager _instance;       // �̱��� ���� ������

    public static CardManager Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        _instance = this;

        deck = new Dictionary<int, int> { { 1, 10 }, { 2, 10 }, { 3, 10 } };       // �׽�Ʈ �������� ������ ���� �����ϵ��� ��
    }

    void Update()
    {
        // �׽�Ʈ ����
        // 1-5 Ű�� ������ �տ� ī�带 ��ο�
        // 6-0 Ű�� ������ �տ� �� ī�带 ��
        if (Input.GetKeyDown(KeyCode.Alpha1)) DrawCard(0, GetRandomIndex());
        if (Input.GetKeyDown(KeyCode.Alpha2)) DrawCard(1, GetRandomIndex());
        if (Input.GetKeyDown(KeyCode.Alpha3)) DrawCard(2, GetRandomIndex());
        if (Input.GetKeyDown(KeyCode.Alpha4)) DrawCard(3, GetRandomIndex());
        if (Input.GetKeyDown(KeyCode.Alpha5)) DrawCard(4, GetRandomIndex());

        if (Input.GetKeyDown(KeyCode.Alpha6)) UseCard(0);
        if (Input.GetKeyDown(KeyCode.Alpha7)) UseCard(1);
        if (Input.GetKeyDown(KeyCode.Alpha8)) UseCard(2);
        if (Input.GetKeyDown(KeyCode.Alpha9)) UseCard(3);
        if (Input.GetKeyDown(KeyCode.Alpha0)) UseCard(4);
    }

    // ī�� ��� ����
    void UseCard(int handIndex)
    {
        CardUIManager.Instance.Discard(handIndex);              // UI�� ī�� ��� �Լ��� ȣ��
        deck[hands[handIndex]] = deck[hands[handIndex]] +1;     // ���� �տ� �ִ� ī�带 ���� ��ȯ
    }

    // ī�� �̱� ����
    void DrawCard(int handIndex, int cardIndex)
    {
        if (!(deck[cardIndex] > 0))     // �������� ���� ī�带 �������� �� ���
        {
            Debug.Log("�������� ���� ī�带 �������� ��.");
            return;
        }

        deck[cardIndex] = deck[cardIndex] - 1;                      // ������ ī�带 �ϳ� ������
        CardUIManager.Instance.DrawCard(handIndex, cardIndex);      // UI�� ī�� �̱� �Լ��� ȣ��
        hands[handIndex] = cardIndex;
        Debug.Log($"{deck[1]}, {deck[2]}, {deck[3]}");              // ��ųʸ��� �ν�����â�� ǥ�ð� �ȵż� ���� �ڵ�
    }

    // ���� ī���� ���� ����� ������ �ε����� ��ȯ�ϴ� �Լ�
    // ���� ī���� �ε����� ���� �� ����� �� ����
    public int GetRandomIndex()
    {
        int total = 0;                              // total���� ���� �ִ� ��� ī���� ������ �����
        foreach(int value in deck.Values)           // ī���� ������ ����� deck.Values�� ��ȸ�ϸ� total�� ��� ī���� ������ ����
            total += value;

        int rand = Random.Range(1, total + 1);      // 1~���ī���ǰ��� ������ ���� �ϳ��� ����
        foreach(int key in deck.Keys)               // deck�� ��ȸ�ϸ� ������ �������� value�� ��
        {
            if (rand <= deck[key])                  // ��ħ�� rand�� value���� �۰� ���� key�� ���õ�
                return key;
            rand -= deck[key];
        }

        return -1;                                  // ��� ī�带 �Ҹ��ϸ� ���� �߻�
    }
}
