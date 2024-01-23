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

        // Move camera if player passes to second screen
        if (animator.GetBool(screens[0].name) && player.position.x > 5.7f && player.position.x < 34.15f && player.position.y > -12.8f)
        {
            GoToScreen(screens[1].name);
        }
        else if (animator.GetBool(screens[1].name) && player.position.y < -7.5f && player.position.x > -14f && player.position.x < 10f)
        {
            GoToScreen(screens[2].name);
        }
        else if (animator.GetBool(screens[2].name) && player.position.x > 17f)
        {
            GoToScreen(screens[3].name);
        }
        else if (animator.GetBool(screens[3].name) && player.position.x < 17f)
        {
            GoToScreen(screens[2].name);
        }
    }

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
