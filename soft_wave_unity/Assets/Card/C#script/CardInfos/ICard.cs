// ī�� ȿ���� �����ϴ� �������̽�
// �� ī���� ȿ���� "Card_00n.cs"�� ���� ������� Ŭ������ ����� �����Ѵ�.
public interface ICard
{
    // ���� ī�尡 �߰��� �� �ߵ��ϴ� ȿ��
    public void OnAcquire();

    // ī�带 ���п� ��ο����� �� �ߵ��ϴ� ȿ��
    public void OnDraw();

    // ���п� �ִ� ī�带 ������� �� �ߵ��ϴ� ȿ��
    public void OnUse();

    // ������ ī�带 ������ �� �ߵ��ϴ� ȿ��
    public void OnRemove();
}
