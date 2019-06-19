﻿using A3ServerTool.Models;
using Interchangeable.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3ServerTool.Storage
{
    public class MissionDao : IDao<Mission>
    {
        public IList<Mission> GetAll(string gamePath)
        {
            var missions = new List<Mission>();

            var missionFolderContent = FileHelper.GetFolder(gamePath)?.FirstOrDefault(f => f.Name == Constants.MissionFolderName);
            if (missionFolderContent == null) return missions;

            var files = FileHelper.GetAllFiles(missionFolderContent).Where(f => f.Extension == Constants.MissionFileExtension);

            Parallel.ForEach(files, file =>
            {
                var mission = new Mission
                {
                    Name = file.Name.Replace(Constants.MissionFileExtension, string.Empty)
                };
                missions.Add(mission);
            });

            return missions;
        }

        public void Delete(Mission item)
        {
            throw new NotImplementedException();
        }

        public Mission Get(Mission item)
        {
            throw new NotImplementedException();
        }

        public IList<Mission> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Save(Mission item)
        {
            throw new NotImplementedException();
        }
    }
}
