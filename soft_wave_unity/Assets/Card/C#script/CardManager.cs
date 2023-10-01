using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;
using System.Threading;

public class CardManager : MonoBehaviour
{
    public List<int> deck = new List<int>();    // �÷��̾ ������ ��
    public int[] hands = new int[5];            // �÷��̾ �տ� ��� �ִ� ��
    public int drawIndex = 0;                   // �̹� ��ο쿡�� ���� ī�� �ε���

    private static CardManager _instance;       // �̱��� ���� ������

    public static CardManager Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        _instance = this;
        
        deck = new List<int>() { 1, 1, 1, 2, 2, 3, 3, 3, 3, 3 };    // �׽�Ʈ �������� ������ ���� �����ϵ��� ��
    }

    void Update()
    {
        // �׽�Ʈ ����
        // 1-5 Ű�� ������ �տ� ī�带 ��ο�
        // 6-0 Ű�� ������ �տ� �� ī�带 ��
        if (Input.GetKeyDown(KeyCode.Alpha1)) DrawCard(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) DrawCard(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) DrawCard(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) DrawCard(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) DrawCard(4);

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
    }

    // ī�� �̱� ����
    void DrawCard(int handIndex)
    {
        if (drawIndex == deck.Count())              // ���� �ε����� �������� ������ ���
        {
            ShuffleDeck();                          // ���� ������ ��
            drawIndex = 0;                          // ó������ �ٽ� ����
        }

        CardUIManager.Instance.DrawCard(handIndex, deck[drawIndex]);  // UI�� ī�� �̱� �Լ��� ȣ��
        hands[handIndex] = deck[drawIndex++];       // �տ� �� �� ���� ��, drawIndex ����
    }

    // �� ����
    // �Ǽ�-������(Fisher-Yate) ���� ������� ����
    void ShuffleDeck()
    {
        for (int i = deck.Count() - 1; i > 1; i--) {
            int rand = Random.Range(0, i + 1);
            int temp = deck[rand];
            deck[rand] = deck[i];
            deck[i] = temp;
        }
    }
}
