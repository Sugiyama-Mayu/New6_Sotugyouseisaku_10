using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S �^�C�g����Q�[���I�[�o�[�Ȃǂ̃I�u�W�F�N�g���܂Ƃ߂�
public class MainSceneObj : MonoBehaviour
{
    public Canvas gameOverCanvas;      // �Q�[���I�[�o�[��ʂł̃{�^���Ȃǂ�UI
    public Canvas titleCanvas;         // �^�C�g����ʂł̃{�^���Ȃǂ�UI
    public GameObject player;          // �ʏ펞�g�p���Ă���J����
    public Camera switchTitleCamera;   // �^�C�g����ʂŎg�p����J����
    public GameObject questCanvas;
    public bool menuOffMode;           // �K�̒��ł̓~�h���{�^�����j���[�I�t
}
