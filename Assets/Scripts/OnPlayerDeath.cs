using UnityEngine;
using UnityEngine.SceneManagement;

public class OnPlayerDeath : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SceneManager.LoadScene("gameOver");
    }
}