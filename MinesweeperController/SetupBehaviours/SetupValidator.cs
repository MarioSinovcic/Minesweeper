using System;
using System.IO;
using MinesweeperController.SetupBehaviours.DTOs;
using Newtonsoft.Json;

namespace MinesweeperController.SetupBehaviours
{
    internal static class SetupValidator
    {
        internal static void ValidateParameters(int width, int height, int difficulty)
        {
            if (width < 1 || width > 99 || height < 1 || height > 99 || difficulty < 1)
            {
                throw new ApplicationException("Invalid input parameters for random generation.");
            }
        }
        
        internal static void ValidatePath(string pathname)
        {
            try
            {
                using var jsonFile = new StreamReader(pathname);
                JsonConvert.DeserializeObject<RandomGridSettingsDTO>(jsonFile.ReadToEnd());
            }
            catch (Exception e)
            {
                throw new IOException("Invalid path for grid creation.", e);
            }
        }
    }
}