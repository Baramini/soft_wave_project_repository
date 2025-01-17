using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Threading;
using UnityEngine.Events;

public class CardManager : MonoBehaviour
{
    public UnityEvent<int> whenCasting; // 카드 사용시 이벤트 (카드 ID를 매개변수로 전달)
    public UnityEvent<int> whenHit; // 데미지를 줄 때 이벤트 (준 데미지 량을 매개변수로 전달)

    public List<int> deck = new List<int>();            // 플레이어가 소지한 덱
    public int[] hands = new int[5];                    // 플레이어가 손에 들고 있는 패
    public int drawIndex = 0;                           // 이번 드로우에서 뽑을 카드 인덱스
    public Image[] staminaCircle;                       // 스태미나를 보여줄 UI
    public float stamina;                               // 실제 스태미나
    [SerializeField] public Slider[] handCooltime;      // 패의 쿨타임(카드를 사용한 후 다시 뽑기까지의 시간)을 보여줄 슬라이더

    private static CardManager _instance;       // 싱글턴 패턴 구현부

    public static CardManager Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        _instance = this;
        
        deck = new List<int>() { 1, 1, 1, 2, 2, 3, 3, 3, 3, 3 };    // 테스트 목적으로 임의의 덱을 소유하도록 함

        // 스테미나 초기화
        foreach (Image circle in staminaCircle) 
            circle.fillAmount = 0;
    }

    void Update()
    {
        stamina = Mathf.Min(stamina+Time.deltaTime, staminaCircle.Length);      // 스태미나 회복

        // 테스트 목적
        // 1-5 키를 누르면 손에 든 카드를 냄
        if (Input.GetKeyDown(KeyCode.Alpha1)) UseCard(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) UseCard(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) UseCard(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) UseCard(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) UseCard(4);

        // 각 패의 쿨타임 감소 && 쿨 찼으면 드로우
        for (int handIndex = 0; handIndex < 5; handIndex++)                     
        {
            if (handCooltime[handIndex].value > 0) handCooltime[handIndex].value -= Time.deltaTime;     // 쿨타임 감소
            else if (CardUIManager.Instance.cardsInHands[handIndex] == null) DrawCard(handIndex);       // 빈 패를 확인하고 드로우
        }

        for (int i = 0; i < staminaCircle.Length; i++)      // 스태미나 UI 관리
            staminaCircle[i].fillAmount = stamina - i;        
    }

    // 덱에 카드 추가
    void addToDeck(int cardIndex)
    {
        deck.Add(cardIndex);        // cardIndex가 가리키는 카드를 덱의 맨뒤에 추가한다
        if (CardInfo.cardInfo[cardIndex].effects != null) CardInfo.cardInfo[cardIndex].effects.OnUse();     // 카드 추가 효과 호출
    }

    // 덱에서 카드 제거
    void removeFromDeck(int deckIndex)
    {
        if (CardInfo.cardInfo[deck[deckIndex]].effects != null) CardInfo.cardInfo[deck[deckIndex]].effects.OnUse(); // 카드 제거 효과 호출
        deck.RemoveAt(deckIndex);   // 덱 내에서 (deckIndex+1)번째 카드를 제거한다
    }

    // 카드 사용
    void UseCard(int handIndex)
    {
        if (CardUIManager.Instance.cardsInHands[handIndex] == null) return;     // 패를 가지고 있는지 검사
        if (stamina < 1) return;                                                // 스태미나가 충분한지 검사
        if (CardInfo.cardInfo[hands[handIndex]].effects != null) CardInfo.cardInfo[hands[handIndex]].effects.OnUse();  // 카드 사용 효과 호출

        CardUIManager.Instance.Discard(handIndex);      // UI에 카드 사용 함수를 호출

        handCooltime[handIndex].value           // 쿨타임 적용
            = handCooltime[handIndex].maxValue
            = CardInfo.cardInfo[hands[handIndex]].cooltime;

        stamina -= 1;       // 스태미나 소모
    }

    // 카드 뽑기
    void DrawCard(int handIndex)
    {
        if (drawIndex == deck.Count())              // 뽑을 인덱스가 마지막에 도달한 경우
        {
            ShuffleDeck();                          // 덱을 셔플한 후
            drawIndex = 0;                          // 처음부터 다시 시작
        }

        CardUIManager.Instance.DrawCard(handIndex, deck[drawIndex]);  // UI에 카드 뽑기 함수를 호출
        hands[handIndex] = deck[drawIndex++];       // 손에 든 패 갱신 후, drawIndex 증가
        if (CardInfo.cardInfo[hands[handIndex]].effects != null) CardInfo.cardInfo[hands[handIndex]].effects.OnDraw();  // 카드 드로우 효과 호출
    }

    // 덱 셔플
    // 피셔-예이츠(Fisher-Yate) 셔플 방식으로 구현
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
