using SpeakingRosesTest.Areas.SpeakingRosesTest.StatusBack.Entities;
using System.Data;

/*
 * GUID:e6c09dfe-3a3e-461b-b3f9-734aee05fc7b
 * 
 * Coded by fiyistack.com
 * Copyright © 2024
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
 */

namespace SpeakingRosesTest.Areas.SpeakingRosesTest.StatusBack.Interfaces
{
    public interface IStatusService
    {
        void ExportToExcel(string path, DataTable dtStatus);

        void ExportToCSV(string path, List<Status> lstStatus);

        void ExportToPDF(string path, List<Status> lstStatus);

        List<Status> ImportExcel(string path, int userId);
    }
}