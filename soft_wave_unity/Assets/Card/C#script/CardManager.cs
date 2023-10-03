using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Threading;

public class CardManager : MonoBehaviour
{
    public List<int> deck = new List<int>();            // �÷��̾ ������ ��
    public int[] hands = new int[5];                    // �÷��̾ �տ� ��� �ִ� ��
    public int drawIndex = 0;                           // �̹� ��ο쿡�� ���� ī�� �ε���
    public Image[] staminaCircle;                       // ���¹̳��� ������ UI
    public float stamina;                               // ���� ���¹̳�
    [SerializeField] public Slider[] handCooltime;      // ���� ��Ÿ��(ī�带 ����� �� �ٽ� �̱������ �ð�)�� ������ �����̴�

    private static CardManager _instance;       // �̱��� ���� ������

    public static CardManager Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        _instance = this;
        
        deck = new List<int>() { 1, 1, 1, 2, 2, 3, 3, 3, 3, 3 };    // �׽�Ʈ �������� ������ ���� �����ϵ��� ��

        // ���׹̳� �ʱ�ȭ
        foreach (Image circle in staminaCircle) 
            circle.fillAmount = 0;
    }

    void Update()
    {
        stamina = Mathf.Min(stamina+Time.deltaTime, staminaCircle.Length);      // ���¹̳� ȸ��

        // �׽�Ʈ ����
        // 1-5 Ű�� ������ �տ� �� ī�带 ��
        if (Input.GetKeyDown(KeyCode.Alpha1)) UseCard(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) UseCard(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) UseCard(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) UseCard(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) UseCard(4);

        // �� ���� ��Ÿ�� ���� && �� á���� ��ο�
        for (int handIndex = 0; handIndex < 5; handIndex++)                     
        {
            if (handCooltime[handIndex].value > 0) handCooltime[handIndex].value -= Time.deltaTime;     // ��Ÿ�� ����
            else if (CardUIManager.Instance.cardsInHands[handIndex] == null) DrawCard(handIndex);       // �� �и� Ȯ���ϰ� ��ο�
        }

        for (int i = 0; i < staminaCircle.Length; i++)      // ���¹̳� UI ����
            staminaCircle[i].fillAmount = stamina - i;        
    }

    // ī�� ��� ����
    void UseCard(int handIndex)
    {
        if (CardUIManager.Instance.cardsInHands[handIndex] == null) return;     // �и� ������ �ִ��� �˻�
        if (stamina < 1) return;                                                // ���¹̳��� ������� �˻�

        CardUIManager.Instance.Discard(handIndex);      // UI�� ī�� ��� �Լ��� ȣ��

        handCooltime[handIndex].value           // ��Ÿ�� ����
            = handCooltime[handIndex].maxValue
            = CardInfo.cardInfo[hands[handIndex]].cooltime;

        stamina -= 1;       // ���¹̳� �Ҹ�
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
