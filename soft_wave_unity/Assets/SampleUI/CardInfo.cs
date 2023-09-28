using UnityEngine;
using UnityEngine.UI;

// ī�� ������ ���� ����ü
public struct Card
{
    public string name;
    public Sprite image;
    public string desc;

    // ī�� ����ü ������
    public Card(string name, Sprite image, string desc)
    {
        this.name = name;
        this.image = image;
        this.desc = desc;
    }
}

public static class CardInfo
{
    // ī�� ��ȣ�� enum���� ����
    public enum CARD { ATTACK, DEFENSE, ROLL }

    // ī�� ��ȣ�� �����Ǵ� ������ ����
    public static Card[] cardInfo = { new Card("Attack", null, "Inflict 2 Damage"),     // 000
                                      new Card("Defense", null, "Defend 2 Damage"),     // 001
                                      new Card("Roll", null, "Roll Forward")            // 002
                                    };

    // ��ü ī���� ���� ����
    public static int size = cardInfo.Length;
}
