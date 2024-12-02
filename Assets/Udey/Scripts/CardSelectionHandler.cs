using System.Collections;
using FMOD.Studio;
using FMODUnity;
using Sound.Scripts.Sound;
using UnityEngine;
using UnityEngine.EventSystems;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace Udey.Scripts
{
    /// Manages the selection and appearance changes of a card object in a Unity environment.
    public class CardSelectionHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
    {
        [SerializeField] private float vertMoveAmount = 30f; //Amount by which the card is vertically moved during selection.
        [SerializeField] private float moveTime = 0.1f; //The amount of time, in seconds, it takes for the card to complete its movement animation.
        [Range(0f, 2f)] [SerializeField] private float scaleFactor = 1.25f; //A factor by which the card's scale is changed during selection animations. Range between 0 and 2.

        private Vector3 _startPos; //Stores the initial position of the card.
        private Vector3 _startScale; //Stores the initial scale of the card before any transformations are applied.

        private Bus bus;
        private EventInstance CardSelected;
        
        private void Start() //Also captures the initial position and scale of the card.
        {
            _startPos = transform.position;
            _startScale = transform.localScale;
            
            bus = FMODUnity.RuntimeManager.GetBus("bus:/");
            CardSelected = AudioManager.Instance.CreateEventInstance(FmodEvents.Instance.CardsSelect);
        }

        #region Animations

        public void OnDeselect(BaseEventData eventData) //Handles the event when the selection is deselected.
        {
            bus.stopAllEvents(STOP_MODE.IMMEDIATE);
            StartCoroutine(MoveCards(false));
        }

        public void OnPointerEnter(PointerEventData eventData) //Handles the event when the pointer enters the card's area.
        {
            AudioManager.Instance.PlayOneShot(FmodEvents.Instance.CardsHover, transform.position);
            eventData.selectedObject = gameObject;
        }

        public void OnPointerExit(PointerEventData eventData) //Called when the pointer exits the UI element.
        {
            bus.stopAllEvents(STOP_MODE.IMMEDIATE);
            eventData.selectedObject = null;
        }

        public void OnSelect(BaseEventData eventData) //Called when the card is selected. Initiates the card movement coroutine to animate the card's position.
        {
            StartCoroutine(OnCardSelected());
        }

        private IEnumerator OnCardSelected()
        {
            AudioManager.Instance.PlayOneShot(FmodEvents.Instance.CardsSelect, transform.position);
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(MoveCards(true));
            yield return new WaitForSeconds(0.5f);
            AudioManager.Instance.PlayOneShot(FmodEvents.Instance.StartingDialogue, transform.position);
        }

        /* Moves the card vertically and scales it up or down over time depending on the value of the parameter.
         <param name="startingOfAnimation">
             If true, moves the card up and scales it up; if false, moves the card back to its
             original position and scale.
         </param> */
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

        #endregion
    }
}