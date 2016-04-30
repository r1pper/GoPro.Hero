using System.Text;
using System.Threading.Tasks;
using GoPro.Hero.Commands;
using GoPro.Hero.Filtering;
using GoPro.Hero.Utilities;

namespace GoPro.Hero
{
    public sealed class Bacpac : IFilterProvider
    {
        private readonly IFilter<Bacpac> _filter;
        private readonly BacpacInformation _information;
        private readonly BacpacStatus _status;

        private Bacpac(string address)
        {
            Address = address;
            _information = new BacpacInformation();
            _status = new BacpacStatus();
            _filter = new NoFilter<Bacpac>();
            _filter.Initialize(this);
        }

        public string Password { get; private set; }
        public string Address { get; private set; }

        public BacpacInformation Information()
        {
            UpdateInformation();
            return _information;
        }

        public async Task<BacpacInformation> InformationAsync()
        {
            await UpdateInformationAsync();

            return _information;
        }

        public BacpacInformation InformationCache()
        {
            return _information;
        }

        private void UpdateInformation()
        {
            var task = UpdateInformationAsync();
            task.Wait();
        }

        private async Task UpdateInformationAsync()
        {
            var request = CreateCommand<CommandBacpacInformation>();
            var response = await request.SendAsync();

            var stream = response.GetResponseStream();
            _information.Update(stream);
        }

        public BacpacStatus Status()
        {
            UpdateStatus();
            return _status;
        }

        public async Task<BacpacStatus> StatusAsync()
        {
            await UpdateStatusAsync();

            return _status;
        }

        public BacpacStatus StatusCache()
        {
            return _status;
        }

        private void UpdateStatus()
        {
            var task = UpdateStatusAsync();
            task.Wait();
        }

        private async Task UpdateStatusAsync()
        {
            var request = CreateCommand<CommandBacpacStatus>();
            var response = await request.SendAsync();

            var stream = response.GetResponseStream();
            _status.Update(stream);
        }

        object IFilterProvider.Filter()
        {
            return _filter;
        }

        public Bacpac UpdatePassword()
        {
            AsyncHelpers.RunSync(UpdatePasswordAsync);
            return this;
        }

        public async Task UpdatePasswordAsync()
        {
            var request = CreateCommand<CommandBacpacRetrievePassword>();
            var response = await request.SendAsync();

            var length = response.RawResponse[1];
            Password = Encoding.UTF8.GetString(response.RawResponse, 2, length);
        }

        public Bacpac Shutter(bool open)
        {
            AsyncHelpers.RunSync(() => ShutterAsync(open));
            return this;
        }

        public async Task ShutterAsync(bool open)
        {
            var request = CreateCommand<CommandBacpacShutter>();
            request.State = open;
            await request.SendAsync();

            await UpdateStatusAsync();
        }

        public Bacpac Power(bool on)
        {
            AsyncHelpers.RunSync(() => PowerAsync(on));
            return this;
        }

        public async Task PowerAsync(bool on)
        {
            var request = CreateCommand<CommandBacpacPowerUp>();
            request.State = on;
            await request.SendAsync();

            await UpdateStatusAsync();
        }

        public async Task ResetAsync()
        {
            var request = CreateCommand<CommandBacpacReset>();
            await request.SendAsync(false);
        }

        public Bacpac Reset()
        {
            AsyncHelpers.RunSync(ResetAsync);
            return this;
        }

        public async Task ConfigureWifiAsync(string name, string password)
        {
            var request = CreateCommand<CommandBacpacWifiConfigure>().SetName(name).SetPassword(password);
            await request.SendAsync();
        }

        public Bacpac ConfigureWifi(string name, string password, bool nonBlocking = false)
        {
            AsyncHelpers.RunSync(() => ConfigureWifiAsync(name, password));
            return this;
        }

        private T CreateCommand<T>(string parameter = null) where T : CommandRequest<Bacpac>
        {
            var request = CommandRequest<Bacpac>.Create<T>(this, Address, passPhrase: Password, parameter: parameter);
            return request;
        }

        public static async Task<Bacpac> CreateAsync(string address)
        {
            var bacpac = new Bacpac(address);

            await bacpac.UpdatePasswordAsync();
            await bacpac.UpdateInformationAsync();
            await bacpac.UpdateStatusAsync();

            return bacpac;
        }

        public static Bacpac Create(string address)
        {
            return AsyncHelpers.RunSync(() => CreateAsync(address));
        }
    }
}