using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using GoPro.Hero.Api.Commands;
using GoPro.Hero.Api.Commands.CameraCommands;
using GoPro.Hero.Api.Hero3;

namespace GoPro.Hero.Api.Filtering
{
    class FilterGeneric:IFilter<ICamera>
    {
        private const string ROOT = "Filter";
        private const string MODEL = "Model";

        private ICamera _owner;
        private XElement _root;

        public void Initialize(ICamera owner)
        {
            _owner = owner;
        }

        public IEnumerable<T> GetValidStates<T, C>() where C : CommandMultiChoice<T, ICamera>
        {
            var type = typeof(C);
            var elements = _root.Elements(type.Name);
            if (elements.Count() == 0)
                return Utilities.Extensions.GetValues<T>();

            var extendedSettings = _owner.ExtendedSettings;
            var bacpacStatus = _owner.BacpacStatus;
            var selectedConfig = elements.First(element => element.Attributes().All(attribute => HasRequiredSetting(attribute, extendedSettings,bacpacStatus)));
            if (selectedConfig == null) return new T[0];

            var result = selectedConfig.DescendantNodes().Cast<XElement>().Select<XElement, T>(element => (T)Enum.Parse(typeof(T), element.Name.ToString(), true));
            return result;
        }

        public IEnumerable<bool> GetValidStates<C>() where C : CommandBoolean<ICamera>
        {
            var type = typeof(C);
            var elements = _root.Elements(type.Name);
            if (elements.Count() == 0)
                return new[] { true, false };

            var extendedSettings = _owner.ExtendedSettings;
            var bacpacStatus = _owner.BacpacStatus;
            var selectedConfig = elements.First(element => element.Attributes().All(attribute => HasRequiredSetting(attribute, extendedSettings,bacpacStatus)));
            if (selectedConfig == null) return new bool[0];

            var result = selectedConfig.DescendantNodes().Cast<XElement>().Select<XElement,bool>(element => Convert.ToBoolean(element.Name.ToString()));
            return result;
        }

        private bool HasRequiredSetting(XAttribute requiredSetting,CameraExtendedSettings settings,BacpacStatus bacpacStatus)
        {
            if (requiredSetting.Name == MODEL)
                return requiredSetting.Value == bacpacStatus.CameraModel.ToString();

            var type = settings.GetType();
            var property = type.GetProperty(requiredSetting.Name.ToString());
            var value=property.GetValue(settings, null);
            return value.ToString() == requiredSetting.Value;
        }

        private XElement GetFilterProfile(string name)
        {
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name);
            return XElement.Load(stream);
        }

        public FilterGeneric(string profileName)
        {
            var doc=GetFilterProfile(profileName);
            _root = doc.Element(ROOT);
        }

        public static FilterGeneric Create(string profileName)
        {
            var filter = new FilterGeneric(profileName);
            return filter;
        }
    }
}
