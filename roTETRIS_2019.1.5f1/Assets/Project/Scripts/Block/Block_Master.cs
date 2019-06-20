using roTETRIS.Planet;
using System;
using UnityEngine;

namespace roTETRIS.Block {
    public class Block_Master : MonoBehaviour {

        public const int MaxRow = 10 * Constant.UnitCount;
        public static Block_Master[,] Grounded = new Block_Master[MaxRow, Constant.UnitCount];

        [SerializeField] private int _num; 

        public event GameEventHandler FindGroundEvent;
        public event GameEventHandler LostGroundEvent;
        public event GameEventHandler CollapseEvent;

        private void Start() {
            CallLostGroundEvent();
        }

        public void CallFindGroundEvent() {
            CheckForFullRows();
            FindGroundEvent?.Invoke();
        }

        public void CallLostGroundEvent() {
            LostGroundEvent?.Invoke();
        }

        public void CallCollapseEvent() {
            CollapseEvent?.Invoke();
        }

        public bool CheckForCollision(int r, int alpha, Transform myTransform) {
            var dalpha = Mathf.CeilToInt(Planet_Master.Instance.MyTransform.rotation.eulerAngles.z / Constant.UnitAngle);
            var i = ProgressionD(r-1);
             if (i == 0 || i >0 && Grounded[i - 1, (alpha - dalpha + Constant.UnitCount) % Constant.UnitCount] != null) {
                Grounded[i, (alpha - dalpha + Constant.UnitCount) % Constant.UnitCount] = this;
                myTransform.SetParent(Planet_Master.Instance.MyTransform);
                return true;
            }

            //// row 1, check row 0
            //if (r == 1 + S(1) && Grounded[1 - 1, alpha] != null) {
            //    Grounded[1, alpha] = this;
            //    return true;
            //}

            //// row 2, check row 1
            //if (r == 1 + 1 + 2 && Grounded[2 - 1, alpha] != null) {
            //    Grounded[2, alpha] = this;
            //    return true;
            //}

            //// row 3, check row 2
            //if (r == 1 + 1 + 2 + 3 && Grounded[3 - 1, alpha] != null) {
            //    Grounded[3, alpha] = this;
            //    return true;
            //}

            return false;
        }

        private int ProgressionD(int r) {
            var s = 0;
            var i = 0;
            while (s < r) {
                i++;
                s += i;
            }
            return s == r ? i : -1;
        }

        private void CheckForFullRows() {
            for (int i = 0; i < MaxRow; i++) {
                bool isFullRow = true;
                for (int j = 0; j < Constant.UnitCount; j++) {
                    if (Grounded[i, j] == null) {
                        isFullRow = false;
                        break;
                    }
                }

                if (isFullRow) {
                    for (int j = 0; j < Constant.UnitCount; j++) {
                        Grounded[i, j].CallCollapseEvent();
                    }

                    for (int k = i+1; k < MaxRow; k++) {
                        for (int j = 0; j < Constant.UnitCount; j++) {
                            Grounded[i, j].CallLostGroundEvent();
                            Grounded[i, j] = null;
                        }
                    }
                    break;
                }
            }
        }
    }
}