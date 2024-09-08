using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
#else
            Application.Quit();//�Q�[���v���C�I��
#endif
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            // ���Z�b�g
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
