using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Animator animator;
    public Transform player;
    private AnimatorControllerParameter[] screens;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        screens = animator.parameters;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.x > 5.7f && player.position.x < 34.15f && player.position.y > -10.9f)
        {
            GoToScreen(screens[1].name);
        }
        else if (player.position.x > 5.7f && player.position.x < 34.15f && player.position.y < -10.9f && player.position.y > -26.9f)
        {
            GoToScreen(screens[2].name);
        }
        else if (player.position.x > 34.15f && player.position.y > -26.9f)
        {
            GoToScreen(screens[3].name);
        }
        else if (player.position.x > 29.3f && player.position.y < -26.9f)
        {
            GoToScreen(screens[4].name);
        }
        else if (player.position.x > 0.85f && player.position.x < 29.3f && player.position.y < -26.9f)
        {
            animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
            GoToScreen(screens[5].name);
        }
        else if (player.position.x > -46.01f && player.position.x < 0.85f && player.position.y < -23f)
        {
            GoToScreen(screens[6].name);
            animator.cullingMode = AnimatorCullingMode.CullCompletely;
            // set the animator in screen6 clip
            animator.Play("Screen6");
            transform.position = new Vector3(
                            Mathf.Clamp(player.position.x, -31.8f, -13.5f),
                            Mathf.Clamp(player.position.y, -35.9f, -31f),
                            -10f
                        );
        }
        else if (player.position.x < -22.8f && player.position.y < -10f && player.position.y > -23f)
        {
            animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
            GoToScreen(screens[7].name);
        }
        else if (player.position.x > -22.8f && player.position.x < 5.7f && player.position.y > -21.9f && player.position.y < 6f)
        {
            animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
            GoToScreen(screens[8].name);
        }
    }

    /* void LateUpdate()
    {
        if (player.position.x > -46.01f && player.position.x < 0.85f && player.position.y < -23f)
        {
            transform.position = new Vector3(
                Mathf.Clamp(player.position.x, -31.8f, -13.5f),
                Mathf.Clamp(player.position.y, -35.9f, -31f),
                -10f
            );
        }
    } */

    private void GoToScreen(string transitionName)
    {
        animator.SetBool(transitionName, true);

        for (int i = 0; i < screens.Length; i++)
        {
            if (screens[i].name != transitionName)
            {
                animator.SetBool(screens[i].name, false);
            }
        }
    }
}
