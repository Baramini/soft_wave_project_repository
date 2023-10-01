using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;

// ī�� ������ ���� ����ü
public struct Card
{
    public string name;
    public Sprite image;
    public string desc;
    public float cooltime;

    // ī�� ����ü ������
    public Card(string name, Sprite image, float cooltime, string desc)
    {
        this.name = name;
        this.image = image;
        this.desc = desc;
        this.cooltime = cooltime;
    }
}

public static class CardInfo
{
    // ī�� ��ȣ�� �����Ǵ� ������ ����
    public static Dictionary<int, Card> cardInfo = new Dictionary<int, Card>();
    // ��ü ī���� ���� ����
    public static int size;
    static CardInfo()
    {
        string path = @"Assets\Card\CardDB.csv";                    // CardDB�� ���;  �̽������� ���ڰ� �ƴ��� �˷��ֱ� ���� @�� ����
        StreamReader streamReader = new StreamReader(path);         // �־��� ����� ������ ����

        streamReader.ReadLine();                                    // CSV������ ù��° ��(�� ���� �̸�)�� ��������
        string[] lines = streamReader.ReadToEnd().Split('\n');      // ������ ������ ���� �� ���͸� �������� ��� ���ڿ��� ����

        foreach (string line in lines)                              // ������ �� ���� line�� ��Ƽ� ��ȸ
        {
            if(line == "") continue;                                    // ���� CSV���Ͽ� �� ���� ���ԵǴ� ��츦 ó���ϱ� ����

            string[] datas = line.Split(',');                           // data�� �� ī���� �����͸� string�迭�� ����

            int     id       = int.Parse(datas[0]);                     // id           ���ڿ��� ���������� ��ȯ
            string  name     = datas[1];                                // name
            Sprite  image    = Resources.Load<Sprite>(datas[2]);        // image        Resoures���Ͽ��� data[2]�ּҿ� �ش��ϴ� �̹����� ������
            float   cooltime = float.Parse(datas[3]);                   // cooltime     ���ڿ��� �Ǽ������� ��ȯ
            string  desc     = datas[4];                                // desc

            cardInfo[id] = new Card(name, image, cooltime, desc);   // id�� key�� Card����ü�� value������ ����
        }
        streamReader.Close();                                       // �޸� ���� ���� ����
        size = cardInfo.Count;
    }
}
