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
        private CameraCapabilities _capabilities;
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

        public ICamera SetName(string name)
        {
            AsyncHelpers.RunSync (()=>SetNameAsync(name));
            return this;
        }

        public ICamera Shutter(bool open)
        {
            Bacpac.Shutter(open);
            return this;
        }

        public async Task ShutterAsync(bool open)
        {
            await Bacpac.ShutterAsync(open);
        }

        public ICamera Power(bool on)
        {
            Bacpac.Power(on);
            return this;
        }

        public async Task PowerAsync(bool on)
        {
            await Bacpac.PowerAsync(on);
        }

        public ICamera Command(CommandRequest<ICamera> command)
        {
            command.Send();
            return this;
        }

        public async Task CommandAsync(CommandRequest<ICamera> command)
        {
            await command.SendAsync();
        }

        public CommandResponse Command(CommandRequest<ICamera> command, bool checkStatus = true)
        {
            return command.Send(checkStatus);
        }

        public async Task<CommandResponse> CommandAsync(CommandRequest<ICamera> command, bool checkStatus = true)
        {
            return await command.SendAsync(checkStatus);
        }

        public T PrepareCommand<T>() where T : CommandRequest<ICamera>
        {
            return CommandRequest<ICamera>.Create<T>(this, Bacpac.Address, passPhrase: Bacpac.Password);
        }

        public T PrepareCommand<T>(int port) where T : CommandRequest<ICamera>
        {
            return CommandRequest<ICamera>.Create<T>(this, string.Format("{0}:{1}", Bacpac.Address, port), passPhrase: Bacpac.Password);
        }

        IFilter<ICamera> IFilterProvider<ICamera>.Filter()
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

        public ICamera Chain(params Func<ICamera, Task>[] fs)
        {
            foreach (var f in fs)
                AsyncHelpers.RunSync(() => f(this));

            return this;
        }

        public async Task ChainAsync(params Func<ICamera, Task>[] fs)
        {
            foreach (var f in fs)
                await f(this);
        }

        public ICamera Chain<T>(Func<ICamera, T> f, out T output)
        {
            output = f(this);
            return this;
        }

        public ICamera Chain<T>(Func<ICamera, Task<T>> f, out T output)
        {
            output = AsyncHelpers.RunSync(() => f(this));
            return this;
        }

        public ICamera Chain<T>(Func<ICamera, T> f, Action<T> output)
        {
            output(f(this));
            return this;
        }

        public async Task ChainAsync<T>(Func<ICamera, Task<T>> f, Action<T> output)
        {
            output(await f(this));
        }

        public ICamera Chain<T>(Func<ICamera, Task<T>> f, Action<T> output)
        {
            output(AsyncHelpers.RunSync(() => f(this)));
            return this;
        }

        public static T Create<T>(Bacpac bacpac) where T : Camera, ICamera
        {
            var task = CreateAsync<T>(bacpac);
            task.Wait();

            return task.Result;
        }

        public static async Task<T> CreateAsync<T>(Bacpac bacpac) where T : Camera, ICamera
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

        public static T Create<T>(string address) where T : Camera, ICamera
        {
            var task = CreateAsync<T>(address);
            task.Wait();

            return task.Result;
        }

        public static async Task<T> CreateAsync<T>(string address) where T : Camera, ICamera
        {
            var bacpac = await Bacpac.CreateAsync(address);
            var camera = await CreateAsync<T>(bacpac);

            return camera;
        }
    }
}