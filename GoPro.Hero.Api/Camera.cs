using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoPro.Hero.Api.Commands;
using GoPro.Hero.Api.Commands.CameraCommands;
using GoPro.Hero.Api.Exceptions;

namespace GoPro.Hero.Api
{
    public class Camera:IHeroCamera
    {
        private Bacpac _bacpac;

        private CameraInformation _information;
        private CameraExtendedSettings _extendedSettings;
        private CameraSettings _settings;

        public CameraInformation Information
        {
            get
            {
                this.GetInformation();
                return _information;
            }
        }
        public CameraExtendedSettings ExtendedSettings
        {
            get
            {
                this.GetExtendedSettings();
                return _extendedSettings;
            }
        }
        public CameraSettings Settings
        {
            get
            {
                this.GetSettings();
                return _settings;
            }
        }
        public BacpacStatus BacpacStatus
        {
            get { return this._bacpac.Status; }
        }
        public BacpacInformation BacpacInformation
        {
            get { return this._bacpac.Information; }
        }

        private void GetInformation()
        {
            var request = this.PrepareCommand<CommandCameraInformation>();
            var response = request.Send();

            var stream = response.GetResponseStream();
            this._information.Update(stream);
        }
        private void GetSettings()
        {
            var request = this.PrepareCommand<CommandCameraSettings>();
            var response = request.Send();

            var stream = response.GetResponseStream();
            this._settings.Update(stream);
        }
        private void GetExtendedSettings()
        {
            var request = this.PrepareCommand<CommandCameraExtendedSettings>();
            var response = request.Send();

            var stream = response.GetResponseStream();
            this._extendedSettings.Update(stream);
        }

        public IHeroCamera Shutter(bool open)
        {
            _bacpac.Shutter(open);
            return this;
        }
        public IHeroCamera Power(bool on)
        {
            _bacpac.Power(on);
            return this;
        }

        public IHeroCamera Command(CommandRequest command)
        {
            var response = command.Send();
            return this;
        }
        public IHeroCamera Command(CommandRequest command,out CommandResponse commandResponse,bool checkStatus=true)
        {
            commandResponse = this.Command(command,checkStatus);
            return this;
        }
        public CommandResponse Command(CommandRequest command, bool checkStatus = true)
        {
            return command.Send(checkStatus);
        }

        public T PrepareCommand<T>() where T : CommandRequest
        {
            return CommandRequest.Create<T>(this._bacpac.Address, passPhrase: this._bacpac.Password);
        }
        public IHeroCamera PrepareCommand<T>(out T command) where T : CommandRequest
        {
            command = this.PrepareCommand<T>();
            return this;
        }

        private Camera()
        {
            this._information = new CameraInformation();
            this._extendedSettings = new CameraExtendedSettings();
            this._settings = new CameraSettings();
        }

        public static T Create<T>(Bacpac bacpac) where T : Camera,IHeroCamera
        {
            var camera = Activator.CreateInstance<T>();
            camera._bacpac = bacpac;

            return camera;
            //return camera.UpdateInformation().UpdateStatus() as T;
        }
    }
}
