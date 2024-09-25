
using UnityEngine;
using VRC.SDKBase;

namespace sweetforest.freezemovegesturema.Components
{
    [ExecuteInEditMode]
    [AddComponentMenu("Freeze Move Gesture MA")]
    [DisallowMultipleComponent]
    public class FreezeMoveGestureComponent : MonoBehaviour, IEditorOnly
    {
        public bool LeftFist;
        public bool LeftOpen;
        public bool LeftPoint;
        public bool LeftPeace;
        public bool LeftRockNRoll = true;
        public bool LeftGun;
        public bool LeftThumbsUp;

        [Space(10)] 
        public bool RightFist;
        public bool RightOpen;
        public bool RightPoint;
        public bool RightPeace;
        public bool RightRockNRoll;
        public bool RightGun;
        public bool RightThumbsUp;

        [Space(20)] 
        public bool AddToggleMenu = true;




    }
}
