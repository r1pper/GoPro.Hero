using System;
using System.Text;
using System.Threading.Tasks;
using GoPro.Hero.Browser;
using GoPro.Hero.Browser.FileSystem;
using GoPro.Hero.Commands;
using GoPro.Hero.Filtering;
using GoPro.Hero.Utilities;

namespace GoPro.Hero.Hero3
{
    public class LegacyCamera : ICamera<LegacyCamera>,IFilterProvider<LegacyCamera>
    {
        private readonly CameraExtendedSettings _extendedSettings;
        private readonly CameraInformation _information;
        private readonly CameraSettings _settings;
        private CameraCapabilities _capabilities;
        private IFilter<LegacyCamera> _filter;
        protected Bacpac Bacpac;


        public LegacyCamera(Bacpac bacpac)
        {
            SetFilter(new NoFilter<LegacyCamera>());

            _information = new CameraInformation();
            _extendedSettings = new CameraExtendedSettings();
            _settings = new CameraSettings();

            Bacpac = bacpac;
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

        public CameraCapabilities Capabilities()
        {
            GetCapabilities();
            return _capabilities;
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

        private void GetCapabilities()
        {
            var task = GetCapabilitiesAsync();
            task.Wait();
        }

        private async Task GetCapabilitiesAsync(int capabilityLevel=1)
        {
            var request = PrepareCommand<CommandCameraCapabilities>();
            var response = await request.SendAsync(false);

            _capabilities=CameraCapabilities.Parse(response.RawResponse,capabilityLevel);
        }

        public LegacyCamera SetFilter(IFilter<LegacyCamera> filter)
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

        public async Task<CameraCapabilities> CapabilitiesAsync()
        {
            await GetCapabilitiesAsync();
            return _capabilities;
        }

        public CameraCapabilities CapabilitiesCache()
        {
            return _capabilities;
        }

        public BacpacStatus BacpacStatus()
        {
            return Bacpac.Status();
        }

        public async Task<BacpacStatus> BacpacStatusAsync()
        {
            return await Bacpac.StatusAsync();
        }

        public BacpacStatus BacpacStatusCache()
        {
            return Bacpac.StatusCache();
        }

        public BacpacInformation BacpacInformation()
        {
            return Bacpac.Information();
        }

        public async Task<BacpacInformation> BacpacInformationAsync()
        {
            return await Bacpac.InformationAsync();
        }

        public BacpacInformation BacpacInformationCache()
        {
            return Bacpac.InformationCache();
        }

        public string GetName()
        {
            var task = GetNameAsync();
            task.Wait();

            return task.Result;
        }

        public async Task<string> GetNameAsync()
        {
            var request = PrepareCommand<CommandCameraGetName>();
            var response = await request.SendAsync();

            var raw = response.RawResponse;
            var length = response.RawResponse[1];
            var name = Encoding.UTF8.GetString(raw, 2, length);
            if (!string.IsNullOrEmpty(name)) return name;
            name = Information().Name;
            return name.Fix();
        }

        public async Task SetNameAsync(string name)
        {
            name = name.UrlEncode();

            var request = PrepareCommand<CommandCameraSetName>();
            request.Name = name;

            await request.SendAsync();
        }

        public LegacyCamera SetName(string name)
        {
            AsyncHelpers.RunSync (()=>SetNameAsync(name));
            return this;
        }

        public LegacyCamera Shutter(bool open)
        {
            Bacpac.Shutter(open);
            return this;
        }

        public async Task ShutterAsync(bool open)
        {
            await Bacpac.ShutterAsync(open);
        }

        public LegacyCamera Power(bool on)
        {
            Bacpac.Power(on);
            return this;
        }

        public async Task PowerAsync(bool on)
        {
            await Bacpac.PowerAsync(on);
        }

        public LegacyCamera Command(CommandRequest<LegacyCamera> command)
        {
            command.Send();
            return this;
        }

        public async Task CommandAsync(CommandRequest<LegacyCamera> command)
        {
            await command.SendAsync();
        }

        public CommandResponse Command(CommandRequest<LegacyCamera> command, bool checkStatus = true)
        {
            return command.Send(checkStatus);
        }

        public async Task<CommandResponse> CommandAsync(CommandRequest<LegacyCamera> command, bool checkStatus = true)
        {
            return await command.SendAsync(checkStatus);
        }

        public T PrepareCommand<T>() where T : CommandRequest<LegacyCamera>
        {
            return CommandRequest<LegacyCamera>.Create<T>(this, Bacpac.Address, passPhrase: Bacpac.Password);
        }

        public T PrepareCommand<T>(int port) where T : CommandRequest<LegacyCamera>
        {
            return CommandRequest<LegacyCamera>.Create<T>(this, string.Format("{0}:{1}", Bacpac.Address, port), passPhrase: Bacpac.Password);
        }

        IFilter<LegacyCamera> IFilterProvider<LegacyCamera>.Filter()
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

        public string MacAddress()
        {
            return BacpacInformationCache().MacAddress;
        }

        public string IpAddress()
        {
            return Bacpac.Address;
        }

        public string Password()
        {
            return Bacpac.Password;
        }

        public Version BootLoader()
        {
            return BacpacInformationCache().BootloaderVersion;
        }

        public Version Firmware()
        {
            return BacpacInformationCache().FirmwareVersion;
        }

        public string Ssid()
        {
            return BacpacInformationCache().Ssid;
        }

        public string Version()
        {
            return InformationCache().Version;
        }

        public Model Model()
        {
            return BacpacStatusCache().CameraModel;
        }

        public static T Create<T>(Bacpac bacpac) where T : LegacyCamera
        {
            var task = CreateAsync<T>(bacpac);
            task.Wait();

            return task.Result;
        }

        public ICameraFacade<LegacyCamera> UnifiedApi()
        {
            return new LegacyFacade(this);
        }

        public static async Task<T> CreateAsync<T>(Bacpac bacpac) where T : LegacyCamera
        {
            var camera = Activator.CreateInstance(typeof(T), bacpac) as T;

            var power = (await camera.BacpacStatusAsync()).CameraPower;

            if (!power)
                return camera;

            await camera.InformationAsync();
            await camera.SettingsAsync();
            await camera.ExtendedSettingsAsync();
            await camera.CapabilitiesAsync();

            return camera;
        }

        public static T Create<T>(string address) where T : LegacyCamera
        {
            return AsyncHelpers.RunSync(()=>CreateAsync<T>(address));
        }

        public static async Task<T> CreateAsync<T>(string address) where T : LegacyCamera
        {
            var bacpac = await Bacpac.CreateAsync(address);
            var camera = await CreateAsync<T>(bacpac);

            return camera;
        }


        void ICamera.Command(ICommandRequest command)
        {
            command.Execute();
        }

        async Task ICamera.CommandAsync(ICommandRequest command)
        {
            await command.ExecuteAsync();
        }

        CommandResponse ICamera.Command(ICommandRequest command, bool checkStatus)
        {
            return command.Send(checkStatus);
        }

        async Task<CommandResponse> ICamera.CommandAsync(ICommandRequest command, bool checkStatus)
        {
            return await command.SendAsync(checkStatus);
        }

        T ICamera.PrepareCommand<T>()
        {
            return CommandRequest.Create<T>(Bacpac.Address, passPhrase: Bacpac.Password);
        }

        T ICamera.PrepareCommand<T>(int port)
        {
            return CommandRequest.Create<T>(string.Format("{0}:{1}", Bacpac.Address, port), passPhrase: Bacpac.Password);
        }
    }
}