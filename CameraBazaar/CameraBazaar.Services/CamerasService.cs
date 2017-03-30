using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CameraBazaar.Models.Enitities;
using CameraBazaar.Models.ViewModels;

namespace CameraBazaar.Services
{
    public class CamerasService : Service
    {
        public IEnumerable<ShortCameraVm> GetAllCameras()
        {
            IEnumerable<Camera> cameras = this.Context.Cameras;
            IEnumerable<ShortCameraVm> vms = Mapper.Map<IEnumerable<Camera>, IEnumerable<ShortCameraVm>>(cameras);
            return vms;
        }

        public DetailsCameraVm GetDetailsVm(int? id, User user)
        {
            User currentUser = null;
            if (user != null)
            {
                currentUser = this.Context.Users.Find(user.Id);
            }
            Camera camera = this.Context.Cameras.FirstOrDefault(camera1 => camera1.Id == id);
            if (camera == null)
            {
                return null;
            }

            DetailsCameraVm vm = Mapper.Map<Camera, DetailsCameraVm>(camera);
            vm.Username = currentUser?.Username;
            return vm;
        }
    }
}