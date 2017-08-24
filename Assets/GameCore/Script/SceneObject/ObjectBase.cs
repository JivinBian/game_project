/********************************************************************************
** 类名称：场景中所有动态生成的对象的基类，作用在于操控模型与数据，处理两者之间的关系，模型不直接对外暴露
** 描述：
** 作者：
** 创建时间：
** 最后修改人：
** 最后修改时间：
** 版权所有 (C) :
*********************************************************************************/

using System;
using GameCore.Script.Common.Interactive;
using GameCore.Script.DataClass.DataConfig;
using GameCore.Script.DataClass.ObjectData;
using GameCore.Script.GameData.DataConfig;
using GameCore.Script.GameManagers.Log;
using GameCore.Script.Interface;
using GameCore.Script.Managers.Time;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameCore.Script.SceneObject
{
    public abstract class ObjectBase
    {
        protected GameObject _contentContainer;
        protected GameObject _content;
        protected ObjectBaseData _objectBaseData;
        protected SceneModel _modelData;
        protected IDataConfigManager _configManager;
        protected IResourceManager _resourceManager;
        protected string _sourcePath = "SceneModel/";

        protected CollideControllerBase _collideController;

        ////
        /// 存储容器的transform
        protected Transform _transform;

        /// ///////
        /// event
        public event Action ModelLoadedCompleteEvent;

        public event Action<Vector3> PositionChanged;

        /// /////////
        protected ObjectBase(ObjectBaseData pBaseData, IDataConfigManager pConfigManager,
            IResourceManager pResourceManager)
        {
            _configManager = pConfigManager;
            _resourceManager = pResourceManager;
            _objectBaseData = pBaseData;
        }

        /// <summary>
        /// 要创建模型，必须要调用Create方法
        /// </summary>
        public virtual ObjectBase Create()
        {
            BindingEvent();
            ParseModelData();
            CreateContainer();
            return this;
        }

        /// <summary>
        /// 创建一个模型容器，也可以由外部加载，但不要在这个方法中调用需要在构造函数中初始化的一些属性
        /// 创建容器后一定要调用ContainerCreateComplete方法
        /// </summary>
        /// <returns></returns>
        protected virtual void CreateContainer()
        {
            _contentContainer = new GameObject(GetContainerName());
            ContainerCreateComplete();
        }

        /// <summary>
        /// 容易加载后再进行模型及其它内部东西的加载，比如可以初始化阴影及其它的物体在容器内
        /// </summary>
        protected virtual void ContainerCreateComplete()
        {
            _transform = _contentContainer.transform;
            LoadContent();
            InitData();
        }

        protected virtual void InitData()
        {
            SetSelfPosition(_objectBaseData.Position);
        }

        /// <summary>
        /// 返回一个模型的ModelData，不要在这个方法中调用需要在构造函数中初始化的一些属性
        /// </summary>
        /// <returns></returns>
        protected virtual void ParseModelData()
        {
            _modelData = _configManager.GetConfigData<SceneModel>(DataConfigDefine.SceneModel, _objectBaseData.ModelId);
        }

        protected virtual void LoadContent()
        {
            _resourceManager.Load(ContentSourcePath, null, ContentLoadedComplete);
        }

        protected virtual string ContentSourcePath
        {
            get { return _sourcePath + _modelData.ModelName; }
        }

        private void ContentLoadedComplete(Object pObject, params object[] pParams)
        {
            if (pObject != null)
            {
                _content = GameObject.Instantiate(pObject) as GameObject;
                _content.name = GetContentObjectName();
            }
            ModelLoadedCompleteEvent();
        }

        protected virtual string GetContainerName()
        {
            return _objectBaseData.Guid.ToString();
        }

        /// <summary>
        /// 主体模型的名字
        /// </summary>
        /// <returns></returns>
        protected virtual string GetContentObjectName()
        {
            return "Body";
        }

        protected virtual void OnLoadModelComplete()
        {
            InitContent();
            InitCollider();
        }

        /// <summary>
        /// 从配置中读出基础属性给Content
        /// </summary>
        protected virtual void InitContent()
        {
            _content.transform.parent = _contentContainer.transform;
            _content.transform.localPosition = _objectBaseData.Offset; //这里用Offset,表示对容器的偏移
            _content.transform.localRotation = Quaternion.Euler(_objectBaseData.Rotaion);
            _content.transform.localScale = _objectBaseData.Size;
        }

        private void InitCollider()
        {
            _collideController = new CollideMouseController(_content.transform);
            _collideController.PressEvent += OnPress;
            _collideController.ReleaseEvent += OnRelease;
            _collideController.ClickEvent += OnClick;
            var tCollider = _content.AddComponent<CapsuleCollider>();
            tCollider.height = _objectBaseData.Height;
            tCollider.radius = _objectBaseData.Radius;
            tCollider.center = new Vector3(0, _objectBaseData.Height / 2, 0);
        }

        public virtual bool TouchEnabled
        {
            get { return _collideController.Enabled; }
            set { _collideController.Enabled = value; }
        }

        protected virtual void OnPress(Transform pTarget, Vector3 pTargetPoint)
        {
        }

        protected virtual void OnRelease(Transform pTarget, Vector3 pTargetPoint)
        {
        }

        protected virtual void OnClick(Transform pTarget, Vector3 pTargetPoint)
        {
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// 事件相关
        protected virtual void BindingEvent()
        {
            PositionChanged += SetSelfPosition;
            ModelLoadedCompleteEvent += OnLoadModelComplete;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///基础属性操作相关
        /// 位置
        private void SetSelfPosition(Vector3 pPosition)
        {
            _transform.position = pPosition;
        }

        public void SetPosition(Vector3 pPosition)
        {
            _objectBaseData.Position = pPosition;
            PositionChanged(pPosition);
        }

        public void SetPosition(float pX, float pY, float pZ)
        {
            _objectBaseData.Position = new Vector3(pX, pY, pZ);
            PositionChanged(_objectBaseData.Position);
        }

        public Vector3 GetPosition()
        {
            return _objectBaseData.Position;
        }

        //朝向
        private void SetSelfRotation(float pAngle)
        {
            _transform.localRotation = Quaternion.Euler(0, pAngle, 0);
        }

        public void FaceTo(Vector3 pTargetPosition)
        {
            _transform.LookAt(pTargetPosition);
            _objectBaseData.Direction = _transform.localRotation.eulerAngles.y;
        }

        public void SetRotation(float pAngle)
        {
            _objectBaseData.Direction = pAngle;
            SetSelfRotation(pAngle);
        }

        public float GetRotation()
        {
            return _transform.localRotation.eulerAngles.y;
        }
    }
}