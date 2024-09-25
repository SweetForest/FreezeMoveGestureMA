
using UnityEngine;
using VRC.SDKBase;

namespace sweetforest.freezemovegesturema.Components
{
    [ExecuteInEditMode]
    [AddComponentMenu("Freeze Move Gesture MA")]
    [DisallowMultipleComponent]
    public class FreezeMoveGestureComponent : MonoBehaviour, IEditorOnly
    {

        public enum HandType
        {
            Left, Right
        }

        [Space(15)] 
        public HandType SelectHand = HandType.Left;
        [Space(15)] 
        public bool Neutral;
        public bool Fist;
        public bool Open;
        public bool Point;
        public bool Peace;
        public bool RockNRoll = true;
        public bool Gun;
        public bool ThumbsUp;


        [Space(15)] 
        public bool AddToggleMenu = true;




    }
}
