using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/* 10/1 00:50 �������� - �۰��
 * �� �ڵ带 �б� ���� CardDBreader.cs �� CardManager.cs�� ���� �а� �� ��
 * UpdateCard���� CardInfo�� �������� �ʰ� CardDBreader�� �����ϵ��� ����
 */

public class CardUIManager : MonoBehaviour
{
    public GameObject cardPrefab;       // ������ �Էµ��� ���� ī�� ������
    public Transform[] handsPos;        // ���� ī�尡 �̵��� ��ġ��
    public GameObject[] cardsInHands;   // ���� �տ� �ִ� ī�忡 ���� ���۷����� ����

    private static CardUIManager _instance; // �̱��� ���� ����
    public static CardUIManager Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        _instance = this;
        cardsInHands = new GameObject[5];
    }

    // ī�� �̱� �ִϸ��̼� ����
    public void DrawCard(int handIndex, int cardIndex)
    {
        GameObject newCard = Instantiate(cardPrefab, transform);                // ���������� ī�� ������Ʈ ����
        UpdateCard(newCard, cardIndex);                                         // ī�� ������ �°� ���� ������Ʈ
        newCard.GetComponent<Animator>().SetInteger("HandIndex", handIndex);    // �־��� ��ȣ�� ��ġ�� �̵��ϴ� �ִϸ��̼� ����
        StartCoroutine(UpdateHandWithDelay(handIndex, newCard));                // �ִϸ��̼� ���� �� ������ �ڷ�ƾ ȣ��
    }

    // ī�� ��� �ִϸ��̼� ����
    public void Discard(int handIndex)
    {
        if (!cardsInHands[handIndex]) return;       // �ش� ��ġ�� �ƹ� ī�尡 ���� ���

        cardsInHands[handIndex].GetComponent<Animator>().SetTrigger("Discard"); // ī�带 ����ϴ� �ִϸ��̼� ����
        StartCoroutine(UpdateHandWithDelay(handIndex, null));                   // �ִϸ��̼� ���� �� ������ �ڷ�ƾ ȣ��
    }

    // card�� ����Ű�� ī�� ������Ʈ�� cardIndex�� �ش��ϴ� ������ ����
    void UpdateCard(GameObject card, int cardIndex)
    {
        foreach (Transform obj in card.GetComponent<Transform>())   // ī�� ������Ʈ�� �ڽ� ������Ʈ�� �߿��� �˻�
        {
            if (obj.name == "Image")                                                                                // �׸� ����
            {
                obj.GetComponent<Image>().sprite = Resources.Load<Sprite>(CardDBreader.ReadDB(cardIndex)[2]);
                obj.GetComponent<Image>().color = Color.white;
            }
            if (obj.name == "Name") obj.GetComponent<TMP_Text>().text = CardDBreader.ReadDB(cardIndex)[1];          // �̸� ����
            if (obj.name == "Description") obj.GetComponent<TMP_Text>().text = CardDBreader.ReadDB(cardIndex)[4];   // ���� ����
        }
    }

    // ī�� �̱�/��� �ִϸ��̼� �� ������ �ڷ�ƾ
    IEnumerator UpdateHandWithDelay(int handIndex, GameObject newCard)
    {
        yield return new WaitForSeconds(.2f);   // .2�� ��...

        Destroy(cardsInHands[handIndex]);       // ������ �� ��ġ�� �ִ� ī��� �ı�
        cardsInHands[handIndex] = newCard;      // �� ��ġ�� �ִ� ī�带 ���� ī��� ����
    }

    // ��� �ִ� ��� �и� ���� (���̺�/�ε� � Ȱ��)
    public void UpdateHands(int[] cardIndexes)
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject newCard = Instantiate(cardPrefab, transform);
            UpdateCard(newCard, cardIndexes[i]);
            newCard.transform.position = handsPos[i].position;
        }
    }
}