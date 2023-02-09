using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using Code.LocomotiveSystems;
using Controls.ControlNames;
using Controls.ControlsAbstracts;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UI.ControlsView.UIComponents;
using UI.MPSU;

namespace Controls.ScreenControlsAbstract
{
    public abstract class ScreenControlAbstract<TValueType> : SerializedMonoBehaviour
    {
        [TypeFilter("GetFilteredTypeList")]
        [HideLabel]
        public BaseScreenModule ControlsModule;
        
        
        internal CompositeDisposable Disposables;
        internal Enum ControlName;
        
        internal virtual TValueType Value { get ; set ; }
            
        public IEnumerable<Type> GetFilteredTypeList()
        {
            var q = typeof(BaseScreenModule).Assembly.GetTypes()
                .Where(x => !x.IsAbstract) // Excludes BaseControl
                .Where(x => !x.IsGenericTypeDefinition) // Excludes BaseModule<>
                .Where(x => typeof(BaseScreenModule).IsAssignableFrom(x)); // Excludes classes not inheriting from BaseClass
        
            // Adds various BaseModule<> type variants.
            q = q.AppendWith(typeof(Module<>).MakeGenericType(typeof(MpsuScreenTopPanelControls)));
            q = q.AppendWith(typeof(Module<>).MakeGenericType(typeof(MpsuScreenButtonControls)));
            q = q.AppendWith(typeof(Module<>).MakeGenericType(typeof(MpsuScreenNames)));
            q = q.AppendWith(typeof(Module<>).MakeGenericType(typeof(LocomotiveSystem)));
            
            return q;
        }
        
        public virtual void Start()
        {
            Value = default;
        }

        public virtual void Awake()
        {
            ControlName = GetControlName();
        }
        
        private Enum GetControlName()
        {
            return (Enum) ControlsModule
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
    }
    
    public abstract class BaseScreenModule{}

    public class Module<T> : BaseScreenModule
    {
        public T ControlName;
    }
}