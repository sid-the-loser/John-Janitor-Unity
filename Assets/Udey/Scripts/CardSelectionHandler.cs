using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Udey.Scripts
{
    /// Manages the selection and appearance changes of a card object in a Unity environment.
    public class CardSelectionHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler,
        IDeselectHandler
    {
        /// Amount by which the card is vertically moved during selection.
        [SerializeField] private float vertMoveAmount = 30f;

        /// The amount of time, in seconds, it takes for the card to complete its movement animation.
        [SerializeField] private float moveTime = 0.1f;

        /// A factor by which the card's scale is increased or decreased during selection animations.
        /// This value can range between 0 and 2, with the default value set to 1.25f.
        [Range(0f, 2f)] [SerializeField] private float scaleFactor = 1.25f;

        /// Stores the initial position of the card.
        private Vector3 _startPos;

        /// Stores the initial scale of the card before any transformations are applied.
        private Vector3 _startScale;

        /// Initializes the CardSelectionHandler class, setting default values for vertical movement,
        /// animation time, and scale factor. Also captures the initial position and scale of the card.
        private void Start()
        {
            _startPos = transform.position;
            _startScale = transform.localScale;
        }

        /// Handles the event when the selection is deselected.
        public void OnDeselect(BaseEventData eventData)
        {
            StartCoroutine(MoveCards(false));
        }

        /// Handles the event when the pointer enters the card's area.
        public void OnPointerEnter(PointerEventData eventData)
        {
            eventData.selectedObject = gameObject;
        }

        /// Called when the pointer exits the UI element.
        public void OnPointerExit(PointerEventData eventData)
        {
            eventData.selectedObject = null;
        }

        /// Called when the card is selected.
        /// This method initiates the card movement coroutine to animate the card's position.
        public void OnSelect(BaseEventData eventData)
        {
            StartCoroutine(MoveCards(true));
        }

        /// Moves the card vertically and scales it up or down over time depending on the value of the parameter.
        /// <param name="startingOfAnimation">
        ///     If true, moves the card up and scales it up; if false, moves the card back to its
        ///     original position and scale.
        /// </param>
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