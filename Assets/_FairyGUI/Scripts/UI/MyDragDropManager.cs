using System;
using System.Collections.Generic;
using UnityEngine;

namespace FairyGUI
{
    /// <summary>
    /// Helper for drag and drop.
    /// 这是一个提供特殊拖放功能的功能类。与GObject.draggable不同，拖动开始后，他使用一个替代的图标作为拖动对象。
    /// 当玩家释放鼠标/手指，目标组件会发出一个onDrop事件。
    /// </summary>
    public class MyDragDropManager
    {
        private GComponent _agent;
        private GLoader loader_a;
        private GLoader loader_b;
        private object _sourceData;

        private static MyDragDropManager _inst;
        public static MyDragDropManager inst
        {
            get
            {
                if (_inst == null)
                    _inst = new MyDragDropManager();
                return _inst;
            }
        }

        public MyDragDropManager()
        {
            _agent = (GComponent)UIObjectFactory.NewObject("component");
            _agent.gameObjectName = "MyDragDropAgent";
            _agent.SetHome(GRoot.inst);
            _agent.touchable = false;//important
            _agent.draggable = true;
            //_agent.SetSize(100, 100);
            
            _agent.sortingOrder = int.MaxValue;
            _agent.onDragEnd.Add(__dragEnd);

            loader_a = (GLoader)UIObjectFactory.NewObject("loader");
            loader_a.SetPivot(0.5f, 0.5f, true);
            loader_a.align = AlignType.Center;
            loader_a.verticalAlign = VertAlignType.Middle;

            loader_b = (GLoader)UIObjectFactory.NewObject("loader");
            loader_b.SetPivot(0.5f, 0.5f, true);
            loader_b.align = AlignType.Center;
            loader_b.verticalAlign = VertAlignType.Middle;

            _agent.AddChild(loader_a);
            _agent.AddChild(loader_b);
        }

        /// <summary>
        /// Loader object for real dragging.
        /// 用于实际拖动的Loader对象。你可以根据实际情况设置loader的大小，对齐等。
        /// </summary>
        public GComponent dragAgent
        {
            get { return _agent; }
        }

        public GLoader dragAgent_a
        {
            get { return loader_a; }
        }

        public GLoader dragAgent_b
        {
            get { return loader_b; }
        }

        /// <summary>
        /// Is dragging?
        /// 返回当前是否正在拖动。
        /// </summary>
        public bool dragging
        {
            get { return _agent.parent != null; }
        }

        /// <summary>
        /// Start dragging.
        /// 开始拖动。
        /// </summary>
        /// <param name="source">Source object. This is the object which initiated the dragging.</param>
        /// <param name="icon">Icon to be used as the dragging sign.</param>
        /// <param name="sourceData">Custom data. You can get it in the onDrop event data.</param>
        /// <param name="touchPointID">Copy the touchId from InputEvent to here, if has one.</param>
        public void StartDrag(GObject source, string icon, string icon2, object sourceData, int touchPointID = -1)
        {
            if (_agent.parent != null)
                return;
            
            loader_a.url = icon;
            loader_b.url = icon2;
            
            _sourceData = sourceData;
            GRoot.inst.AddChild(_agent);
            _agent.xy = GRoot.inst.GlobalToLocal(Stage.inst.GetTouchPosition(touchPointID));
            _agent.StartDrag(touchPointID);
        }

        /// <summary>
        /// Cancel dragging.
        /// 取消拖动。
        /// </summary>
        public void Cancel()
        {
            if (_agent.parent != null)
            {
                _agent.StopDrag();
                GRoot.inst.RemoveChild(_agent);
                _sourceData = null;
            }
        }

        private void __dragEnd(EventContext evt)
        {
            if (_agent.parent == null) //cancelled
                return;

            GRoot.inst.RemoveChild(_agent);

            object sourceData = _sourceData;
            _sourceData = null;

            GObject obj = GRoot.inst.touchTarget;
            while (obj != null)
            {
                if (obj is GComponent)
                {
                    if (!((GComponent)obj).onDrop.isEmpty)
                    {
                        obj.RequestFocus();
                        ((GComponent)obj).onDrop.Call(sourceData);
                        return;
                    }
                }

                obj = obj.parent;
            }
        }
    }
}