using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public static class Utill
{
    private static Camera mainCam;
    private static CinemachineVirtualCamera vcam;

    public static Vector3 GetMouseWorldPos()
    {
        if (mainCam == null)
        {
            mainCam = Camera.main;
        }

        Vector3 mouseWorldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        return mouseWorldPos;
    }

    public static float GetAnimLength(Animator animator, string animName)
    {
        float time = 0;
        RuntimeAnimatorController ac = animator?.runtimeAnimatorController;

        time = 1f; // 

        if(ac != null)
        {
            for (int i = 0; i < ac.animationClips.Length; i++)
            {
                if (ac.animationClips[i].name == animName)
                {
                    time = ac.animationClips[i].length;
                }
            }
        }
        else
        {
            Debug.Log("해당하는 애니메이션을 찾을 수 없습니다.");
            time = 0.5f;
        }
        return time;
    }


}
