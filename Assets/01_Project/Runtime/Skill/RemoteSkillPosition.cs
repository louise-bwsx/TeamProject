using UnityEngine;

public class RemoteSkillPosition : MonoBehaviour
{
    private int floor;
    private Vector3 playertomouse;
    private MeshRenderer mousePosition;
    [SerializeField] private PlayerFaceDirection playerFaceDirection;
    [SerializeField] private SkillSelect skillControl;
    [SerializeField] private MeshRenderer skillRotation;

    private void Awake()
    {
        mousePosition = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        floor = LayerMask.GetMask("Floor");
    }
    private void Update()
    {
        //TODOError: 沒辦法使用技能特效
        //TODO: 這裡應該還可以移到動畫Event控制
        if (playerFaceDirection.isMagicAttack)
        {
            switch (skillControl.CurrentIndex)
            {
                //毒風土
                case (int)SkillType.Poison:
                case (int)SkillType.Wind:
                case (int)SkillType.Earth:
                    float cameraraylength = 500;
                    Ray cameraray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(cameraray, out hit, cameraraylength, floor))
                    {
                        playertomouse = hit.point;
                    }
                    mousePosition.enabled = true;
                    transform.position = playertomouse;
                    break;
                //水火
                case (int)SkillType.Water:
                case (int)SkillType.Fire:
                    skillRotation.enabled = true;
                    break;
                default:
                    break;
            }
        }
        else
        {
            skillRotation.enabled = false;
            mousePosition.enabled = false;
        }
    }
}
