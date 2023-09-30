using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

// CardDB.csv������ �б� ���� Ŭ����
// CardDB���� �� ī���� id(DB�� PrimaryKey), name, image���, description�� �����
// �ڵ带 �����ϴ� �� ������ �� �� �ִ� ��Ʃ�� ���� https://youtu.be/SvUYPh6lnqo?t=540
public static class CardDBreader
{
    static string path = @"Assets\Card\CardDB.csv";          // CardDB�� ���; �̽������� ���ڰ� �ƴ��� �˷��ֱ� ���� @�� ����

    public static string[] ReadDB(int ID)                           // ID�� �������� CSV������ �� ���� Ž���Ͽ� ���ڿ� �迭�� ��ȯ�ϴ� �Լ�
    {
        Stream readStream = new FileStream(path, FileMode.Open);        // �־��� ����� ������ �б���� ����
        StreamReader streamReader = new StreamReader(readStream);

        string line = null;                                             // line���� CSV������ ���� �����
        for (int i = 0; i <= ID; i++)                                   // id == 0 : �� �ʵ��� ����; id >= 1 : id�� �ش��ϴ� ī���� ������ ��� ��
            line = streamReader.ReadLine();                                 // ���ϴ� id�� ������ ������ ��� ���������� �ѱ�
            
        readStream.Close();                                             // Ȥ�� �� �޸� ���� ���� ����

        return line.Split(',');                                         // ���� ���� ','�� �������� ������ ��ȯ
    }
}