using UnityEngine;

public class ShowBackpack : MonoBehaviour
{
    public bool Backpack = false; // ��ʼ��B�鲻�@ʾ����
    public GameObject BackpackCanva; // �B�Y����������� Canvas
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (BackpackCanva != null)
        {
            BackpackCanva.SetActive(false); // �_����ʼ��B����������[��
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) // ����Ұ��� TAB �I
        {
            Backpack = !Backpack; // �ГQ������B

            if (BackpackCanva != null)
            {
                BackpackCanva.SetActive(Backpack); // �@ʾ���[�ر�������
            }
            if (Backpack)
            {
                Cursor.visible = true; // �@ʾ����
                Cursor.lockState = CursorLockMode.None; // ���i����
            }
            else
            {
                Cursor.visible = false; // �[�ػ���
                Cursor.lockState = CursorLockMode.Locked; // �i������
            }
        }
    }
}
