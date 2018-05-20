namespace VRTK.Examples
{
    using UnityEngine;
    using UnityEventHelper;


    public class ControlReactorMoveObject : MonoBehaviour
    {
        public TextMesh go;
        public GameObject leverObject;
        public GameObject outputObject;

        private VRTK_Control_UnityEvents controlEvents;

        private void Start()
        {
            transform.position = new Vector3();
            controlEvents = GetComponent<VRTK_Control_UnityEvents>();
            if (controlEvents == null)
            {
                controlEvents = gameObject.AddComponent<VRTK_Control_UnityEvents>();
            }

            controlEvents.OnValueChanged.AddListener(HandleChange);
        }

        private void HandleChange(object sender, Control3DEventArgs e)
        {
            //outputObject = e.value;
            //boxTransform.position.y = boxVector;
            go.text = e.value.ToString() + "(" + e.normalizedValue.ToString() + "%)";
        }
    }
}