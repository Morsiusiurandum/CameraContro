using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCon : MonoBehaviour
{


    public static CameraCon instance;
    Camera cam;//需要先建一个实例
    float rotationY;
    float rotationX;
    public float sensitivity;//摄像机灵敏度
    public float rotalimitX;//摄像机旋转的最大角度限制
    public float rotalimitY;
    public float fovsensitivity;


    public void Awake()
    {
        if (instance== null)
        {
           instance = this;
           DontDestroyOnLoad(gameObject); // 防止场景切换时销毁
        }
        else
        {
            // 如果已经存在实例，则销毁当前实例
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


            //鼠标的Y坐标是从上向下增加的。为了使物体绕X轴向上旋转（即用户视角向上看），我们需要将这个负值转换为正值
            angle.x = transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;//绕x轴旋转角度
            //Debug.Log(Input.GetAxis("Mouse Y"));
            angle.y = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;//绕y轴旋转角度

            transform.localEulerAngles = angle;//游戏物体最终旋转到的角度
            //transform.rotation是将欧拉角转化为四元数进行旋转
        }
    }


    public void ResetCam()//过一定时间以后相机复原，按一定速度旋转
    {

    }

    public void CameraFov()
    {
        //这个数大于0表示往前滑动，放大镜头；小于0表示往后滑动，缩小镜头
        Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
        float fov = Input.GetAxis("Mouse ScrollWheel") * fovsensitivity * Time.deltaTime;
        float currentfov = cam.fieldOfView;
        cam.fieldOfView -= fov;
    }
}
