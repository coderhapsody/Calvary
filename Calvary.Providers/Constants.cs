using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calvary.ViewModels;

namespace Calvary.Providers
{
    public static class Constants
    {
        public static IEnumerable<DropDownViewModel> MaritalStatuses
        {
            get
            {
                var list = new List<DropDownViewModel>();
                list.Add(new DropDownViewModel() { Text = "Tidak Kawin", Value = "TK"});
                list.Add(new DropDownViewModel() { Text = "Janda", Value = "JD" });
                list.Add(new DropDownViewModel() { Text = "Duda", Value = "DD" });
                list.Add(new DropDownViewModel() { Text = "Kawin", Value = "K" });
                return list;
            }            
        }

        public static IEnumerable<DropDownViewModel> BloodTypes
        {
            get
            {
                var list = new List<DropDownViewModel>();
                list.Add(new DropDownViewModel() { Text = "A", Value = "A" });
                list.Add(new DropDownViewModel() { Text = "B", Value = "B" });
                list.Add(new DropDownViewModel() { Text = "AB", Value = "AB" });
                list.Add(new DropDownViewModel() { Text = "O", Value = "O" });
                return list;
            }
        }

        public static IEnumerable<DropDownViewModel> Genders
        {
            get
            {
                var list = new List<DropDownViewModel>();
                list.Add(new DropDownViewModel() { Text = "Laki-Laki", Value = "L" });
                list.Add(new DropDownViewModel() { Text = "Perempuan", Value = "P" });
                return list;
            }
        }

        public static IEnumerable<DropDownViewModel> ChrismationTypes
        {
            get
            {
                var list = new List<DropDownViewModel>();
                list.Add(new DropDownViewModel() { Text = "Baptis", Value = "B" });
                list.Add(new DropDownViewModel() { Text = "Sidi", Value = "S" });                
                return list;
            }
        }

    }
}
