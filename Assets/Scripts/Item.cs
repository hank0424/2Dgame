using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// �@�����ԕ�׌ Unity ��݋���п���͸�^���I�x�ν����� ScriptableObject
// ·���顸Create �� Scriptable object �� Item��
[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    // ===== �[��߉݋���P�O�� =====
    [Header("Only gameplay")] // �� Inspector �@ʾ��һ�����}
    public TileBase tile;      // ԓ��Ʒ������ Tile����춵؈D�ϵ��@ʾ��
    public ItemType type;      // ��Ʒ��ͣ��زġ����ߡ������ȣ�
    public ActionType actionType; // ʹ�÷�ʽ�����ӡ����ӡ��b�䣩
    public Vector2Int range = new Vector2Int(5, 4);
    // ʹ�ù�����������������������������Ӱ푅^��

    // ===== UI �@ʾ���P�O�� =====
    [Header("Only UI")]
    public bool stackable = true; // �Ƿ���ԶѯB����ˎˮ���زģ�
    public int maxStack = 10;     // �ѯB���ޔ���
    public string itemName;       // ��Ʒ���Q��UI �@ʾ��

    // ===== UI �c�[�򶼕��õ� =====
    [Header("Both")]
    public Sprite image;          // ��Ʒ�DƬ��UI �@ʾ���[���ʹ�ã�

    // ===== ��Ʒʹ��߉݋ =====
    public virtual void Use()
    {
        if (actionType == ActionType.����ʹ��)
        {
            // �����Ʒ�O���顸����ʹ�á����t�����@�e��߉݋
            Debug.Log($"ʹ���˵��ߣ�{name}");

            // TODO: ���@�e������w���ܣ����磺
            // - �؏� HP
            // - ���ӹ�����
            // - �|�l Buff
            // - ������Ч�ȵ�
        }
        else
        {
            // �����Ʒ��������ʹ����ͣ��@ʾ����ӍϢ
            Debug.LogWarning($"���ߣ�{name} �o������ʹ�ã�");
        }
    }
}

// ===== ��Ʒ������e =====
// ��춅^����Ʒ�������������Дࣩ
public enum ItemType
{
    �ز�,     // ��������
    ����,     // һ����ߣ�����؏�ˎˮ��
    �P�I����, // �������i�õ��������
    ����,     // �����b�������
    ����      // ���ܕ���W�����ܵ���Ʒ
}

// ===== ʹ�÷�ʽ���e =====
public enum ActionType
{
    ����ʹ��, // �Ԅ���Ч�������b�����Ԅ����ӌ��ԣ�
    ����ʹ��, // ����ք�ʹ�ã�����ˎˮ�����ܾ��S��
    �b��      // ���b������Ʒ�����������ף�
}
