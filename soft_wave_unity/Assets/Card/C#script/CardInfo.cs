using UnityEngine;
using UnityEngine.UI;

/* 10/1 00:50 �������� - �۰��
 * �� �ڵ带 �б� ���� CardDBreader.cs�� ���� �а� �� ��
 * Card ����ü�� ��Ÿ��(ī�带 ����� �� �� ���ڸ��� ��ο�Ǳ������ �ð�) ���� �߰�
 * ī�� �����ͺ��̽��� CardInfo��� CardDB.csv���Ϸ� ��ü�ϴ� ���� ����
 */

// ī�� ������ ���� ����ü
public struct Card
{
    public string name;
    public Sprite image;
    public string desc;
    public float cooltime;

    // ī�� ����ü ������
    public Card(string name, Sprite image, string desc, float cooltime)
    {
        this.name = name;
        this.image = image;
        this.desc = desc;
        this.cooltime = cooltime;
    }
}

public static class CardInfo
{
    // ī�� ��ȣ�� enum���� ����
    public enum CARD { ATTACK, DEFENSE, ROLL }

    // ī�� ��ȣ�� �����Ǵ� ������ ����
    public static Card[] cardInfo = { new Card("Attack", null, "Inflict 2 Damage", 0),     // 000
                                      new Card("Defense", null, "Defend 2 Damage", 0),     // 001
                                      new Card("Roll", null, "Roll Forward", 0)            // 002
                                    };

    // ��ü ī���� ���� ����
    public static int size = cardInfo.Length;
}
