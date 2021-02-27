using System.Collections;
using UnityEngine;



namespace Enemy1State{


    // Stateの実装を管理するクラス
    public class StateProcessor
    {
       
        private Enemy1State _State;
        public Enemy1State State
        {
            set { _State = value; }
            get { return _State; }
        }

        // 実行
        public void Execute() {

            State.Execute();
        }
    }

    public abstract class Enemy1State
    {
        public delegate void executeState();
        public executeState execDelegate;

        // 実行距離
        public virtual void Execute() {
            if(execDelegate != null)
            {
                execDelegate();
            }
        }

        // State名取得
        public abstract string getStateName();
    }


    //=============================================
    //
    //      以下状態クラス
    //
    //=============================================

    //=================================
    //  待機
    //=================================
    public class Enemy1StateWait : Enemy1State
    {
        public override string getStateName()
        {
            return "State:Wait";
        }
    }

    //=================================
    //  ランダム移動
    //=================================
    public class Enemy1StateRandMove : Enemy1State
    {
        public override string getStateName()
        {
            return "State:RandMove";
        }
    }

    //=================================
    //  プレイヤー追跡
    //=================================
    public class Enemy1StateChace : Enemy1State
    {
        public override string getStateName()
        {
            return "State:Chace";
        }
    }

    //=================================
    //  攻撃？
    //=================================
    public class Enemy1StateAttack : Enemy1State
    {
        public override string getStateName()
        {
            return "State:Attack";
        }

        //public override void Execute()
        //{
        //    Debug.Log("特別な処理がある場合は子が処理してもよい");
        //    if (execDelegate != null)
        //    {
        //        execDelegate();
        //    }
        //}
    }

}

