using Leeax.Web.Components;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ComponentsDemo.Components
{
    public partial class ComponentInfo
    {
        private ComponentReflector _reflector;
        private Type _type;
        private IConverter _converter;

        protected IEnumerable<ParameterInfo> parameters;
        protected IEnumerable<CascadingParameterInfo> cascadingParameters;
        protected IEnumerable<Type> subComponents;

        private void UpdateReflector()
        {
            _reflector = new ComponentReflector(Type);
            _converter = new TypeStringConverter();
            parameters = _reflector.GetParameterInfos().Where(x => !x.CaptureUnmatchedValues).ToList();
            cascadingParameters = _reflector.GetCascadingParameterInfos().ToList();
            subComponents = ComponentReflector.GetSubComponentTypes(Type).ToList();
        }

        [Parameter]
        public Type Type
        {
            get => _type;
            set
            {
                _type = value;
                UpdateReflector();
            }
        }

        //[Parameter]
        //public RenderFragment Demo { get; set; }
    }
}