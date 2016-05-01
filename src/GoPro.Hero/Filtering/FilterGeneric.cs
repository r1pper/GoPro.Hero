using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;
using GoPro.Hero.Commands;
using Extensions = GoPro.Hero.Utilities.Extensions;

namespace GoPro.Hero.Filtering
{
    internal class FilterGeneric : IFilter<Camera>
    {
        private const string ROOT = "Filter";
        private const string MODEL = "Model";

        private readonly XElement _root;
        private Camera _owner;

        public FilterGeneric(string profileName)
        {
            _root = GetFilterProfile(profileName);
        }

        public void Initialize(Camera owner)
        {
            _owner = owner;
        }

        public  IEnumerable<TD> GetValidStates<TD, TC>(string command) where TC : CommandMultiChoice<TD, Camera>
        {
            var task = GetValidStatesAsync<TD, TC>(command);
            task.Wait();
            return task.Result;
        }

        public async Task<IEnumerable<TD>> GetValidStatesAsync<TD, TC>(string command) where TC : CommandMultiChoice<TD, Camera>
        {
            var elements = _root.Elements(command).ToArray();
            if (!elements.Any())
                return Extensions.GetValues<TD>();

            var extendedSettings = await _owner.ExtendedSettingsAsync();
            var bacpacStatus = await _owner.BacpacStatusAsync();
            var selectedConfig =
                elements.First(
                    element =>
                    element.Attributes().All(attribute => HasRequiredSetting(attribute, extendedSettings, bacpacStatus)));
            if (selectedConfig == null) return new TD[0];

            var result =
                selectedConfig.DescendantNodes()
                              .Cast<XElement>()
                              .Select(element => (TD) Enum.Parse(typeof (TD), element.Name.ToString(), true));
            return result;
        }

        public  IEnumerable<bool> GetValidStates<TC>(string command) where TC : CommandBoolean<Camera>
        {
            var task = GetValidStatesAsync<TC>(command);
            task.Wait();
            return task.Result;
        }

        public async Task<IEnumerable<bool>> GetValidStatesAsync<TC>(string command) where TC : CommandBoolean<Camera>
        {
            var elements = _root.Elements(command).ToArray();
            if (!elements.Any())
                return new[] {true, false};

            var extendedSettings = await _owner.ExtendedSettingsAsync();
            var bacpacStatus = await _owner.BacpacStatusAsync();
            var selectedConfig =
                elements.First(
                    element =>
                    element.Attributes().All(attribute => HasRequiredSetting(attribute, extendedSettings, bacpacStatus)));
            if (selectedConfig == null) return new bool[0];

            var result =
                selectedConfig.DescendantNodes()
                              .Cast<XElement>()
                              .Select(element => Convert.ToBoolean(element.Name.ToString()));
            return result;
        }

        private bool HasRequiredSetting(XAttribute requiredSetting, CameraExtendedSettings settings,
                                        BacpacStatus bacpacStatus)
        {
            if (requiredSetting.Name == MODEL)
                return requiredSetting.Value == bacpacStatus.CameraModel.ToString();

            var type = settings.GetType();
            var property = type.GetRuntimeProperty(requiredSetting.Name.ToString());
            var value = property.GetValue(settings, null);
            return value.ToString() == requiredSetting.Value;
        }

        private XElement GetFilterProfile(string name)
        {
            var stream = typeof(FilterGeneric).GetTypeInfo().Assembly.GetManifestResourceStream(name);
            return XElement.Load(stream);
        }

        public static FilterGeneric Create(string profileName)
        {
            var filter = new FilterGeneric(profileName);
            return filter;
        }
    }
}