using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Udey.Scripts
{
    public class CardSelectionHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler,
        IDeselectHandler
    {
        [SerializeField] private float vertMoveAmount;
        [SerializeField] private float moveTime;
        [Range(0f, 2f)] [SerializeField] private float scaleFactor;

        private Vector3 _startPos;
        private Vector3 _startScale;

        private void Start()
        {
            vertMoveAmount = 30f;
            moveTime = 0.1f;
            scaleFactor = 1.25f;
            scaleFactor = 1.25f;

            _startPos = transform.position;
            _startScale = transform.localScale;
        }

        public void OnDeselect(BaseEventData eventData)
        {
            StartCoroutine(MoveCards(false));
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            eventData.selectedObject = gameObject;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            eventData.selectedObject = null;
        }

        public void OnSelect(BaseEventData eventData)
        {
            StartCoroutine(MoveCards(true));
        }

        private IEnumerator MoveCards(bool startingOfAnimation)
        {
            var runningTime = 0f;
            while (runningTime < moveTime)
            {
                runningTime += Time.deltaTime;

                Vector3 endPos;
                Vector3 endScale;
                if (startingOfAnimation)
                {
                    endPos = _startPos + new Vector3(0f, vertMoveAmount, 0f);
                    endScale = _startScale * scaleFactor;
                }
                else
                {
                    endPos = _startPos;
                    endScale = _startScale;
                }

                var lerpedPos = Vector3.Lerp(transform.position, endPos, runningTime / moveTime);
                var lerpedScale = Vector3.Lerp(transform.localScale, endScale, runningTime / moveTime);

                transform.position = lerpedPos;
                transform.localScale = lerpedScale;

                yield return null;
            }
        }
    }
}