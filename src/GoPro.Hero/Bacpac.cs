using System.Text;
using System.Threading.Tasks;
using GoPro.Hero.Commands;
using GoPro.Hero.Filtering;

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

        public Bacpac UpdatePassword(bool nonBlocking = false)
        {
            var task=UpdatePasswordAsync();

            if (!nonBlocking) 
                task.Wait();

            return this;
        }

        public async Task<Bacpac> UpdatePasswordAsync()
        {
            var request = CreateCommand<CommandBacpacRetrievePassword>();
            var response = await request.SendAsync();

            var length = response.RawResponse[1];
            Password = Encoding.UTF8.GetString(response.RawResponse, 2, length);

            return this;
        }

        public Bacpac Shutter(bool open, bool nonBlocking = false)
        {
            var task = ShutterAsync(open);

            if (!nonBlocking)
                task.Wait();

            return this;
        }

        public async Task<Bacpac> ShutterAsync(bool open)
        {
            var request = CreateCommand<CommandBacpacShutter>();
            request.State = open;
            await request.SendAsync();

            await UpdateStatusAsync();
            return this;
        }

        public Bacpac Power(bool on, bool nonBlocking = false)
        {
            var task = PowerAsync(on);

            if (!nonBlocking)
                task.Wait();

            return this;
        }

        public async Task<Bacpac> PowerAsync(bool on)
        {
            var request = CreateCommand<CommandBacpacPowerUp>();
            request.State = on;
            await request.SendAsync();

            await UpdateStatusAsync();
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
            var task = CreateAsync(address);
            task.Wait();

            return task.Result;
        }
    }
}