using System;
using System.Text;
using System.Threading.Tasks;
using GoPro.Hero.Browser;
using GoPro.Hero.Browser.FileSystem;
using GoPro.Hero.Commands;
using GoPro.Hero.Filtering;
using GoPro.Hero.Utilities;

namespace GoPro.Hero
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

        public CameraInformation Information()
        {
                GetInformation();
                return _information;
        }

        public CameraExtendedSettings ExtendedSettings()
        {
                GetExtendedSettings();
                return _extendedSettings;
        }

        public CameraSettings Settings()
        {
                GetSettings();
                return _settings;
        }

        private void GetInformation()
        {
            var task = GetInformationAsync();
            task.Wait();
        }

        private async Task GetInformationAsync()
        {
            var request = PrepareCommand<CommandCameraInformation>();
            var response = await request.SendAsync();

            var stream = response.GetResponseStream();
            _information.Update(stream);
        }

        private void GetSettings()
        {
            var task = GetSettingsAsync();
            task.Wait();
        }

        private async Task GetSettingsAsync()
        {
            var request = PrepareCommand<CommandCameraSettings>();
            var response = await request.SendAsync();

            var stream = response.GetResponseStream();
            _settings.Update(stream);
        }

        private void GetExtendedSettings()
        {
            var task = GetExtendedSettingsAsync();
            task.Wait();
        }

        private async Task GetExtendedSettingsAsync()
        {
            var request = PrepareCommand<CommandCameraExtendedSettings>();
            var response = await request.SendAsync();

            var stream = response.GetResponseStream();
            _extendedSettings.Update(stream);
        }

        public ICamera SetFilter(IFilter<ICamera> filter)
        {
            _filter = filter;
            filter.Initialize(this);
            return this;
        }

        public async Task<CameraInformation> InformationAsync()
        {
            await GetInformationAsync();
            return _information;
        }

        public CameraInformation InformationCache()
        {
            return _information;
        }

        public async Task<CameraSettings> SettingsAsync()
        {
            await GetSettingsAsync();
            return _settings;
        }

        public CameraSettings SettingsCache()
        {
            return _settings;
        }

        public async Task<CameraExtendedSettings> ExtendedSettingsAsync()
        {
            await GetExtendedSettingsAsync();
            return _extendedSettings;
        }

        public CameraExtendedSettings ExtendedSettingsCache()
        {
            return _extendedSettings;
        }

        public BacpacStatus BacpacStatus()
        {
            return Bacpac.Status();
        }

        public async Task<BacpacStatus> BacpacStatusAsync()
        {
            return await Bacpac.StatusAsync();
        }

        public BacpacInformation BacpacInformation()
        {
            return Bacpac.Information();
        }

        public async Task<BacpacInformation> BacpacInformationAsync()
        {
            return await Bacpac.InformationAsync();
        }

        public string GetName()
        {
            var request = PrepareCommand<CommandCameraGetName>();
            var response = request.Send();

            var raw = response.RawResponse;
            var length = response.RawResponse[1];
            var name = Encoding.UTF8.GetString(raw, 2, length);
            if (!string.IsNullOrEmpty(name)) return name;
            name = Information().Name;
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

        public T PrepareCommand<T>(int port) where T : CommandRequest<ICamera>
        {
            return CommandRequest<ICamera>.Create<T>(this, string.Format("{0}:{1}", Bacpac.Address, port), passPhrase: Bacpac.Password);
        }

        public ICamera PrepareCommand<T>(out T command) where T : CommandRequest<ICamera>
        {
            command = PrepareCommand<T>();
            return this;
        }

        public ICamera PrepareCommand<T>(int port, out T command) where T : CommandRequest<ICamera>
        {
            command = PrepareCommand<T>(port);
            return this;
        }

        object IFilterProvider.Filter()
        {
            return _filter;
        }

        public T Browse<T>(int port = 8080) where T : IGeneralBrowser
        {
            var instance = Activator.CreateInstance<T>();
            instance.Initialize(this,new Uri(string.Format("http://{0}:{1}", Bacpac.Address, port)));
            return instance;
        }

        public Node FileSystem<T>(int port = 8080) where T : IFileSystemBrowser
        {
            var node = Node.Create<T>(this, new Uri(string.Format("http://{0}:{1}", Bacpac.Address, port)));
            return node;
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