using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using Controls.ControlNames;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UI.ControlsView.UIComponents;
using UI.CustomTouchInput;
using Code.LocomotiveSystems;
using UI.MPSU;
using UI.Msud;
using UnityEngine.EventSystems;


namespace Controls.ControlsAbstracts
{
    public abstract class AbstractControl<TValueType> : SerializedMonoBehaviour, ITouchHandler
    {
        [TypeFilter("GetFilteredTypeList")]
        [HideLabel]
        public BaseModule ControlsModule;

        internal CompositeDisposable Disposables;
        internal Enum ControlName;

        internal virtual TValueType Value { get; set; }
        public IEnumerable<Type> GetFilteredTypeList()
        {
            var q = typeof(BaseModule).Assembly.GetTypes()
                .Where(x => !x.IsAbstract) // Excludes BaseControl
                .Where(x => !x.IsGenericTypeDefinition) // Excludes BaseModule<>
                .Where(x => typeof(BaseModule).IsAssignableFrom(x)); // Excludes classes not inheriting from BaseClass

            // Adds various BaseModule<> type variants.
            q = q.AppendWith(typeof(Module<>).MakeGenericType(typeof(MsudControls)));
            q = q.AppendWith(typeof(Module<>).MakeGenericType(typeof(LocomotiveControls)));
            q = q.AppendWith(typeof(Module<>).MakeGenericType(typeof(UIControls)));
            q = q.AppendWith(typeof(Module<>).MakeGenericType(typeof(Klub)));
            q = q.AppendWith(typeof(Module<>).MakeGenericType(typeof(Saut)));
            q = q.AppendWith(typeof(Module<>).MakeGenericType(typeof(Bil)));
            q = q.AppendWith(typeof(Module<>).MakeGenericType(typeof(LocomotiveSystemsOutPutNames)));
            q = q.AppendWith(typeof(Module<>).MakeGenericType(typeof(MpsuControls)));

            return q;
        }

        private void Start()
        {
            Value = default;
        }

        public virtual void Awake()
        {
            ControlName = GetControlName();
        }

        private Enum GetControlName()
        {
            return (Enum)ControlsModule
                .GetType()
                .GetField("ControlName")
                .GetValue(ControlsModule);
        }

        public virtual void OnEnable()
        {
            Disposables = new CompositeDisposable();
        }

        public virtual void OnDisable()
        {
            Disposables?.Dispose();
        }

        internal abstract void OnControlInteraction();

        public abstract void OnTouchBegun(PointerEventData eventData);

        public abstract void OnTouchEnded(PointerEventData eventData);

        public abstract void OnTouchMoved(PointerEventData eventData);
    }

    public abstract class BaseModule { }

    public class Module<T> : BaseModule
    {
        public T ControlName;
    }
}
