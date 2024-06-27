using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCon : MonoBehaviour
{


    public static CameraCon instance;
    Camera cam;//��Ҫ�Ƚ�һ��ʵ��
    float rotationY;
    float rotationX;
    public float sensitivity;//�����������
    public float rotalimitX;//�������ת�����Ƕ�����
    public float rotalimitY;
    public float fovsensitivity;


    public void Awake()
    {
        if (instance== null)
        {
           instance = this;
           DontDestroyOnLoad(gameObject); // ��ֹ�����л�ʱ����
        }
        else
        {
            // ����Ѿ�����ʵ���������ٵ�ǰʵ��
            //stroy(gameObject);
        }
    }
    void Start()
    {
        cam = transform.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        CameraRot();

        CameraFov();
    }


    public void CameraRot()
    {
        if (Input.GetMouseButton(1))
        {

            Vector3 angle = Vector3.zero;


            //����Y�����Ǵ����������ӵġ�Ϊ��ʹ������X��������ת�����û��ӽ����Ͽ�����������Ҫ�������ֵת��Ϊ��ֵ
            angle.x = transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;//��x����ת�Ƕ�
            //Debug.Log(Input.GetAxis("Mouse Y"));
            angle.y = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;//��y����ת�Ƕ�

            transform.localEulerAngles = angle;//��Ϸ����������ת���ĽǶ�
            //transform.rotation�ǽ�ŷ����ת��Ϊ��Ԫ��������ת
        }
    }


    public void ResetCam()//��һ��ʱ���Ժ������ԭ����һ���ٶ���ת
    {

    }

    public void CameraFov()
    {
        //���������0��ʾ��ǰ�������Ŵ�ͷ��С��0��ʾ���󻬶�����С��ͷ
        Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
        float fov = Input.GetAxis("Mouse ScrollWheel") * fovsensitivity * Time.deltaTime;
        float currentfov = cam.fieldOfView;
        cam.fieldOfView -= fov;
    }
}
