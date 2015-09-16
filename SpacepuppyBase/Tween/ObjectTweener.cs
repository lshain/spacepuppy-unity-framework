﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

using com.spacepuppy.Utils;
using System;

namespace com.spacepuppy.Tween
{
    public class ObjectTweener : Tweener
    {

        #region Fields

        private object _target;
        private TweenCurve _curve;
        private object _id;
        private object _tokenUid;

        #endregion
        
        #region CONSTRUCTOR

        public ObjectTweener(object targ, TweenCurve curve)
        {
            if (targ == null) throw new System.ArgumentNullException("targ");
            if (curve == null) throw new System.ArgumentNullException("curve");
            if (curve.Tween != null) throw new System.ArgumentException("Tweener can only be created with an unregistered Curve.", "curve");

            _target = targ;
            _curve = curve;
            _curve.Init(this);
        }

        #endregion

        #region Properties

        public override object Id
        {
            get
            {
                return (_id != null) ? _id : _target;
            }
            set
            {
                _id = value;
            }
        }

        public object Target { get { return _target; } }

        public object AutoKillToken
        {
            get { return _tokenUid; }
            set { _tokenUid = value; }
        }

        #endregion

        #region Tweener Interface

        protected override float GetPlayHeadLength()
        {
            return _curve.TotalTime;
        }

        protected override void DoUpdate(float dt, float t)
        {
            if (_target.IsNullOrDestroyed())
            {
                this.Stop();
                return;
            }
            _curve.Update(_target, dt, t);
        }

        #endregion

        #region Special Types

        private struct TokenPairing
        {
            public object Target;
            public object TokenUid;

            public TokenPairing(object targ, object uid)
            {
                this.Target = targ;
                this.TokenUid = uid;
            }

            //public override bool Equals(object obj)
            //{
            //    if (obj == null) return false;
            //    if (!(obj is TokenPairing)) return false;

            //    var token = (TokenPairing)obj;
            //    return token.Target == this.Target && token.TokenUid == this.TokenUid;
            //}
        }

        #endregion

    }
}
