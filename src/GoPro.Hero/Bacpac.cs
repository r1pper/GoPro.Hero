using System.Text;
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

            UpdatePassword();
            UpdateInformation();
            UpdateStatus();
        }

        public string Password { get; private set; }
        public string Address { get; private set; }

        public BacpacInformation Information
        {
            get
            {
                UpdateInformation();
                return _information;
            }
        }

        public BacpacStatus Status
        {
            get
            {
                UpdateStatus();
                return _status;
            }
        }

        object IFilterProvider.Filter()
        {
            return _filter;
        }

        public Bacpac UpdatePassword()
        {
            var request = CreateCommand<CommandBacpacRetrievePassword>();
            var response = request.Send();

            var length = response.RawResponse[1];
            Password = Encoding.UTF8.GetString(response.RawResponse, 2, length);

            return this;
        }

        private void UpdateStatus()
        {
            var request = CreateCommand<CommandBacpacStatus>();
            var response = request.Send();

            var stream = response.GetResponseStream();
            _status.Update(stream);
        }

        private void UpdateInformation()
        {
            var request = CreateCommand<CommandBacpacInformation>();
            var response = request.Send();

            var stream = response.GetResponseStream();
            _information.Update(stream);
        }

        public Bacpac Shutter(bool open)
        {
            var request = CreateCommand<CommandBacpacShutter>();
            request.State = open;
            request.Send();

            UpdateStatus();
            return this;
        }

        public Bacpac Power(bool on)
        {
            var request = CreateCommand<CommandBacpacPowerUp>();
            request.State = on;
            request.Send();

            UpdateStatus();
            return this;
        }

        private T CreateCommand<T>(string parameter = null) where T : CommandRequest<Bacpac>
        {
            var request = CommandRequest<Bacpac>.Create<T>(this, Address, passPhrase: Password, parameter: parameter);
            return request;
        }

        public static Bacpac Create(string address)
        {
            var bacpac = new Bacpac(address);
            return bacpac;
        }
    }
}