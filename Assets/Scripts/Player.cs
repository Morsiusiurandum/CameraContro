using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public Camera playercam;//��E�������
    bool isopencarm = false;
    int photonum;//��������ñ���ģ����泡���л��ı�
    // Start is called before the first frame update
    void Start()
    {
        photonum = 0;
        playercam = transform.GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            OpenCam();
        }
        TakePhoto();
    }

    void OpenCam()
    {
        if (isopencarm == false)
        {
            playercam.gameObject.SetActive(true);
            playercam.depth = 1;
            //���Զ������������ת����ɶ��
            isopencarm = true;
        }
        else
        {
            playercam.gameObject.SetActive(false);
            playercam.depth = -1;
            isopencarm = false;
        }
    }

    void TakePhoto()
    {
        if (isopencarm)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                int rectwidth = Screen.width / 2;
                int recthight = Screen.height / 2;//���ջ�ȡ��Ļ�ķ�Χ
                Rect rect = new Rect(0,0,rectwidth, recthight);//����X���꣬����Y���꣬���εĳ�����
                RenderTexture rt = new RenderTexture(rectwidth, recthight, -1);//������һ����Ⱦ������������������������Ⱦ
                playercam.targetTexture = rt;//���������ȾĿ������Ϊ��Ⱦ����
                playercam.Render();//�ֶ���Ⱦһ֡
                RenderTexture.active = rt;//�������rt
                Texture2D screenShot = new Texture2D(rectwidth, recthight);//����һ��2D����׼��������Ⱦ��һ֡
                screenShot.ReadPixels(rect, 0,0);//��active�����rt�ж�ȡ����
                screenShot.Apply();

                playercam.targetTexture = null;
                RenderTexture.active = null;
                GameObject.Destroy(rt);//�ǵ�ɾ���������rt��unity�����Զ�����

                byte[] bytes = screenShot.EncodeToPNG();//��2D����תΪһ��pngͼƬ
                string filename = Application.dataPath + "/Screenshot" + photonum + ".png";
                System.IO.File.WriteAllBytes(filename, bytes);//�����ڱ���
                Debug.Log(string.Format("�ɹ�������һ����Ƭ:{0}", filename));
                photonum++;
            }
        }
    }



}
