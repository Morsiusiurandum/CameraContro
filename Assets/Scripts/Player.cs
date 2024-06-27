using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public Camera playercam;//按E键打开相机
    bool isopencarm = false;
    int photonum;//这个是永久保存的，不随场景切换改变
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
            //可以对相机操作，旋转缩放啥的
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
                int recthight = Screen.height / 2;//拍照获取屏幕的范围
                Rect rect = new Rect(0,0,rectwidth, recthight);//矩形X坐标，矩形Y坐标，矩形的长，宽
                RenderTexture rt = new RenderTexture(rectwidth, recthight, -1);//创建了一个渲染纹理，相机可以在这个纹理上渲染
                playercam.targetTexture = rt;//将相机的渲染目标设置为渲染纹理
                playercam.Render();//手动渲染一帧
                RenderTexture.active = rt;//激活这个rt
                Texture2D screenShot = new Texture2D(rectwidth, recthight);//创建一个2D纹理，准备保存渲染的一帧
                screenShot.ReadPixels(rect, 0,0);//从active激活的rt中读取像素
                screenShot.Apply();

                playercam.targetTexture = null;
                RenderTexture.active = null;
                GameObject.Destroy(rt);//记得删除回收这个rt，unity不会自动回收

                byte[] bytes = screenShot.EncodeToPNG();//将2D纹理转为一个png图片
                string filename = Application.dataPath + "/Screenshot" + photonum + ".png";
                System.IO.File.WriteAllBytes(filename, bytes);//保存在本地
                Debug.Log(string.Format("成功截屏了一张照片:{0}", filename));
                photonum++;
            }
        }
    }



}
