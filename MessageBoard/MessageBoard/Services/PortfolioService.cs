using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using MessageBoard.Data;
using MessageBoard.Models;

namespace MessageBoard.Services
{
    public class PortfolioService : Service
    {
        public IEnumerable<PictureVM> GetPictureVms()
        {
            IEnumerable<Picture> pictures = this.Context.Pictures;
            IEnumerable<PictureVM> vms = Mapper.Instance.Map<IEnumerable<Picture>, IEnumerable<PictureVM>>(pictures);
            return vms;
        }
    }
}