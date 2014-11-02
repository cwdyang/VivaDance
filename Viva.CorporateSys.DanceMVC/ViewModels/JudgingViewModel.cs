using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Viva.CorporateSys.DanceAPI;

namespace Viva.CorporateSys.DanceMVC.ViewModels
{
    public class JudgingViewModel
    {
        public Judge Judge { get; set; }
        public List<Competition> Competitions { get; set; }


        public JudgingViewModel(Judge judge, List<Competition> competitions)
        {
            Judge = judge;
            Competitions = competitions;
        }
    }


}