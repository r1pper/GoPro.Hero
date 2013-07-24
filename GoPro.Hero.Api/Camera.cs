using System;
using System.Text;
using GoPro.Hero.Api.Browser;
using GoPro.Hero.Api.Commands;
using GoPro.Hero.Api.Filtering;
using GoPro.Hero.Api.Utilities;

namespace GoPro.Hero.Api
{
    public class Camera : ICamera
    {
        private readonly CameraExtendedSettings _extendedSettings;
        private readonly CameraInformation _information;
        private readonly CameraSettings _settings;
        private IFilter<ICamera> _filter;
        protected Bacpac Bacpac;

        public Camera(Bacpac bacpac)
        {
            SetFilter(new NoFilter<ICamera>());

            _information = new CameraInformation();
            _extendedSettings = new CameraExtendedSettings();
            _settings = new CameraSettings();

            this.Bacpac = bacpac;
        }

        public CameraInformation Information
        {
            get
            {
                GetInformation();
                return _information;
            }
        }

        public CameraExtendedSettings ExtendedSettings
        {
            get
            {
                GetExtendedSettings();
                return _extendedSettings;
            }
        }

        public CameraSettings Settings
        {
            get
            {
                GetSettings();
                return _settings;
            }
        }

        public ICamera SetFilter(IFilter<ICamera> filter)
        {
            _filter = filter;
            filter.Initialize(this);
            return this;
        }

        public BacpacStatus BacpacStatus
        {
            get { return Bacpac.Status; }
        }

        public BacpacInformation BacpacInformation
        {
            get { return Bacpac.Information; }
        }

        public string GetName()
        {
            var request = PrepareCommand<CommandCameraGetName>();
            var response = request.Send();

            var raw = response.RawResponse;
            var length = response.RawResponse[1];
            var name = Encoding.UTF8.GetString(raw, 2, length);
            if (!string.IsNullOrEmpty(name)) return name;
            name = Information.Name;
            return name.Fix();
        }

        public ICamera GetName(out string name)
        {
            name = GetName();

            return this;
        }

        public ICamera SetName(string name)
        {
            name = name.UrlEncode();

            var request = PrepareCommand<CommandCameraSetName>();
            request.Name = name;

            request.Send();

            return this;
        }

        public ICamera Shutter(bool open)
        {
            Bacpac.Shutter(open);
            return this;
        }

        public ICamera Power(bool on)
        {
            Bacpac.Power(on);
            return this;
        }

        public ICamera Command(CommandRequest<ICamera> command)
        {
            command.Send();
            return this;
        }

        public ICamera Command(CommandRequest<ICamera> command, out CommandResponse commandResponse,
                               bool checkStatus = true)
        {
            commandResponse = Command(command, checkStatus);
            return this;
        }

        public CommandResponse Command(CommandRequest<ICamera> command, bool checkStatus = true)
        {
            return command.Send(checkStatus);
        }

        public T PrepareCommand<T>() where T : CommandRequest<ICamera>
        {
            return CommandRequest<ICamera>.Create<T>(this, Bacpac.Address, passPhrase: Bacpac.Password);
        }

        public ICamera PrepareCommand<T>(out T command) where T : CommandRequest<ICamera>
        {
            command = PrepareCommand<T>();
            return this;
        }

        object IFilterProvider.Filter()
        {
            return _filter;
        }

        public Node Browse<T>(int port = 8080) where T : IBrowser
        {
            var node = Node.Create<T>(this, new Uri(string.Format("http://{0}:{1}", Bacpac.Address, port)));
            return node;
        }

        private void GetInformation()
        {
            var request = PrepareCommand<CommandCameraInformation>();
            var response = request.Send();

            var stream = response.GetResponseStream();
            _information.Update(stream);
        }

        private void GetSettings()
        {
            var request = PrepareCommand<CommandCameraSettings>();
            var response = request.Send();

            var stream = response.GetResponseStream();
            _settings.Update(stream);
        }

        private void GetExtendedSettings()
        {
            var request = PrepareCommand<CommandCameraExtendedSettings>();
            var response = request.Send();

            var stream = response.GetResponseStream();
            _extendedSettings.Update(stream);
        }

        public static T Create<T>(Bacpac bacpac) where T : Camera, ICamera
        {
            var camera = Activator.CreateInstance(typeof (T), bacpac) as T;
            return camera;
        }

        public static T Create<T>(string address) where T : Camera, ICamera
        {
            var bacpac = Bacpac.Create(address);
            var camera = Create<T>(bacpac);

            return camera;
        }
    }
}