﻿using A3ServerTool;
using A3ServerTool.Models;
using A3ServerTool.Models.Config;
using Interchangeable.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace A3ServerTool.Storage
{
    /// <inheritdoc/>
    public class BasicConfigDao : IConfigDao<BasicConfig>
    {
        /// <inheritdoc/>
        public BasicConfig Get(Profile profile)
        {
            var file = FileHelper.GetFile(Path.Combine(Constants.RootFolder, Profile.StorageFolder, profile.Id.ToString(),
                    Constants.BasicConfigFileName) + Constants.ConfigFileExtension);
            if (file == null) return null;

            var properties = TextManager.ReadFileLineByLine(file);
            if (!properties.Any()) return null;

            var result = TextParseHandler.Parse<BasicConfig>(properties);
            result.FileLocation = file.FullName;
            return result;
        }

        /// <inheritdoc/>
        public void Save(Profile profile)
        {
            var configDto = new SaveDataDto
            {
                Content = string.Join("\r\n", TextParseHandler.ConvertToText(profile.BasicConfig)),
                FileExtension = Constants.ConfigFileExtension,
                FileName = Constants.BasicConfigFileName,
                Folders = new List<string>
                    {
                        Constants.RootFolder,
                        Profile.StorageFolder,
                        profile.Id.ToString()
                    }
            };

            profile.BasicConfig.FileLocation = configDto.GetFullPath();
            FileHelper.Save(configDto);
        }
    }
}
