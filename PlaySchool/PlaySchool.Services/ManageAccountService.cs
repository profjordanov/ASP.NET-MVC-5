using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AutoMapper.Mappers;
using PlaySchool.Data;
using PlaySchool.Models;
using PlaySchool.Models.BMs.Account;
using PlaySchool.Models.EntityModels;
using PlaySchool.Models.VMs.Manage;
using PlaySchool.Services.Interfaces;

namespace PlaySchool.Services
{
    public class ManageAccountService :Service, IManageAccountService
    {
        public ManageAccountService(PlaySchoolContext context) : base(context)
        {
        }

        public string UploadProfilePicture(HttpPostedFileBase file, string userId, string path)
        {
            if (file != null && file.ContentLength > 0 && file.FileName.EndsWith("jpg"))
            {
                using (BinaryReader br = new BinaryReader(file.InputStream))
                {
                    if (HasJpegHeader(br))
                    {
                        var Player = Context.Players.FirstOrDefault(p => p.AppUser.Id == userId);
                        if (Player != null)
                        {
                            string fileName = Player.ProfilePictureName;
                            path += fileName;
                            file.SaveAs(path);
                            Player.HaveProfilePicture = true;
                            Context.SaveChanges();
                            return "Profile Picture Changed!";
                        }
                        return "Error with finding current user!";
                    }
                }
                return "This file is not supported!";
            }
            return "This file is not supported. It must be .jpg!";
        }

        public ChangeNameViewModel ChangeName(ChangeNameBm bind,string userId)
        {
            Player player = Context.Players.FirstOrDefault(p=>p.AppUser.Id==userId);
            if (bind == null)
            {
                if (player != null)
                {
                    return new ChangeNameViewModel() {FirstName = player.FirstName, LastName = player.LastName};
                }
                else
                {
                    return null;
                }
            }
            player.FirstName = bind.FirstName;
            player.LastName = bind.LastName;
            Context.SaveChanges();
            return new ChangeNameViewModel() { FirstName = player.FirstName, LastName = player.LastName };
        }

        private bool HasJpegHeader(BinaryReader br)
        {
            UInt16 soi = br.ReadUInt16();  // Start of Image (SOI) marker (FFD8)
            UInt16 marker = br.ReadUInt16(); // JFIF marker (FFE0) or EXIF marker(FF01)
            return soi == 0xd8ff && (marker & 0xe0ff) == 0xe0ff;
        }
    }
}
