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

        public ICamera GetNameAsync(Action<string> result)
        {
            GetNameAsync().Result(result);
            return this;
        }

        public ICamera GetName(out string name)
        {
            name = GetName();
            return this;
        }

        public async Task<ICamera> SetNameAsync(string name)
        {
            name = name.UrlEncode();

            var request = PrepareCommand<CommandCameraSetName>();
            request.Name = name;

            await request.SendAsync();

            return this;
        }

        public ICamera SetName(string name, bool nonBlocking = false)
        {
            var task = SetNameAsync(name);

            if (!nonBlocking)
                task.Wait();

            return this;
        }

        public ICamera Shutter(bool open, bool nonBlocking = false)
        {
            Bacpac.Shutter(open, nonBlocking);
            return this;
        }

        public async Task<ICamera> ShutterAsync(bool open)
        {
            await Bacpac.ShutterAsync(open);
            return this;
        }

        public ICamera Power(bool on, bool nonBlocking = false)
        {
            Bacpac.Power(on , nonBlocking);
            return this;
        }

        public async Task<ICamera> PowerAsync(bool on, bool nonBlocking = false)
        {
            await Bacpac.PowerAsync(on);
            return this;
        }

        public ICamera Command(CommandRequest<ICamera> command)
        {
            command.Send();
            return this;
        }

        public async Task<ICamera> CommandAsync(CommandRequest<ICamera> command)
        {
            await command.SendAsync();
            return this;
        }

        public ICamera Command(CommandRequest<ICamera> command, out CommandResponse commandResponse,
                               bool checkStatus = true)
        {
            commandResponse = Command(command, checkStatus);
            return this;
        }

        public async Task<ICamera> CommandAsync(CommandRequest<ICamera> command, Action<CommandResponse> result,
                       bool checkStatus = true)
        {
            var response = await CommandAsync(command, checkStatus);
            result(response);
            return this;
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